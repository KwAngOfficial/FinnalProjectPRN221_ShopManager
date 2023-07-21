using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class ImportProduct
{
    public int ImportId { get; set; }

    public int? AccountId { get; set; }

    public int? SupplierId { get; set; }

    public DateTime? ImportDate { get; set; }

    public double? TotalPrice { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<ImportProductDetail> ImportProductDetails { get; set; } = new List<ImportProductDetail>();

    public virtual Supplier? Supplier { get; set; }
}
