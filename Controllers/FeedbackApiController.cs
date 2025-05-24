using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using prctica3.Data;
using prctica3.Models;

namespace prctica3.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackApiController : ControllerBase
    {
        private readonly FeedbackDbContext _context;

        public FeedbackApiController(FeedbackDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> PostFeedback([FromBody] Feedback feedback)
        {
            var existe = await _context.Feedbacks.AnyAsync(f => f.PostId == feedback.PostId);
            if (existe)
                return BadRequest("Ya enviaste feedback para este post.");

            feedback.Fecha = DateTime.Now;
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();

            return Ok(feedback);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFeedback()
        {
            var lista = await _context.Feedbacks.ToListAsync();
            return Ok(lista);
        }
    }
}
