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
    public class _360questionarieController : ControllerBase
    {
        private readonly TechnicalEvaluationContext _context;

        public _360questionarieController(TechnicalEvaluationContext context)
        {
            _context = context;
        }

        // GET: api/_360questionarie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<_360questionarie>>> Get_360questionarie()
        {
            return await _context._360questionarie.ToListAsync();
        }

        // GET: api/_360questionarie/groups
        [HttpGet("groups")]
        public async Task<ActionResult<IEnumerable<_360feedbackGroup>>> Get_360questionarieGroups()
        {
            return await _context._360feedbackGroup.ToListAsync();
        }

        // GET: api/_360questionarie/5/questions
        [HttpGet("{id}/questions")]
        public async Task<ActionResult<IEnumerable<_360questionToMark>>> Get_360questionarieQuestions(int id)
        {
            var questions = await _context._360questionToMark
                .Include(q => q._360question)
                .Where(q => q.QuestionId == id)
                .ToListAsync();
            
            return questions;
        }
        
        // GET: api/_360questionarie/5/questions
        [HttpPost("{id}/questions")]
        public async Task<IActionResult> Save_360questionarieQuestions(int id, IEnumerable<_360question> questions)
        {
            foreach (var item in questions)
            {
                item.OrganizationId = 1;
                if (item.Id == 0)
                {
                    _context._360question.Add(item);
                }
                else
                {
                    _context.Entry(item).State = EntityState.Modified;
                }
            }
            //_context._360question.AddRange(questions);
            //foreach (var item in questions)
            //{
            //    foreach (var q in item._360question)
            //    {
            //        if (q.Id != 0)
            //        {
            //            _context.Entry(q).State = EntityState.Modified;
            //        }
            //    }
            //}

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // GET: api/_360questionarie/5
        [HttpGet("{id}")]
        public async Task<ActionResult<_360questionarie>> Get_360questionarie(int id)
        {
            var _360questionarie = await _context._360questionarie.FindAsync(id);

            if (_360questionarie == null)
            {
                return NotFound();
            }

            return _360questionarie;
        }

        // PUT: api/_360questionarie/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put_360questionarie(int id, _360questionarie _360questionarie)
        {
            if (id != _360questionarie.Id)
            {
                return BadRequest();
            }

            _context.Entry(_360questionarie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_360questionarieExists(id))
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

        // POST: api/_360questionarie
        [HttpPost]
        public async Task<ActionResult<_360questionarie>> Post_360questionarie(_360questionarie _360questionarie)
        {
            //_360questionarie._360questionToMark = new List<_360questionToMark>
            //{
            //    new _360questionToMark {MarkId = 1, _360question = new List<_360question> { new _360question { }} },
            //    new _360questionToMark {MarkId = 3, _360question = new List<_360question> { new _360question { }} },
            //    new _360questionToMark {MarkId = 5, _360question = new List<_360question> { new _360question { }} }
            //};
            _360questionarie.OrganizationId = 1;
            _360questionarie._360questionToMark = new List<_360questionToMark>
            {
                new _360questionToMark {MarkId = 1,OrganizationId = 1},
                new _360questionToMark {MarkId = 3,OrganizationId = 1},
                new _360questionToMark {MarkId = 5,OrganizationId = 1}
            };
            _context._360questionarie.Add(_360questionarie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get_360questionarie", new { id = _360questionarie.Id }, _360questionarie);
        }

        // DELETE: api/_360questionarie/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<_360questionarie>> Delete_360questionarie(int id)
        {
            var _360questionarie = await _context._360questionarie.FindAsync(id);
            if (_360questionarie == null)
            {
                return NotFound();
            }

            _context._360questionarie.Remove(_360questionarie);
            await _context.SaveChangesAsync();

            return _360questionarie;
        }

        private bool _360questionarieExists(int id)
        {
            return _context._360questionarie.Any(e => e.Id == id);
        }
    }
}
