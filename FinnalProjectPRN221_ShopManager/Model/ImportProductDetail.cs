using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class ImportProductDetail
{
    public int ImportProductDetailId { get; set; }

    public int? ImportProductId { get; set; }

    public int? ProductId { get; set; }

    public int? UnitId { get; set; }

    public int? Price { get; set; }

    public int? Quantity { get; set; }

    public double? TotalPrice { get; set; }

    public virtual ImportProduct? ImportProduct { get; set; }

    public virtual Product? Product { get; set; }

    public virtual Unit? Unit { get; set; }
}
