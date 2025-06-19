using HopeCellApis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HopeCellApis.Controllers
{
    [Route("api/[controller]")]  // Fixed: Added missing closing bracket
    [ApiController]
    public class DonorsController : ControllerBase  // Changed from Controller to ControllerBase
    {
        private readonly HopeCellDbContext _context;
        public DonorsController(HopeCellDbContext context)
        {
            _context = context;
        }

        // DonorsController.cs
        [HttpPost]
        public async Task<ActionResult<Donor>> RegisterDonor(Donor donorDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Invalid data",
                        errors = ModelState.Values
                            .SelectMany(v => v.Errors)
                            .Select(e => e.ErrorMessage)
                    });
                }

                if (await _context.Donors.AnyAsync(d => d.Email == donorDto.Email))
                {
                    return Conflict(new
                    {
                        message = "Email address is already registered"
                    });
                }

                var donor = new Donor
                {
                    FullName = donorDto.FullName,
                    Email = donorDto.Email,
                    PhoneNumber = donorDto.PhoneNumber,
                    Gender = donorDto.Gender,
                    Age = donorDto.Age,
                    Ethnicity = donorDto.Ethnicity,
                    StreetAddress = donorDto.StreetAddress,
                    City = donorDto.City,
                    StateProvince = donorDto.StateProvince,
                    ZipPostalCode = donorDto.ZipPostalCode,
                    Country = donorDto.Country,
                    HasHealthConditions = donorDto.HasHealthConditions,
                    HealthConditionsDetails = donorDto.HealthConditionsDetails,
                    BloodType = donorDto.BloodType,
                    WillingnessToDonate = donorDto.WillingnessToDonate,
                    AgreedToIdVerification = donorDto.AgreedToIdVerification,
                    RegistrationDate = DateTime.UtcNow,
                    LastUpdated = DateTime.UtcNow,
                    IsActive = true
                };

                _context.Donors.Add(donor);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetDonor), new { id = donor.DonorId }, donor);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Internal server error",
                    error = ex.Message
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Donor>> GetDonor(int id)  // Changed return type to ActionResult<Donor>
        {
            var donor = await _context.Donors.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
            return Ok(donor);  // Wrapped in Ok() for proper ActionResult return
        }

        private async Task LogAudit(string actionType, string tableName, int recordId, string oldValues, Donor newValues)
        {
            var auditLog = new AuditLog
            {
                ActionType = actionType,
                TableName = tableName,
                RecordId = recordId,
                OldValues = oldValues,
                NewValues = System.Text.Json.JsonSerializer.Serialize(newValues),
                ChangedBy = "System",
                ChangeDate = DateTime.UtcNow
            };

            _context.AuditLogs.Add(auditLog);
            await _context.SaveChangesAsync();
        }
    }
}