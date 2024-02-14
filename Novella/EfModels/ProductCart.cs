using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class ProductCart
{
    public int PkProductCartId { get; set; }

    public int FkCartId { get; set; }

    public int FkProductId { get; set; }

    public int QuantityInCart { get; set; }

    public virtual Cart FkCart { get; set; } = null!;

    public virtual Product FkProduct { get; set; } = null!;
}
