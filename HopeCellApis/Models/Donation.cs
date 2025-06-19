using System;
using System.Collections.Generic;

namespace HopeCellApis.Models;

public partial class Donation
{
    public int DonationId { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Gender { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Country { get; set; }

    public string? Comment { get; set; }

    public string? SpecialAppeal { get; set; }

    public decimal Amount { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public bool IsAnonymous { get; set; }

    public string? TransactionId { get; set; }

    public string? PaymentStatus { get; set; }

    public DateTime DonationDate { get; set; }

    public virtual ICollection<PaymentResponse> PaymentResponses { get; set; } = new List<PaymentResponse>();
}
