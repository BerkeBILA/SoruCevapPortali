using SoruCevapPortali.DTO;
using SoruCevapPortali.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace SoruCevapPortali.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly SoruCevapContext _context;

        public AnswerController(SoruCevapContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAnswers()
        {
            var answers = await _context.Answer
                .Where(i => i.IsActive)
                .Select(p => ProductToDTO(p))
                .ToListAsync();

            return Ok(answers);
        }

        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAnswer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer
                .Where(i => i.AnswerId == id)
                .Select(p => ProductToDTO(p))
                .FirstOrDefaultAsync();

            if (answer == null)
            {
                return NotFound();
            }

            return Ok(answer);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateAnswer(Answer entity)
        {
            _context.Answer.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnswer), new { id = entity.AnswerId }, entity);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAnswer(int id, Answer entity)
        {
            if (id != entity.AnswerId)
            {
                return BadRequest();
            }

            var answer = await _context.Answer.FirstOrDefaultAsync(i => i.AnswerId == id);

            if (answer == null)
            {
                return NotFound();
            }

            answer.AnswerName = entity.AnswerName;
            answer.Title = entity.Title;
            answer.IsActive = entity.IsActive;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnswer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var answer = await _context.Answer.FirstOrDefaultAsync(i => i.AnswerId == id);

            if (answer == null)
            {
                return NotFound();
            }

            _context.Answer.Remove(answer);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return NotFound();
            }

            return NoContent();
        }

        private static AnswerDTO ProductToDTO(Answer p)
        {
            var entity = new AnswerDTO();
            if (p != null)
            {
                entity.AnswerId = p.AnswerId;
                entity.AnswerName = p.AnswerName;
                entity.Title = p.Title;
                entity.IsActive = p.IsActive;
            }
            return entity;
        }
    }
}
