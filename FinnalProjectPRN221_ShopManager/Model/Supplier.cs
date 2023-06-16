using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class Supplier
{
    public int SupplierId { get; set; }

    public string? SupplierName { get; set; }

    public virtual ICollection<ImportProduct> ImportProducts { get; set; } = new List<ImportProduct>();
}
