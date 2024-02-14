using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class Cart
{
    public int PkCartId { get; set; }

    public string FkUserId { get; set; } = null!;

    public virtual UserAccount FkUser { get; set; } = null!;

    public virtual ICollection<ProductCart> ProductCarts { get; set; } = new List<ProductCart>();
}
