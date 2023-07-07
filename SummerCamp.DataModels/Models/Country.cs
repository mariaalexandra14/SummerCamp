using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class Country
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Flag { get; set; }

    public virtual ICollection<Coach> Coaches { get; set; } = new List<Coach>();

    public virtual ICollection<Player> Players { get; set; } = new List<Player>();
}
