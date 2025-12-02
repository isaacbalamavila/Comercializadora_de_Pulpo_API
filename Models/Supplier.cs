using System;
using System.Collections.Generic;

namespace comercializadora_de_pulpo_api.Models;

public partial class Supplier
{
    public Guid Id { get; set; }

    public string Name { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Rfc { get; set; }

    public string? AltPhone { get; set; }

    public string? AltEmail { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }

    public virtual ICollection<Purchase> Purchases { get; set; } = new List<Purchase>();
}
