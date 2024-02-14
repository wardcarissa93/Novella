using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class Category
{
    public int PkCategoryId { get; set; }

    public string CategoryName { get; set; } = null!;

    public string CategoryDescription { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
