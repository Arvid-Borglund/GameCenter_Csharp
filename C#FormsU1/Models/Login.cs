using System;
using System.Collections.Generic;

namespace GameCenter.Models;

public partial class Login
{
    public string LoginId { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? CustomerId { get; set; }

    public string? EmployeeId { get; set; }

    public string AccessLevel { get; set; } = null!;

    public virtual Customer? Customer { get; set; }

    public virtual Employee? Employee { get; set; }
}
