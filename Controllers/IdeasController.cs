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
    public class IdeasController : ControllerBase
    {
        private readonly EvoflareDbContext _context;

        public IdeasController(EvoflareDbContext context)
        {
            _context = context;
        }

        // GET: api/Ideas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Idea>>> GetIdea()
        {
            return await _context.Idea
                .Include(i => i.CreatedBy)
                .Include(i => i.IdeaTagRef)
                .Include(i => i.IdeaComment)
                .Include(i => i.IdeaLike)
                .Include(i => i.IdeaView)
                .ToListAsync();
        }

        // GET: api/Ideas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Idea>> GetIdea(int id)
        {
            var idea = await _context.Idea
                .Include(i => i.CreatedBy)
                .Include(i => i.IdeaTagRef)
                .Include(i => i.IdeaComment)
                    .ThenInclude(c => c.CreatedBy)
                .Include(i => i.IdeaLike)
                .Include(i => i.IdeaView)
                .FirstOrDefaultAsync(i => i.Id == id);
            idea.IdeaComment = idea.IdeaComment.Where(c => !c.ParentCommentId.HasValue).ToList();
            if (idea == null)
            {
                return NotFound();
            }

            return idea;
        }

        //// PUT: api/Ideas/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutIdea(int id, Idea idea)
        //{
        //    if (id != idea.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(idea).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!IdeaExists(id))
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

        // POST: api/Ideas
        [HttpPost]
        public async Task<ActionResult<Idea>> PostIdea(Idea idea)
        {
            idea.CreatedById = 1;
            idea.CreatedDate = DateTime.UtcNow;
            idea.OrganizationId = 1;
            
            _context.Idea.Add(idea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIdea", new { id = idea.Id }, idea);
        }

        //// DELETE: api/Ideas/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Idea>> DeleteIdea(int id)
        //{
        //    var idea = await _context.Idea.FindAsync(id);
        //    if (idea == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Idea.Remove(idea);
        //    await _context.SaveChangesAsync();

        //    return idea;
        //}

        //private bool IdeaExists(int id)
        //{
        //    return _context.Idea.Any(e => e.Id == id);
        //}
    }
}
