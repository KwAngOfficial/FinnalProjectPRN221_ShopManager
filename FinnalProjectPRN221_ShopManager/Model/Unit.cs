using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class Unit
{
    public int UnitId { get; set; }

    public string? UnitName { get; set; }

    public virtual ICollection<ImportProductDetail> ImportProductDetails { get; set; } = new List<ImportProductDetail>();
}
