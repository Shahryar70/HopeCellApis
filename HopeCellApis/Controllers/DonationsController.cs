using HopeCellApis.Models;
using HopeCellApis.Services;
using Microsoft.AspNetCore.Mvc;

namespace HopeCellApis.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DonationsController : ControllerBase
    {
        private readonly HopeCellDbContext _context;
        private readonly ILogger<DonationsController> _logger;

        public DonationsController(
            HopeCellDbContext context,
            ILogger<DonationsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpPost]
        public async Task<ActionResult> CreateDonation([FromBody] Donation donation)
        {
            try
            {
                // Set default values
                donation.DonationDate = DateTime.UtcNow;
                donation.PaymentStatus = "Recorded"; // Since we're not processing payments
                donation.TransactionId = Guid.NewGuid().ToString(); // Generate a pseudo transaction ID

                if (string.IsNullOrEmpty(donation.Country))
                {
                    donation.Country = "Pakistan";
                }

                _context.Donations.Add(donation);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    DonationId = donation.DonationId,
                    Message = "Donation recorded successfully"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving donation");
                return StatusCode(500, new { error = "Failed to save donation" });
            }
        }
    }
}
