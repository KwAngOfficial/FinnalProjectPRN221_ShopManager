using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public int? AccountId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? Name { get; set; }

    public int? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public double? TotalPrice { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
