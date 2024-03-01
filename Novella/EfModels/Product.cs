using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class Product
{
    public int? PkProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public string ProductDescription { get; set; } = null!;

    public int QuantityAvailable { get; set; }

    public int FkCategoryId { get; set; }

    public virtual Category FkCategory { get; set; } = null!;

    public virtual ICollection<ImageStore> ImageStores { get; set; } = new List<ImageStore>();

    public virtual ICollection<ProductCart> ProductCarts { get; set; } = new List<ProductCart>();

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
}
