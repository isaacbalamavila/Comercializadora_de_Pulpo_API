using System;
using System.Collections.Generic;

namespace comercializadora_de_pulpo_api.Models;

public partial class Unit
{
    public int Id { get; set; }

    public string Abbreviation { get; set; } = null!;

    public string? Label { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
