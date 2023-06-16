using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class Account
{
    public int AccountId { get; set; }

    public string? AccountName { get; set; }

    public string? Password { get; set; }

    public string? Name { get; set; }

    public int? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public int? RoleId { get; set; }

    public virtual ICollection<ImportProduct> ImportProducts { get; set; } = new List<ImportProduct>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role? Role { get; set; }
}
