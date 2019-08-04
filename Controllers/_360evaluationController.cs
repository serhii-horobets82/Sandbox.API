using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Evoflare.API.Models;

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

        // GET: api/_360evaluation
        [HttpGet]
        public async Task<ActionResult<IEnumerable<_360evaluation>>> Get_360evaluation()
        {
            return await _context._360evaluation.ToListAsync();
        }
        
        // GET: api/_360evaluation/5
        [HttpGet("{id}")]
        public async Task<ActionResult<_360evaluation>> Get_360evaluation(int id)
        {
            var _360evaluation = await _context._360evaluation.FindAsync(id);

            if (_360evaluation == null)
            {
                return NotFound();
            }

            return _360evaluation;
        }

        // GET: api/_360evaluation/employee/5/evaluator
        /// <summary>
        /// Gets a questionary for a specific employee. Used when 360 in progress, evaluator needs to give feedback.
        /// </summary>
        [HttpGet("employee/{id}/evaluator")]
        public async Task<ActionResult<List<_360questionarie>>> Get_360evaluationQuestionary(int id)
        {
            var questionarie = await _context._360employeeEvaluation
                .Where(e => e.EvaluatorEmployeeId == GetEmployeeId() && e.Evaluation.EmployeeId == id)
                .Include(e => e._360feedbackGroup)
                    .ThenInclude(f => f._360questionarie)
                        .ThenInclude(q => q._360questionToMark)
                            .ThenInclude(q => q._360question)
                .ToListAsync();

            return questionarie
                .SelectMany(e => e._360feedbackGroup._360questionarie)
                .ToList();
        }

        public class _360FeedbackSubmit
        {
            public string StartDoing { get; set; }
            public string StopDoing { get; set; }
            public string OtherComments { get; set; }
            public List<_360evaluation> Feedbacks { get; set; }
        }

        /// <summary>
        /// Save a full feedback for all the questions in the questionarie.
        /// </summary>
        [HttpPost("feedback/{id}")]
        public async Task<IActionResult> Save360Feedback(int id, _360FeedbackSubmit feedback)
        {
            feedback.Feedbacks.ForEach(f => f.OrganizationId = 1);
            _context._360evaluation.AddRange(feedback.Feedbacks);

            var evaluation = await _context._360employeeEvaluation.FirstOrDefaultAsync(e => e.Id == id);
            evaluation.EndDate = DateTime.UtcNow;
            evaluation.StartDoing = feedback.StartDoing;
            evaluation.StopDoing = feedback.StopDoing;
            evaluation.OtherComments = feedback.OtherComments;
            _context.Entry(evaluation).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok();
        }

        // PUT: api/_360evaluation/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put_360evaluation(int id, _360evaluation _360evaluation)
        {
            if (id != _360evaluation.Id)
            {
                return BadRequest();
            }

            _context.Entry(_360evaluation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_360evaluationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/_360evaluation
        [HttpPost]
        public async Task<ActionResult<_360evaluation>> Post_360evaluation(_360evaluation _360evaluation)
        {
            _context._360evaluation.Add(_360evaluation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get_360evaluation", new { id = _360evaluation.Id }, _360evaluation);
        }

        // DELETE: api/_360evaluation/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<_360evaluation>> Delete_360evaluation(int id)
        {
            var _360evaluation = await _context._360evaluation.FindAsync(id);
            if (_360evaluation == null)
            {
                return NotFound();
            }

            _context._360evaluation.Remove(_360evaluation);
            await _context.SaveChangesAsync();

            return _360evaluation;
        }

        // GET: api/_360evaluation/by-project/5
        [HttpGet("by-project/{id}")]
        public async Task<ActionResult<List<EmployeeRelations>>> Get_360evaluationByProject(int id)
        {
            var employees = await _context.EmployeeRelations
                .Where(r => r.ProjectId == id)
                .Include(r => r.Team)
                .Include(r => r.Employee)
                .Include(r => r.Employee)
                    .ThenInclude(e => e.EmployeeEvaluationEmployee)
                        .ThenInclude(e => e._360employeeEvaluation)
                .Include(r => r.Manager)
                .Include(r => r.Manager)
                    .ThenInclude(e => e.EmployeeEvaluationEmployee)
                        .ThenInclude(e => e._360employeeEvaluation)
                .ToListAsync();
            foreach (var item in employees)
            {
                item.Team.EmployeeRelations = null;
                //if (item.Employee != null)
                //{
                //item.Employee.EcfEmployeeEvaluationEndBy = null;
                //    item.Employee.EcfEmployeeEvaluationStartBy = null;
                //    item.Employee.EcfEmployeeEvaluationEvaluator = null;
                //    item.Employee.EmployeeEvaluationEndedBy = null;
                //    item.Employee.EmployeeEvaluationStartedBy = null;
                //    item.Employee.EmployeeRelationsEmployee = null;
                //    item.Employee.EmployeeRelationsManager = null;
                //}
            }
            return employees;
        }


        // GET: api/_360evaluation/profile
        [HttpGet("profile")]
        public async Task<ActionResult<dynamic>> Get_360evaluationForProfile()
        {
            var id = GetEmployeeId();
            var allEvaluations = await _context.EmployeeEvaluation
                .OrderByDescending(e => e.StartDate)
                .ToListAsync();
            var allTheData = await _context._360employeeEvaluation
                .Select(e => new
                {
                    e.EvaluationId,
                    e.EvaluatorEmployeeId,
                    EvaluationResults = e._360evaluation.Select(ev => new
                    {
                        ev.QuestionId,
                        ev.FeedbackMarkId
                    }).ToList()
                }).ToListAsync();
            var myMeanByQuestion = allTheData
                .Where(d => d.EvaluatorEmployeeId == id)
                .SelectMany(d => d.EvaluationResults)
                .GroupBy(r => r.QuestionId, r => r.FeedbackMarkId)
                .ToDictionary(r => r.Key, r => r.Average());// (r.Sum() * 1.0) / r.Count());
            var companyMeanByQuestion = allTheData
                .SelectMany(d => d.EvaluationResults)
                .GroupBy(r => r.QuestionId, r => r.FeedbackMarkId)
                .ToDictionary(r => r.Key, r => r.Average());// (r.Sum() * 1.0) / r.Count());

            var questions = await _context._360questionarie
                .Where(q => q._360feedbackGroupId == 2) // for peers
                .OrderBy(q => q.Id)
                .ToListAsync();
                //.ToDictionaryAsync(q => q.Id, q => q.Text);
            var barData = new
            {
                My = questions
                    .Select(q => 
                        new { Question = q.Text, Value = Math.Round(myMeanByQuestion[q.Id], 1) })
                    .ToList(),
                Company = questions
                    .Select(q => 
                        new { Question = q.Text, Value = Math.Round(companyMeanByQuestion[q.Id], 1) })
                    .ToList(),
            };
            var quarter = new Dictionary<int, string>
            {
                { 1, "I" },
                { 2, "II" },
                { 3, "III" },
                { 4, "IV" },
            };
            var lineDataCategories = allEvaluations
                .Where(e => e.EmployeeId == id)
                .Take(4)
                .OrderBy(e => e.StartDate)
                .Select(e => $"{quarter[(int)Math.Ceiling((e.StartDate.Month + 1) / 3.0)]} {e.StartDate.Year}")
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
                        Value = f.FeedbackMarkId,
                        Evaluation = e
                    })
                )
                .GroupBy(e => e.Question)
                .Select(e => new
                {
                    Question = questions.First(q => q.Id == e.Key).Text,
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

        private bool _360evaluationExists(int id)
        {
            return _context._360evaluation.Any(e => e.Id == id);
        }
    }
}
