using System;
using System.Collections.Generic;

namespace GameCenter.Models;

public partial class Employee
{
    public string EmployeeId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public string Phonenumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public string JobTitle { get; set; } = null!;

    public virtual ICollection<EmployeeSchedule> EmployeeSchedules { get; } = new List<EmployeeSchedule>();

    public virtual ICollection<Login> Logins { get; } = new List<Login>();

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}
