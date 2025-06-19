using HopeCellApis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HopeCellApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageDonationsController : Controller
    {
        private readonly HopeCellDbContext _context;


        public ManageDonationsController(HopeCellDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var donations = await _context.ManageDonations.ToListAsync();
            return Ok(donations);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ManageDonation donation)
        {
            _context.ManageDonations.Add(donation);
            await _context.SaveChangesAsync();
            return Ok(donation);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDonation(int id, [FromBody] ManageDonation donation)
        {
            if (id != donation.Id)
            {
                return BadRequest("Donation ID mismatch");
            }

            var existingDonation = await _context.ManageDonations.FindAsync(id);
            if (existingDonation == null)
            {
                return NotFound();
            }

            existingDonation.Donor = donation.Donor;
            existingDonation.Amount = donation.Amount;
            existingDonation.Date = donation.Date;

            await _context.SaveChangesAsync();

            return Ok(existingDonation);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var donation = await _context.ManageDonations.FindAsync(id);
            if (donation == null) return NotFound();
            _context.ManageDonations.Remove(donation);
            await _context.SaveChangesAsync();
            return Ok(donation);
        }
    }
}
