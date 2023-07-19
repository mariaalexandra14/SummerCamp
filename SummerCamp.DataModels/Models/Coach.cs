﻿using System;
using System.Collections.Generic;

namespace SummerCamp.DataModels.Models;

public partial class Coach
{
    public int Id { get; set; }

    public string? FullName { get; set; }

    public int? Age { get; set; }

    public string? Picture { get; set; }

    public int? CountryId { get; set; }

    public virtual Country? Country { get; set; }

    public virtual ICollection<Team> Teams { get; set; } = new List<Team>();
}
