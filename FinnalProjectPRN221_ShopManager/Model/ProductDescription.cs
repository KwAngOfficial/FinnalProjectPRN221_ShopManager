using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class ProductDescription
{
    public int DescriptionId { get; set; }

    public int? ProductId { get; set; }

    public string? Detail { get; set; }

    public string? Description { get; set; }

    public virtual Product? Product { get; set; }
}
