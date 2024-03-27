using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class Order
{
    public int PkOrderId { get; set; }

    public string FkUserId { get; set; } = null!;

    public int? FkOrderStatusId { get; set; }

    public int FkShippingAddressId { get; set; }

    public int FkBillingAddressId { get; set; }

    public DateTime DateOrdered { get; set; }

    public string PaypalTransactionId { get; set; } = null!;

    public virtual Address FkBillingAddress { get; set; } = null!;

    public virtual OrderStatus FkOrderStatus { get; set; } = null!;

    public virtual Address FkShippingAddress { get; set; } = null!;

    public virtual UserAccount FkUser { get; set; } = null!;

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
