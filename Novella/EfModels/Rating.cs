using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class Rating
{
    public int? PkRatingId { get; set; }

    public int FkProductId { get; set; }

    public string FkUserId { get; set; } = null!;

    public string? Review { get; set; }

    public decimal RatingValue { get; set; }

    public DateTime DateRated { get; set; }

    public virtual Product FkProduct { get; set; } = null!;

    public virtual UserAccount FkUser { get; set; } = null!;
}
