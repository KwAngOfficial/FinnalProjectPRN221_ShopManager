using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class CategoryType
{
    public int CategoryTypeId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
}
