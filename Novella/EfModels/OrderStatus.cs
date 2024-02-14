using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class OrderStatus
{
    public int PkOrderStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public string StatusDescription { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
