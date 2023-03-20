using System;
using System.Collections.Generic;

namespace GameCenter.Models;

public partial class Computer
{
    public string ComputerId { get; set; } = null!;

    public string Cpu { get; set; } = null!;

    public string Gpu { get; set; } = null!;

    public int Ram { get; set; }

    public string DataStorage { get; set; } = null!;

    public bool Reserved { get; set; }

    public virtual ICollection<Game> Games { get; } = new List<Game>();

    public virtual ICollection<Reservation> Reservations { get; } = new List<Reservation>();
}
