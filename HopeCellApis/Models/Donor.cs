using System;
using System.Collections.Generic;

namespace HopeCellApis.Models;

public partial class Donor
{
    public int DonorId { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public int Age { get; set; }

    public string? Ethnicity { get; set; }

    public string StreetAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string StateProvince { get; set; } = null!;

    public string ZipPostalCode { get; set; } = null!;

    public string Country { get; set; } = null!;

    public bool HasHealthConditions { get; set; }

    public string? HealthConditionsDetails { get; set; }

    public string? BloodType { get; set; }

    public string WillingnessToDonate { get; set; } = null!;

    public bool AgreedToIdVerification { get; set; }

    public DateTime RegistrationDate { get; set; }

    public DateTime LastUpdated { get; set; }

    public bool IsActive { get; set; }
}
