using System;
using System.Collections.Generic;

namespace Novella.EfModels;

public partial class ImageStore
{
    public int? PkImageId { get; set; }

    public string FileName { get; set; } = null!;

    public byte[] Image { get; set; } = null!;

    public int? FkProductId { get; set; }

    public virtual Product FkProduct { get; set; } = null!;
}
