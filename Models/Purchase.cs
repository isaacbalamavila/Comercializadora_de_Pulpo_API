using System;
using System.Collections.Generic;

namespace comercializadora_de_pulpo_api.Models;

public partial class Purchase
{
    public Guid Id { get; set; }

    public string Sku { get; set; } = null!;

    public Guid SupplierId { get; set; }

    public int RawMaterialId { get; set; }

    public int TotalKg { get; set; }

    public decimal TotalPrice { get; set; }

    public decimal PriceKg { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual RawMaterial RawMaterial { get; set; } = null!;

    public virtual Supplier Supplier { get; set; } = null!;
}
