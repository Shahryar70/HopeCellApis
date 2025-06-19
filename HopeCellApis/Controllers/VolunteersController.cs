using HopeCellApis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HopeCellApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VolunteersController : Controller
    {
        private readonly HopeCellDbContext _context;
        public VolunteersController(HopeCellDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVolunteers()
        {
            var volunteers = await _context.ManageVolunteers.ToListAsync();
            return Ok(volunteers);
        }

        [HttpGet("{id}")]
        public IActionResult GetVolunteer(int id)
        {
            var volunteer = _context.ManageVolunteers.Find(id);
            if (volunteer == null) return NotFound();
            return Ok(volunteer);
        }

        [HttpPost]
        public IActionResult CreateVolunteer([FromBody] ManageVolunteer volunteer)
        {
            _context.ManageVolunteers.Add(volunteer);
            _context.SaveChanges();
            return Ok(volunteer);
        }
        [HttpPut("{id}")]
        public IActionResult UpdateVolunteer(int id, [FromBody] ManageVolunteer volunteer)
        {
            if (id != volunteer.Id) return BadRequest();

            var existing = _context.ManageVolunteers.Find(id);
            if (existing == null) return NotFound();

            existing.Name = volunteer.Name;
            existing.Email = volunteer.Email;
            existing.Joined = volunteer.Joined;

            _context.SaveChanges();
            return Ok(existing);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVolunteer(int id)
        {
            var volunteer = _context.ManageVolunteers.Find(id);
            if (volunteer == null) return NotFound();

            _context.ManageVolunteers.Remove(volunteer);
            _context.SaveChanges();
            return Ok();
        }
    }
}
