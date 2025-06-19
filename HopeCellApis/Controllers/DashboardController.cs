using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HopeCellApis.Models;
namespace HopeCellApis.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : Controller
    {
        private readonly HopeCellDbContext _context;
        public DashboardController(HopeCellDbContext context)
        {
            _context = context;
        }
        [HttpGet("overview")]
        public IActionResult GetDashboardOverview()
        {
            var totalUrgentCases = _context.ManageUrgentCases.Count();

            // Temporary fake values (replace with real logic if needed)
            var donationsReceived = _context.ManageDonations.Count();
            var registeredDonors = _context.ManageVolunteers.Count();

            return Ok(new
            {
                totalUrgentCases,
                donationsReceived,
                registeredDonors
            });
        }

    }
}
