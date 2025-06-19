using System;
using System.Collections.Generic;

namespace HopeCellApis.Models;

public partial class PaymentResponse
{
    public int ResponseId { get; set; }

    public int? DonationId { get; set; }

    public string? PaymentUrl { get; set; }

    public string? TransactionId { get; set; }

    public string? Status { get; set; }

    public DateTime? ResponseDate { get; set; }

    public virtual Donation? Donation { get; set; }
}
