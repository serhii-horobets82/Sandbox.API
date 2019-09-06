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
        private readonly EvoflareDbContext _context;

        public _360questionarieController(EvoflareDbContext context)
        {
            _context = context;
        }

        // GET: api/_360questionarie
        [HttpGet]
        public async Task<ActionResult<IEnumerable<_360questionnarie>>> Get_360questionarie()
        {
            return await _context._360questionnarie.ToListAsync();
        }

        //// GET: api/_360questionarie/groups
        //[HttpGet("groups")]
        //public async Task<ActionResult<IEnumerable<_360feedbackGroup>>> Get_360questionarieGroups()
        //{
        //    return await _context._360feedbackGroup.ToListAsync();
        //}

        // GET: api/_360questionarie/5/questions
        [HttpGet("{id}/questions")]
        public async Task<ActionResult<IEnumerable<_360questionnarieStatement>>> Get_360questionarieQuestions(int id)
        {
            var questions = await _context._360questionnarieStatement
                .Where(q => q.QuestionnarieId == id)
                .ToListAsync();
            
            return questions;
        }
        
        // GET: api/_360questionarie/5/questions
        [HttpPost("{id}/questions")]
        public async Task<IActionResult> Save_360questionarieQuestions(int id, IEnumerable<_360questionnarieStatement> questions)
        {
            foreach (var item in questions)
            {
                if (item.Id == 0)
                {
                    _context._360questionnarieStatement.Add(item);
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

        //// GET: api/_360questionarie/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<_360questionarie>> Get_360questionarie(int id)
        //{
        //    var _360questionarie = await _context._360questionarie.FindAsync(id);

        //    if (_360questionarie == null)
        //    {
        //        return NotFound();
        //    }

        //    return _360questionarie;
        //}

        // PUT: api/_360questionarie/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put_360questionarie(int id, _360questionnarie _360questionarie)
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
        public async Task<ActionResult<_360questionnarie>> Post_360questionarie(_360questionnarie _360questionarie)
        {
            //_360questionarie._360questionToMark = new List<_360questionToMark>
            //{
            //    new _360questionToMark {MarkId = 1, _360question = new List<_360question> { new _360question { }} },
            //    new _360questionToMark {MarkId = 3, _360question = new List<_360question> { new _360question { }} },
            //    new _360questionToMark {MarkId = 5, _360question = new List<_360question> { new _360question { }} }
            //};
            _360questionarie._360questionnarieStatement = 
                Enumerable.Range(1, 5)
                    .Select(i => new _360questionnarieStatement { Mark = i, Text = " " })
                    .ToList();
            
            _context._360questionnarie.Add(_360questionarie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Get_360questionarie", new { id = _360questionarie.Id }, _360questionarie);
        }

        //// DELETE: api/_360questionarie/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<_360questionnarie>> Delete_360questionarie(int id)
        //{
        //    var _360questionarie = await _context._360questionnarie.FindAsync(id);
        //    if (_360questionarie == null)
        //    {
        //        return NotFound();
        //    }

        //    _context._360questionnarie.Remove(_360questionarie);
        //    await _context.SaveChangesAsync();

        //    return _360questionarie;
        //}

        private bool _360questionarieExists(int id)
        {
            return _context._360questionnarie.Any(e => e.Id == id);
        }
    }
}
