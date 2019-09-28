using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evoflare.API.Models;
using Evoflare.API.Helpers;

namespace Evoflare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class _360evaluationController : BaseController
    {
        private readonly EvoflareDbContext _context;

        public _360evaluationController(EvoflareDbContext context)
        {
            _context = context;
        }

        //// GET: api/_360evaluation
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<_360evaluation>>> Get_360evaluation()
        //{
        //    return await _context._360evaluation.ToListAsync();
        //}

        //// GET: api/_360evaluation/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<_360evaluation>> Get_360evaluation(int id)
        //{
        //    var _360evaluation = await _context._360evaluation.FindAsync(id);

        //    if (_360evaluation == null)
        //    {
        //        return NotFound();
        //    }

        //    return _360evaluation;
        //}

        // GET: api/_360evaluation/employee/5/evaluator
        /// <summary>
        /// Gets a questionary for a specific employee. Used when 360 in progress, evaluator needs to give feedback.
        /// </summary>
        [HttpGet("employee/{id}/evaluator")]
        public async Task<ActionResult<List<_360questionnarie>>> Get_360evaluationQuestionary(int id)
        {
            var isManager = User.IsManager();
            return await _context._360questionnarie
                .Where(q => q.IsForManager == isManager)
                .Include(q => q._360questionnarieStatement)
                .ToListAsync();
        }

        public class _360FeedbackSubmit
        {
            public string StartDoing { get; set; }
            public string StopDoing { get; set; }
            public string OtherComments { get; set; }
            public List<_360evaluationResult> Feedbacks { get; set; }
        }
        /// <summary>
        /// Save a full feedback for all the questions in the questionarie.
        /// </summary>
        [HttpGet("feedback/{id}")]
        public async Task<IActionResult> Get360Feedback(int id)
        {
            var result = await _context._360employeeEvaluation.AsNoTracking()
                .Include(e => e._360evaluationResult)
                    .ThenInclude(e => e._360questionnarieStatement)
                        .ThenInclude(e => e.Questionnarie)
                .FirstOrDefaultAsync(e => e.Id == id);

            return Ok(result);
        }
        /// <summary>
        /// Save a full feedback for all the questions in the questionarie.
        /// </summary>
        [HttpPost("feedback/{id}")]
        public async Task<IActionResult> Save360Feedback(int id, _360FeedbackSubmit feedback)
        {
            _context._360evaluationResult.AddRange(feedback.Feedbacks);

            var evaluation = await _context._360employeeEvaluation.FirstOrDefaultAsync(e => e.Id == id);
            evaluation.EndDate = DateTime.UtcNow;
            evaluation.StartDoing = feedback.StartDoing;
            evaluation.StopDoing = feedback.StopDoing;
            evaluation.OtherComments = feedback.OtherComments;
            _context.Entry(evaluation).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(evaluation);
        }

        //// PUT: api/_360evaluation/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put_360evaluation(int id, _360evaluation _360evaluation)
        //{
        //    if (id != _360evaluation.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(_360evaluation).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!_360evaluationExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/_360evaluation
        //[HttpPost]
        //public async Task<ActionResult<_360evaluation>> Post_360evaluation(_360evaluation _360evaluation)
        //{
        //    _context._360evaluation.Add(_360evaluation);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("Get_360evaluation", new { id = _360evaluation.Id }, _360evaluation);
        //}

        //// DELETE: api/_360evaluation/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<_360evaluation>> Delete_360evaluation(int id)
        //{
        //    var _360evaluation = await _context._360evaluation.FindAsync(id);
        //    if (_360evaluation == null)
        //    {
        //        return NotFound();
        //    }

        //    _context._360evaluation.Remove(_360evaluation);
        //    await _context.SaveChangesAsync();

        //    return _360evaluation;
        //}

        // GET: api/_360evaluation/periods
        [HttpGet("periods")]
        public async Task<IEnumerable<dynamic>> GetEvaluationPeriods()
        {
            var periods = await _context.EmployeeEvaluation
                .Select(e => e.StartDate)
                .Distinct()
                .OrderByDescending(d => d)
                .ToListAsync();
            var settings = await _context._360evaluationSchedule.FirstAsync();
            return periods
                .Select(d => new
                {
                    Text = DateTimeUtils.ToQuarterYearString(d) + (IsPeriodClosed(d, settings) ? " (Closed)" : ""),
                    Value = DateTimeUtils.GetQuarterStartDate(d),
                    IsClosed = IsPeriodClosed(d, settings)
                });
        }

        // GET: api/_360evaluation/last-period
        [HttpGet("last-period")]
        public async Task<dynamic> GetLastEvaluationPeriod()
        {
            var period = await _context.EmployeeEvaluation
                .Select(e => e.StartDate)
                .Distinct()
                .OrderByDescending(d => d)
                .FirstOrDefaultAsync();
            var settings = await _context._360evaluationSchedule.FirstOrDefaultAsync();
            if (settings != null)
            {
                return new
                {
                    StartDate = period,
                    EndDate = period.AddMonths(settings.EvaluationWindowMonths),
                    IsClosed = IsPeriodClosed(period, settings)
                };
            }
            else
            {
                return new { NotConfigured = true };
            }
        }

        private bool IsPeriodClosed(DateTime date, _360evaluationSchedule settings)
        {
            return date.AddMonths(settings.EvaluationWindowMonths) < DateTime.UtcNow;
        }

        // GET: api/_360evaluation/schedule-settings
        [HttpGet("schedule-settings")]
        public async Task<_360evaluationSchedule> GetEvaluationScheduleSettings()
        {
            return await _context._360evaluationSchedule.FirstAsync();
        }

        // POST: api/_360evaluation/schedule-settings
        [HttpPost("schedule-settings")]
        public async Task<_360evaluationSchedule> SaveEvaluationScheduleSettings(_360evaluationSchedule settings)
        {
            _context.Entry(settings).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return settings;
        }

        // POST: api/_360evaluation/start
        // TODO: remove this after demo, evaluation should start automatically, or by HR approval
        [HttpPost("start/{date}")]
        public async Task<IActionResult> Start360Evaluation(DateTime date)
        {
            var relations = BuildRelations(_context);
            var hrId = User.GetEmployeeId();
            var evaluations = new List<EmployeeEvaluation>();

            foreach (var employee in relations.Keys)
            {
                // create evaluation slot for the employee
                var evaluation = new EmployeeEvaluation
                {
                    EmployeeId = employee.Id,
                    StartDate = date,
                    StartedById = hrId,
                    OrganizationId = 1,
                };

                evaluations.Add(evaluation);

                // get all the peers to current employee
                // let the employee to create feedback for everyone (or mostly everyone)
                foreach (var peer in relations[employee])
                {                    
                    var _360EmployeeEvaluation = new _360employeeEvaluation
                    {
                        Evaluation = evaluation,
                        EvaluatorEmployeeId = peer.Id,
                        StartDoing = null,
                        StopDoing = null,
                        OtherComments = null,
                        StartDate = date,
                        OrganizationId = 1
                    };
                        
                    evaluation._360employeeEvaluation.Add(_360EmployeeEvaluation);
                }
            }
            await _context.EmployeeEvaluation.AddRangeAsync(evaluations.OrderByDescending(x => x.StartDate));
            await _context.SaveChangesAsync();
            return Ok(date);   
        }

        private static Dictionary<Employee, List<Employee>> BuildRelations(EvoflareDbContext context)
        {
            var employees = context.Employee.ToList();
            var relations = context.EmployeeRelations
                .Include(r => r.Employee)
                .Include(r => r.Manager)
                .ToList();

            var employeePeers = new Dictionary<Employee, List<Employee>>();
            foreach (var employee in employees.Where(e => !e.IsManager))
            {
                var a = relations.Where(r => r.EmployeeId == employee.Id).ToList();
                var teams = a.Select(r => r.TeamId).Distinct().ToHashSet();
                var directManagerIds = a.Select(r => r.ManagerId).Where(m => m != null).Distinct().ToHashSet();

                var directManagers = employees
                    .Where(e => directManagerIds.Contains(e.Id))
                    .ToList();

                var peersAndManagers = relations
                    .Where(r => teams.Contains(r.TeamId))
                    .ToList();
                var peers = peersAndManagers.Select(r => r.Employee).Where(e => e != null)
                    //.Concat(
                    //    peersAndManagers.Select(r => r.Manager).Where(m => m != null)
                    //)
                    .Where(e => e.Id != employee.Id)
                    .Distinct().ToList();
                employeePeers.Add(employee, peers);
            }
            return employeePeers;
        }

        // GET: api/_360evaluation/by-project/5
        [HttpGet("by-project/{id}")]
        public async Task<ActionResult<Dictionary<int, List<dynamic>>>> Get_360evaluationByProject(int id, [FromQuery] DateTime periodStart)
        {
            var employeeId = GetEmployeeId();
            if (!await _context.EmployeeRelations.AnyAsync(r => r.ProjectId == id && r.ManagerId == employeeId))
            {
                return NotFound();
            }

            var employeeRelations = await _context.EmployeeRelations
                .Where(r => r.ProjectId == id)
                .Include(r => r.Team)
                .Include(r => r.Employee)
                .ToListAsync();
            var periodEnd = periodStart.AddMonths(3);
            var evaluations = await _context.EmployeeEvaluation
                .Where(e => e.StartDate >= periodStart && e.StartDate < periodEnd)
                .Include(e => e._360employeeEvaluation)
                .ToListAsync();

            var evaluationsByWhomEvaluating = evaluations.ToLookup(e => e.EmployeeId, e => e._360employeeEvaluation);
            var employeeRelationsByTeam = employeeRelations
                .Where(e => e.TeamId.HasValue && e.EmployeeId.HasValue)
                .ToLookup(e => e.TeamId);

            var results = new Dictionary<int, List<dynamic>>();
            foreach(var kv in employeeRelationsByTeam)
            {
                var teamId = kv.Key.Value;
                var relationIds = employeeRelationsByTeam[teamId].Select(r => r.EmployeeId.Value).ToHashSet();
                var rows = new List<dynamic>();
                foreach (var rel in relationIds)
                {
                    var ev = evaluationsByWhomEvaluating[rel].SelectMany(r => r.Select(x => x)).Select(t => t.EvaluatorEmployeeId).ToList();

                    var data = new
                    {
                        EmployeeId = rel,
                        EvaluatedIds = relationIds.Intersect(ev).Where(i => i != rel),
                        NotEvaluatedIds = relationIds.Except(ev).Where(i => i != rel)
                    };
                    rows.Add(data);
                }
                results.Add(teamId, rows);
            }
            return results;
        }


        // GET: api/_360evaluation/profile
        [HttpGet("profile")]
        public async Task<ActionResult<dynamic>> Get_360evaluationForProfile()
        {
            var id = GetEmployeeId();
            var employee = await _context.Employee.FirstOrDefaultAsync(e => e.Id == id);
            if (employee.IsManager) return Ok(new { });

            var allEvaluations = await _context.EmployeeEvaluation
                .OrderByDescending(e => e.StartDate)
                .ToListAsync();
            var allTheData = await _context._360employeeEvaluation
                .Select(e => new
                {
                    e.EvaluationId,
                    e.EvaluatorEmployeeId,
                    EvaluationResults = e._360evaluationResult.Select(ev => new
                    {
                        QuestionId = ev._360questionnarieStatement.QuestionnarieId,
                        QuestionStatementsId = ev._360questionnarieStatementId,
                        Mark = ev._360questionnarieStatement.Mark
                    }).ToList()
                }).ToListAsync();
            var myMeanByQuestion = allTheData
                .Where(d => d.EvaluatorEmployeeId == id)
                .SelectMany(d => d.EvaluationResults)
                .GroupBy(r => r.QuestionId, r => r.Mark)
                .ToDictionary(r => r.Key, r => r.Average());// (r.Sum() * 1.0) / r.Count());
            var companyMeanByQuestion = allTheData
                .SelectMany(d => d.EvaluationResults)
                .GroupBy(r => r.QuestionId, r => r.Mark)
                .ToDictionary(r => r.Key, r => r.Average());// (r.Sum() * 1.0) / r.Count());

            var isManager = User.IsManager();
            var questions = await _context._360questionnarie
                .Where(q => q.IsForManager == isManager)
                .OrderBy(q => q.Id)
                .ToListAsync();
                //.ToDictionaryAsync(q => q.Id, q => q.Text);
            var barData = new
            {
                My = questions
                    .Select(q => 
                        new { Question = q.Name, Value = Math.Round(myMeanByQuestion[q.Id], 1) })
                    .ToList(),
                Company = questions
                    .Select(q => 
                        new { Question = q.Name, Value = Math.Round(companyMeanByQuestion[q.Id], 1) })
                    .ToList(),
            };

            var lineDataCategories = allEvaluations
                .Where(e => e.EmployeeId == id)
                .Take(4)
                .OrderBy(e => e.StartDate)
                .Select(e => DateTimeUtils.ToQuarterYearString(e.StartDate))
                .ToList();

            var myEvaluation = allEvaluations
                .Where(e => e.EmployeeId == id)
                .Take(4)
                .OrderBy(e => e.StartDate)
                .SelectMany(e => allTheData
                    .Where(d => d.EvaluationId == e.Id)
                    .SelectMany(d => d.EvaluationResults)
                    .Select(f => new
                    {
                        Question = f.QuestionId,
                        Value = f.Mark,
                        Evaluation = e
                    })
                )
                .GroupBy(e => e.Question)
                .Select(e => new
                {
                    Question = questions.First(q => q.Id == e.Key).Name,
                    Values = e
                        .GroupBy(q => q.Evaluation.StartDate, q => q.Value)
                        .Select(q => new { Date = q.Key, Value = Math.Round(q.Average(), 1) })
                        .OrderBy(q => q.Date)
                        .Select(q => q.Value)
                        .ToList()
                });

            var lineData = new
            {
                Categories = lineDataCategories,
                Data = myEvaluation
            };
            // Evaluation (date)
            //      question
            //      mark
            // ===>
            // Question
            //      evaluation
            //      Average(mark)

            return new
            {
                BarData = barData,
                LineData = lineData
            };
        }

        public class Criteria
        {
            public int FeedbacksGiven { get; set; }
            //public Criterias FeedbacksGivenCriteria { get; set; }
            public double MeanValue { get; set; }
            //public Criterias MeanValueCriteria { get; set; }
            //public ICollection<int> NotContainsMarks { get; set; }
            public double NotContainsMarksTreshold { get; set; }
            //public Criterias NotContainsMarksTresholdCriteria { get; set; }

            public int CommentLengthWords { get; set; }
            //public Criterias CommentLengthWordsCriteria { get; set; }

            //public enum Criterias
            //{
            //    More,
            //    MoreOrEqual,
            //    Less,
            //    LessOrEqual,

            //    Equal, // +/- 0.2

            //    LessThanMean,
            //    GreatedThanMean
            //}

            //public class FeedbackCriteria
            //{
            //    public int FeedbacksGiven { get; set; }
            //    public Criterias Criterias { get; set; }
            //}
        }
        public enum AnalysisGroups
        {
            Kind,
            Righteous,
            Hardworker
        }
        public class Analytics360Settings
        {
            public AnalysisGroups Group { get; set; }
            public Criteria Criteria { get; set; }
        }
        private Analytics360Settings[] GetCriterias()
        {
            var criterias = new[]
            {
                new Analytics360Settings
                {
                    Group = AnalysisGroups.Kind,
                    Criteria = new Criteria
                    {
                        FeedbacksGiven = 3,
                        //FeedbacksGivenCriteria = Criteria.Criterias.More,
                        MeanValue = 4.1,
                        //MeanValueCriteria = Criteria.Criterias.More,
                        //NotContainsMarks = new HashSet<int>(new[] { 1, 2 })
                    }
                },
                new Analytics360Settings
                {
                    Group = AnalysisGroups.Righteous,
                    Criteria= new Criteria
                    {
                        FeedbacksGiven = 3,
                        //FeedbacksGivenCriteria = Criteria.Criterias.More,
                        MeanValue = 2.7,
                        //MeanValueCriteria = Criteria.Criterias.Less,
                        //NotContainsMarksTresholdCriteria = Criteria.Criterias.LessThanMean
                    }
                },
                new Analytics360Settings
                {
                    Group = AnalysisGroups.Hardworker,
                    Criteria = new Criteria
                    {
                        //FeedbacksGivenCriteria = Criteria.Criterias.GreatedThanMean,
                        MeanValue = 3,
                        //MeanValueCriteria = Criteria.Criterias.Equal,
                        CommentLengthWords = 3,
                        //CommentLengthWordsCriteria = Criteria.Criterias.LessOrEqual
                    }
                }
            };
            return criterias;
        }
        //private bool ValidateForCriteria(Criteria criteria, _360employeeEvaluation employeeEvaluation, ICollection<_360employeeEvaluation> allEvaluations)
        //{
        //    var checklist = new List<bool>();
        //    var feedbackCriteria = new Dictionary<Criteria.Criterias, Func<int, int, bool>>
        //    {
        //        { Criteria.Criterias.More, (expected, actual) => actual > expected }
        //    };
        //    if (feedbackCriteria.TryGetValue(criteria.FeedbacksGivenCriteria, out var simpleCriteria))
        //    {
        //        checklist.Add(simpleCriteria(criteria.FeedbacksGiven, employeeEvaluation._360evaluation.Count));
        //    } 
        //    else
        //    {
        //        if (criteria.FeedbacksGivenCriteria == Criteria.Criterias.GreatedThanMean)
        //        {
        //            var meanCount = allEvaluations
        //                .GroupBy(e => e.EvaluatorEmployeeId)
        //                .Select(e => e.Count()).Average();
        //            checklist.Add(employeeEvaluation._360evaluation.Count > meanCount);
        //        }
        //    }

        //    var meanFeedbackCriteria = new Dictionary<Criteria.Criterias, Func<double, double, bool>>
        //    {
        //        { Criteria.Criterias.More, (expected, actual) => actual > expected },
        //        { Criteria.Criterias.Less, (expected, actual) => actual > expected },
        //        { Criteria.Criterias.Equal, (expected, actual) => ((expected + 0.1) > actual) && ((expected - 0.1) < actual) },
        //    };
        //    if (meanFeedbackCriteria.TryGetValue(criteria.MeanValueCriteria, out var meanCriteria))
        //    {
        //        var mean = employeeEvaluation._360evaluation.Select(e => e.FeedbackMarkId).Average();
        //        checklist.Add(meanCriteria(criteria.MeanValue, mean));
        //    }

        //    if (criteria.NotContainsMarks.Any())
        //    {
        //        checklist.Add(employeeEvaluation._360evaluation.All(e => !criteria.NotContainsMarks.Contains(e.FeedbackMarkId)));
        //    }
        //    if (criteria.NotContainsMarksTresholdCriteria == Criteria.Criterias.LessThanMean)
        //    {
        //        var meanMark = allEvaluations
        //            .SelectMany(e => e._360evaluation)
        //            .Select(e => e.FeedbackMarkId)
        //            .Average();
        //        checklist.Add(employeeEvaluation._360evaluation.All(e => e.FeedbackMarkId < meanMark));
        //    }

        //    if (criteria.CommentLengthWordsCriteria == Criteria.Criterias.LessOrEqual)
        //    {
        //        employeeEvaluation.
        //    }
        //    return checklist.All(c => c);
        //}
        private dynamic GetStatistics(
            ICollection<_360employeeEvaluation> employeeEvaluations,
            ICollection<_360employeeEvaluation> allEmployeeEvaluations)
        {
            var feedbacks = employeeEvaluations.Count;
            var averageCount = allEmployeeEvaluations
                .GroupBy(e => e.EvaluatorEmployeeId)
                .Select(e => e.Count())
                .Average();
            var feedbackMarks  = employeeEvaluations
                .SelectMany(e => e._360evaluationResult.Select(ev => ev._360questionnarieStatement.Mark))
                .OrderBy(x => x).ToList();
            var averageFeedback = feedbackMarks.Average();
            var allFeedbacksAverage = allEmployeeEvaluations
                .SelectMany(e => e._360evaluationResult.Select(ev => ev._360questionnarieStatement.Mark))
                .Average();
            var variance = Math.Abs(Variance(feedbackMarks));
            return feedbackMarks;
        }
        private bool ValidateForKind(
            ICollection<_360employeeEvaluation> employeeEvaluations, 
            ICollection<_360employeeEvaluation> allEmployeeEvaluations)
        {
            var checks = new List<bool>();
            var criteria = GetCriterias()[0].Criteria;
            // дал более 4 оценок
            checks.Add(employeeEvaluations.Count > criteria.FeedbacksGiven);

            // средний балл выше 4,1
            var feedbacks = employeeEvaluations
                .SelectMany(e => e._360evaluationResult.Select(ev => ev._360questionnarieStatement.Mark))
                .ToList();
            checks.Add(feedbacks.Average() > criteria.MeanValue);

            // среди оценок отсутствуют единицы и двойки
            checks.Add(!feedbacks.Contains(1));
            checks.Add(!feedbacks.Contains(2));

            return checks.All(c => c);
        }
        private bool ValidateForRighteous(
            ICollection<_360employeeEvaluation> employeeEvaluations,
            ICollection<_360employeeEvaluation> allEmployeeEvaluations)
        {
            var checks = new List<bool>();
            var criteria = GetCriterias()[1].Criteria;

            // сделавших более четырех ревью
            checks.Add(employeeEvaluations.Count > criteria.FeedbacksGiven);

            // поставивших меньше 2,7
            var feedbacks = employeeEvaluations
                .SelectMany(e => e._360evaluationResult.Select(ev => ev._360questionnarieStatement.Mark))
                .ToList();
            checks.Add(feedbacks.Average() < criteria.MeanValue);

            // не ставивших оценки выше средней
            var allFeedbacksMean = allEmployeeEvaluations
                .SelectMany(e => e._360evaluationResult.Select(ev => ev._360questionnarieStatement.Mark))
                .Average();
            checks.Add(feedbacks.All(f => f <= allFeedbacksMean));
            
            return checks.All(c => c);
        }
        private bool ValidateForHardworker(
            ICollection<_360employeeEvaluation> employeeEvaluations,
            ICollection<_360employeeEvaluation> allEmployeeEvaluations)
        {
            var checks = new List<bool>();
            var criteria = GetCriterias()[2].Criteria;

            // провели сравнительно много ревью
            var meanCount = allEmployeeEvaluations
                .GroupBy(e => e.EvaluatorEmployeeId)
                .Select(e => e.Count()).Average();
            checks.Add(employeeEvaluations.Count > meanCount);

            // средняя оценка — три,
            var marks = employeeEvaluations
                .SelectMany(e => e._360evaluationResult)
                .Select(e => e._360questionnarieStatement.Mark)
                .ToList();
            var average = marks.Average();
            checks.Add(average == criteria.MeanValue);
            // и дисперсия нулевая
            checks.Add(Math.Abs(Variance(marks)) < 0.001);

            return checks.All(c => c);
        }
        private bool ValidateForSemiHardworker(
            ICollection<_360employeeEvaluation> employeeEvaluations,
            ICollection<_360employeeEvaluation> allEmployeeEvaluations)
        {
            var checks = new List<bool>();
            var criteria = GetCriterias()[2].Criteria;

            // провели сравнительно много ревью
            var meanCount = allEmployeeEvaluations
                .GroupBy(e => e.EvaluatorEmployeeId)
                .Select(e => e.Count()).Average();
            checks.Add(employeeEvaluations.Count > meanCount);

            // средняя оценка — три
            // !! с небольшим отклонением
            // !! дисперсию не учитываем
            var marks = employeeEvaluations
                .SelectMany(e => e._360evaluationResult)
                .Select(e => e._360questionnarieStatement.Mark)
                .ToList();
            var average = marks.Average();
            checks.Add(Math.Abs(average - criteria.MeanValue) < 0.1);

            return checks.All(c => c);
        }

        private double Variance(ICollection<int> nums, double? average = null)
        {
            if (nums.Count > 1)
            {
                // Get the average of the values
                double avg = average.HasValue ? average.Value : nums.Average();

                // Now figure out how far each point is from the mean
                // So we subtract from the number the average
                // Then raise it to the power of 2
                double sumOfSquares = 0.0;

                foreach (int num in nums)
                {
                    sumOfSquares += Math.Pow((num - avg), 2.0);
                }

                // Finally divide it by n - 1 (for standard deviation variance)
                // Or use length without subtracting one ( for population standard deviation variance)
                return sumOfSquares / (nums.Count - 1);
            }
            else { return 0.0; }
        }
        
        // GET: api/_360evaluation/analytics
        [HttpGet("analytics")]
        public async Task<ActionResult<dynamic>> Get_360evaluationAnalytics()
        {
            // TODO: have filter by employees for manager/hr/other?
            //var lastEvaluationsCount = 4;
            var lastEvaluations = await _context.EmployeeEvaluation
                .Where(r => r.StartDate > DateTime.Now.AddYears(-1))
                .Include(e => e._360employeeEvaluation)
                    .ThenInclude(ev => ev._360evaluationResult)
                        .ThenInclude(evr => evr._360questionnarieStatement)
                .ToListAsync();
            var employees = await _context.Employee
                .Where(e => !e.IsManager)
                .ToListAsync();

            var rep = new Dictionary<DateTime, Dictionary<int, List<bool>>>();
            var ids = new HashSet<int>() { 12, 13, 15 };
            //var ids = new HashSet<int>() { 3, 4, 6, 7, 12, 13, 15, 16, 17, 18, 21 };
            foreach (var p in lastEvaluations.ToLookup(e => e.StartDate))
            {
                var dict = new Dictionary<int, List<bool>>();
                rep.Add(p.Key, dict);

                var period = p;
                var allEvaluations = period
                    .SelectMany(c => c._360employeeEvaluation)
                    .ToList();
                foreach (var employee in employees.Where(e => ids.Contains(e.Id)))
                {
                    var evaluations = period
                        .SelectMany(c => c._360employeeEvaluation)
                        .Where(e => e.EvaluatorEmployeeId == employee.Id)
                        .ToList();
                    if (evaluations.Any())
                    {
                        var c = evaluations.Count;
                        var stat = GetStatistics(evaluations, allEvaluations);
                        var isGood = ValidateForKind(evaluations, allEvaluations);
                        var isBad = ValidateForRighteous(evaluations, allEvaluations);
                        var isUgly = ValidateForHardworker(evaluations, allEvaluations);
                        var isSemiUgly = ValidateForSemiHardworker(evaluations, allEvaluations);
                        var all = new List<bool> { isGood, isBad, isUgly, isSemiUgly };
                        if (all.Where(_ => _).Count() > 1)
                        {

                        }
                        dict.Add(employee.Id, all);

                    }
                }
            }

            return rep;
        }

        //private bool _360evaluationExists(int id)
        //{
        //    return _context._360evaluationResult.Any(e => e.Id == id);
        //}
    }
}
