using System;
using System.Collections.Generic;

namespace GameCenter.Models;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int LoyaltyLevel { get; set; }

    public virtual ICollection<Login> Logins { get; } = new List<Login>();

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}
