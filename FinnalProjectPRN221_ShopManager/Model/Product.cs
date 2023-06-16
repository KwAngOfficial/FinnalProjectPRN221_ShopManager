using System;
using System.Collections.Generic;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class Product
{
    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public int? BrandId { get; set; }

    public string? ProductName { get; set; }

    public double? Price { get; set; }

    public int? Quantity { get; set; }

    public virtual Brand? Brand { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<ImportProductDetail> ImportProductDetails { get; set; } = new List<ImportProductDetail>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual ICollection<ProductDescription> ProductDescriptions { get; set; } = new List<ProductDescription>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}
