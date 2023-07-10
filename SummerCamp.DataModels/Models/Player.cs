using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class Player
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public DateTime? BirthDate { get; set; }

    public string? Address { get; set; }

    public int? Position { get; set; }

    public int? ShirtNumber { get; set; }

    public int? TeamId { get; set; }

    public int? CountryId { get; set; }

    public string? Picture { get; set; }

    public virtual Country? Country { get; set; }

    public virtual Team? Team { get; set; }
}
