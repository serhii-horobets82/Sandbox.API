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
    public class IdeaTagsController : ControllerBase
    {
        private readonly TechnicalEvaluationContext _context;

        public IdeaTagsController(TechnicalEvaluationContext context)
        {
            _context = context;
        }

        // GET: api/IdeaTags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IdeaTag>>> GetIdeaTag()
        {
            return await _context.IdeaTag.ToListAsync();
        }

        //// GET: api/IdeaTags/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<IdeaTag>> GetIdeaTag(int id)
        //{
        //    var ideaTag = await _context.IdeaTag.FindAsync(id);

        //    if (ideaTag == null)
        //    {
        //        return NotFound();
        //    }

        //    return ideaTag;
        //}

        //// PUT: api/IdeaTags/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutIdeaTag(int id, IdeaTag ideaTag)
        //{
        //    if (id != ideaTag.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ideaTag).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!IdeaTagExists(id))
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

        // POST: api/IdeaTags
        [HttpPost]
        public async Task<ActionResult<IdeaTag>> PostIdeaTag(IdeaTag ideaTag)
        {
            _context.IdeaTag.Add(ideaTag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIdeaTag", new { id = ideaTag.Id }, ideaTag);
        }

        //// DELETE: api/IdeaTags/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<IdeaTag>> DeleteIdeaTag(int id)
        //{
        //    var ideaTag = await _context.IdeaTag.FindAsync(id);
        //    if (ideaTag == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.IdeaTag.Remove(ideaTag);
        //    await _context.SaveChangesAsync();

        //    return ideaTag;
        //}

        //private bool IdeaTagExists(int id)
        //{
        //    return _context.IdeaTag.Any(e => e.Id == id);
        //}
    }
}
