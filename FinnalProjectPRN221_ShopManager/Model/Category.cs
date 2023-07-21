using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class Category
{
    public int CategoryId { get; set; }

    public int? CategoryTypeId { get; set; }

    public string? CategoryName { get; set; }

    public virtual CategoryType? CategoryType { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
