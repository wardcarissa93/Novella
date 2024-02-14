using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class Address
{
    public int PkAddressId { get; set; }

    public string AddressLineOne { get; set; } = null!;

    public string? AddressLineTwo { get; set; }

    public string City { get; set; } = null!;

    public string Province { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public virtual ICollection<Order> OrderFkBillingAddresses { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderFkShippingAddresses { get; set; } = new List<Order>();
}
