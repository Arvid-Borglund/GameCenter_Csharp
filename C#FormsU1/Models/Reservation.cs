using System;
using System.Collections.Generic;

namespace GameCenter.Models;

public partial class Reservation
{
    public string ComputerId { get; set; } = null!;

    public DateTime TimeDate { get; set; }

    public string? CustomerId { get; set; } 

    public string? EmployeeId { get; set; } 

    public virtual Computer Computer { get; set; } = null!;

    public virtual Customer Customer { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
