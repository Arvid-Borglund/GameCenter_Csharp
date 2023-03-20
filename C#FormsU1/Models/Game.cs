using System;
using System.Collections.Generic;

namespace GameCenter.Models;

public partial class Game
{
    public string GameId { get; set; } = null!;

    public string ComputerId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Genre { get; set; } = null!;

    public virtual Computer Computer { get; set; } = null!;
}
