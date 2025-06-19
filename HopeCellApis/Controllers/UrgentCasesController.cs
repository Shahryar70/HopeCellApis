using HopeCellApis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HopeCellApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UrgentCasesController : ControllerBase
    {
        private readonly HopeCellDbContext _context;
        public UrgentCasesController(HopeCellDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.ManageUrgentCases.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManageUrgentCase model)
        {
            _context.ManageUrgentCases.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ManageUrgentCase updatedCase)
        {
            var existing = await _context.ManageUrgentCases.FindAsync(id);
            if (existing == null) return NotFound();

            existing.Type = updatedCase.Type;
            existing.Patient = updatedCase.Patient;
            existing.Location = updatedCase.Location;
            existing.Deadline = updatedCase.Deadline;
            existing.Priority = updatedCase.Priority;

            await _context.SaveChangesAsync();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _context.ManageUrgentCases.FindAsync(id);
            if (existing == null) return NotFound();

            _context.ManageUrgentCases.Remove(existing);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
