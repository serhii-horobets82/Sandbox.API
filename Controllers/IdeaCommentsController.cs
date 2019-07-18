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
    public class IdeaCommentsController : ControllerBase
    {
        private readonly EvoflareDbContext _context;

        public IdeaCommentsController(EvoflareDbContext context)
        {
            _context = context;
        }

        //// GET: api/IdeaComments
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<IdeaComment>>> GetIdeaComment()
        //{
        //    return await _context.IdeaComment.ToListAsync();
        //}

        // GET: api/IdeaComments/1/5
        [HttpGet("{ideaId}/{id}")]
        public async Task<ActionResult<IdeaComment>> GetIdeaComment(int ideaId, int id)
        {
            var ideaComment = await _context.IdeaComment
                .Include(c => c.CreatedBy)
                .FirstOrDefaultAsync(c => c.Id == id && c.IdeaId == ideaId);

            if (ideaComment == null)
            {
                return NotFound();
            }

            return ideaComment;
        }

        //// PUT: api/IdeaComments/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutIdeaComment(int id, IdeaComment ideaComment)
        //{
        //    if (id != ideaComment.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(ideaComment).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!IdeaCommentExists(id))
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

        // POST: api/IdeaComments
        [HttpPost]
        public async Task<ActionResult<IdeaComment>> PostIdeaComment(IdeaComment ideaComment)
        {
            ideaComment.CreatedDate = DateTime.UtcNow;
            ideaComment.CreatedById = 1;
            ideaComment.OrganizationId = 1;

            _context.IdeaComment.Add(ideaComment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIdeaComment", new { id = ideaComment.Id, ideaId = ideaComment.IdeaId }, ideaComment);
        }

        //// DELETE: api/IdeaComments/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<IdeaComment>> DeleteIdeaComment(int id)
        //{
        //    var ideaComment = await _context.IdeaComment.FindAsync(id);
        //    if (ideaComment == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.IdeaComment.Remove(ideaComment);
        //    await _context.SaveChangesAsync();

        //    return ideaComment;
        //}

        //private bool IdeaCommentExists(int id)
        //{
        //    return _context.IdeaComment.Any(e => e.Id == id);
        //}
    }
}
