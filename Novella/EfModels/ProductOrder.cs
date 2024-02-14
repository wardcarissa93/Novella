using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class ProductOrder
{
    public int PkProductOrderId { get; set; }

    public int FkOrderId { get; set; }

    public int FkProductId { get; set; }

    public int QuantityInOrder { get; set; }

    public virtual Order FkOrder { get; set; } = null!;

    public virtual Product FkProduct { get; set; } = null!;
}
