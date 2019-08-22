using Evoflare.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;


namespace Evoflare.API.Data
{
    public class SeedTestData
    {
        public static void RemoveExisting360EvaluationData(EvoflareDbContext context)
        {
            context.Database.ExecuteSqlCommand("delete from \"360Evaluation\"");
            context.Database.ExecuteSqlCommand("delete from \"360EmployeeEvaluation\"");
            context.Database.ExecuteSqlCommand("delete from \"EmployeeEvaluation\"");
        }

		public static bool Seed_360EvaluationForAnalytics(EvoflareDbContext context)
        {
            var hrId = 27;
            var relations = BuildRelations(context);
            var questions = context._360questionarie
                .Include(g => g._360questionToMark)
                .ToList();

            var peerFeedbackGroupId = 2;  // peer
            var peersQuestions = questions
                .Where(g => g._360feedbackGroupId == peerFeedbackGroupId)
                .ToList();

            var random = new Random(Guid.NewGuid().GetHashCode());

            var evaluations = new List<EmployeeEvaluation>();
            var _360employeeEvaluations = new List<_360employeeEvaluation>();
            var amountAll = 0;
            var amountSkipped = 0;

            //var chanceToEvaluatePeerPercent = 80;
            //var markChanceDistribution = new Dictionary<int, int>
            //        {
            //            { 1, 2 },
            //            { 2, 5 },
            //            { 3, 25 },
            //            { 4, 45 },
            //            { 5, 23 },
            //        };
            var kind = new
            {
                EvaluationChance = 75,
                FeedbackMarkChances = new Dictionary<int, int>
                        {
                            { 1, 0 },
                            { 2, 0 },
                            { 3, 10 },
                            { 4, 65 },
                            { 5, 25 },
                        },
                MarkDistribution = new int[] { 0, 0, 0, 0, 0, 0 }
            };
            var righteous = new
            {
                EvaluationChance = 75,
                FeedbackMarkChances = new Dictionary<int, int>
                        {
                            { 1, 25 },
                            { 2, 45 },
                            { 3, 30 },
                            { 4, 0 },
                            { 5, 0 },
                        },
                MarkDistribution = new int[] { 0, 0, 0, 0, 0, 0 }
            };
            var hardworker = new
            {
                EvaluationChance = 95,
                FeedbackMarkChances = new Dictionary<int, int>
                        {
                            { 1, 0 },
                            { 2, 0 },
                            { 3, 100 },
                            { 4, 0 },
                            { 5, 0 },
                        },
                MarkDistribution = new int[] { 0, 0, 0, 0, 0, 0 }
            };
            var kindIds = new List<int>
                    {
                        3,  // Karl QA
                        4,  // Marta AutoQA
                        6,  // Linus Developer
                        7,  // Mark Developer
                    };
            var righteousIds = new List<int>
                    {
                        12, // Tapak QA
                        13, // Mikki QA
                        18, // Mila Developer
                    };
            var hardworkerIds = new List<int>
                    {
                        15,  // Billy AutoQA
                        16,  // Todd Developer
                        17,  // Riana Developer
                        21,  // Alex Took
                    };
            var inverseMarkChanceDistribution = new int[] { 0, 0, 0, 0, 0, 0 }; // length 6
            foreach (var i in Enumerable.Range(1, 5))
            {
                //inverseMarkChanceDistribution[i] =
                //    markChanceDistribution.Where(kv => kv.Key <= i).Select(kv => kv.Value).Sum();
                kind.MarkDistribution[i] = kind.FeedbackMarkChances.Where(kv => kv.Key <= i).Select(kv => kv.Value).Sum();
                righteous.MarkDistribution[i] = righteous.FeedbackMarkChances.Where(kv => kv.Key <= i).Select(kv => kv.Value).Sum();
                hardworker.MarkDistribution[i] = hardworker.FeedbackMarkChances.Where(kv => kv.Key <= i).Select(kv => kv.Value).Sum();
            }

            // 360 evaluation routine is starting each quarter 1st day of month
            var periodMonths = 3;
            var startDate = new DateTime(2017, 1, 1);
            // 360 evaluation period lasts for 1 month
            var evaluationPeriodMonth = 1;
            for (var date = startDate; date < DateTime.Now; date = date.AddMonths(periodMonths))
            {
                // evaluation is Archived/Closed after 1 month
                var isArchived = date < DateTime.Now.AddMonths(-evaluationPeriodMonth);
                // for each employee
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
                    if (isArchived)
                    {
                        evaluation.EndedById = hrId;
                        evaluation.EndDate = date.AddMonths(evaluationPeriodMonth);
                        evaluation.Archived = true;
                    }

                    // TODO: debugging
                    evaluations.Add(evaluation);

                    // get all the peers to current employee
                    // let the employee to create feedback for everyone (or mostly everyone)
                    foreach (var peer in relations[employee])
                    {
                        var config =
                            kindIds.Contains(peer.Id)
                                ? kind
                                : righteousIds.Contains(peer.Id)
                                    ? righteous
                                    : hardworkerIds.Contains(peer.Id)
                                        ? hardworker
                                        : null;
                        if (config == null)
                        {
                            continue;
                        }
                        amountAll++;
                        if (random.Next(100) < config.EvaluationChance)
                        {

                            var _360EmployeeEvaluation = new _360employeeEvaluation
                            {
                                Evaluation = evaluation,
                                EvaluatorEmployeeId = peer.Id,
                                _360feedbackGroupId = peerFeedbackGroupId,
                                StartDoing = "",
                                StopDoing = "",
                                OtherComments = "",
                                StartDate = date,
                                OrganizationId = 1
                            };
                            if (isArchived)
                            {
                                _360EmployeeEvaluation.EndDate = date.AddMonths(1);
                            }
                            evaluation._360employeeEvaluation.Add(_360EmployeeEvaluation);

                            // TODO: debugging
                            _360employeeEvaluations.Add(_360EmployeeEvaluation);
                            var feedbacks = peersQuestions.Select(q =>
                            {
                                var mark = GetMarkUsingDistribution(config.MarkDistribution, random);
                                return new _360evaluation
                                {
                                    Evaluation = _360EmployeeEvaluation,
                                    QuestionId = q.Id,
                                    FeedbackMarkId = mark,
                                    OrganizationId = 1
                                };
                            }).ToList();
                            _360EmployeeEvaluation._360evaluation = feedbacks;
                        }
                        else
                        {
                            amountSkipped++;
                        }
                    }
                }
            }

            context.EmployeeEvaluation.AddRange(evaluations.OrderByDescending(x => x.StartDate));
            context.SaveChanges();
            return true;
        }

        private static int GetMarkUsingDistribution(int[] distribution, Random random)
        {
            var num = random.Next(100);
            for(var i = 1; i < distribution.Length; i++)
            {
                if (num < distribution[i]) return i;
            }
            return 0;
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
    }
}
