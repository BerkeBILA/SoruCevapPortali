using SoruCevapPortali.DTO;
using SoruCevapPortali.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmlakPortali.Controllers
{
    // localhost:5000/api/products
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionController : ControllerBase
    {
        
        private readonly SoruCevapContext _context;

        public QuestionController(SoruCevapContext context)
        {
           _context = context;
        }

        // localhost:5000/api/products => GET
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var questions = await _context.Question.Where(i => i.IsActive).Select(p => 
            ProductToDTO(p))
            .ToListAsync();
            return Ok(questions);
        }

        // localhost:5000/api/products/1 => GET
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int? id)
        {
           if(id == null)
            {
                return NotFound();
            }

            var p = await _context
                .Question.Where(i => i.QuestionId == id)
                .Select(p => ProductToDTO(p))
                .FirstOrDefaultAsync();

            if(p == null)
            {
                return NotFound();
            }

            return Ok(p);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Question entity)
        {
            _context.Question.Add(entity);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = entity.QuestionId }, entity);
        }

        // localhost:5000/api/products/5 => GET
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, Question entity)
        {
            if (id != entity.QuestionId)
            {
                return BadRequest();
            }

            var question = await _context.Question.FirstOrDefaultAsync(i => i.QuestionId == id);

            if (question == null)
            {
                return NotFound();
            }

            question.QuestionName = entity.QuestionName;
            question.Title = entity.Title;
            question.IsActive = entity.IsActive;

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
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if(id == null) 
            { 
                return NotFound();
            }

            var question = await _context.Question.FirstOrDefaultAsync(i => i.QuestionId == id);

            if(question == null)
            {
                return NotFound();
            }

            _context.Question.Remove(question);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception)
            {
                return NotFound();
            }
            return NoContent();

        }
        private static QuestionDTO ProductToDTO(Question p)
        {
            var entity = new QuestionDTO();
            if(p != null)
            {
                entity.QuestionId = p.QuestionId;
                entity.QuestionName = p.QuestionName;
                entity.Title = p.Title;
            }
            return entity;
        }
    }


}