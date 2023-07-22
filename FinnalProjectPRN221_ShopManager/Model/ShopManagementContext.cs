using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace FinnalProjectPRN221_ShopManager.Model;

public partial class ShopManagementContext : DbContext
{
    public ShopManagementContext()
    {
    }

    public ShopManagementContext(DbContextOptions<ShopManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<CategoryType> CategoryTypes { get; set; }

    public virtual DbSet<ImportProduct> ImportProducts { get; set; }

    public virtual DbSet<ImportProductDetail> ImportProductDetails { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductDescription> ProductDescriptions { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Unit> Units { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
                                              .SetBasePath(Directory.GetCurrentDirectory())
                                              .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        IConfigurationRoot configuration = builder.Build();
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("MyComDB"));
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__ACCOUNT__349DA58603D3219A");

            entity.ToTable("ACCOUNT");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.AccountName).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.Password).HasMaxLength(250);

            entity.HasOne(d => d.Role).WithMany(p => p.Accounts)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__ACCOUNT__RoleId__412EB0B6");
        });

        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.BannerId).HasName("PK__BANNER__32E86AD1836C4230");

            entity.ToTable("BANNER");

            entity.Property(e => e.BannerId).ValueGeneratedNever();
            entity.Property(e => e.Image).HasColumnType("image");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.BrandId).HasName("PK__Brand__DAD4F05EF92D15EB");

            entity.ToTable("Brand");

            entity.Property(e => e.BrandId).ValueGeneratedNever();
            entity.Property(e => e.BrandName).HasMaxLength(100);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A0B33735D7E");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).ValueGeneratedNever();
            entity.Property(e => e.CategoryName).HasMaxLength(50);

            entity.HasOne(d => d.CategoryType).WithMany(p => p.Categories)
                .HasForeignKey(d => d.CategoryTypeId)
                .HasConstraintName("FK__Category__Catego__4222D4EF");
        });

        modelBuilder.Entity<CategoryType>(entity =>
        {
            entity.HasKey(e => e.CategoryTypeId).HasName("PK__Category__7B30E7A32ACB2A75");

            entity.ToTable("CategoryType");

            entity.Property(e => e.CategoryTypeId).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<ImportProduct>(entity =>
        {
            entity.HasKey(e => e.ImportId).HasName("PK__ImportPr__869767EAB5B1D01C");

            entity.ToTable("ImportProduct");

            entity.Property(e => e.ImportId).ValueGeneratedNever();
            entity.Property(e => e.ImportDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Account).WithMany(p => p.ImportProducts)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__ImportPro__Accou__4316F928");

            entity.HasOne(d => d.Supplier).WithMany(p => p.ImportProducts)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("FK__ImportPro__Suppl__440B1D61");
        });

        modelBuilder.Entity<ImportProductDetail>(entity =>
        {
            entity.HasKey(e => e.ImportProductDetailId).HasName("PK__ImportPr__AA7713A6633FB6B2");

            entity.ToTable("ImportProductDetail");

            entity.Property(e => e.ImportProductDetailId).ValueGeneratedNever();

            entity.HasOne(d => d.ImportProduct).WithMany(p => p.ImportProductDetails)
                .HasForeignKey(d => d.ImportProductId)
                .HasConstraintName("FK__ImportPro__Impor__44FF419A");

            entity.HasOne(d => d.Product).WithMany(p => p.ImportProductDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ImportPro__Produ__45F365D3");

            entity.HasOne(d => d.Unit).WithMany(p => p.ImportProductDetails)
                .HasForeignKey(d => d.UnitId)
                .HasConstraintName("FK__ImportPro__UnitI__46E78A0C");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Order__C3905BCF11182060");

            entity.ToTable("Order");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.Address).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(100);
            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Account).WithMany(p => p.Orders)
                .HasForeignKey(d => d.AccountId)
                .HasConstraintName("FK__Order__AccountId__47DBAE45");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetail1).HasName("PK__OrderDet__452E175FCCA370E8");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.OrderDetail1)
                .ValueGeneratedNever()
                .HasColumnName("OrderDetail");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .HasConstraintName("FK__OrderDeta__Order__48CFD27E");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__OrderDeta__Produ__49C3F6B7");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6CD9A332FE9");

            entity.ToTable("Product");

            entity.Property(e => e.ProductName).HasMaxLength(50);

            entity.HasOne(d => d.Brand).WithMany(p => p.Products)
                .HasForeignKey(d => d.BrandId)
                .HasConstraintName("FK__Product__BrandId__4AB81AF0");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__Product__Categor__4BAC3F29");
        });

        modelBuilder.Entity<ProductDescription>(entity =>
        {
            entity.HasKey(e => e.DescriptionId).HasName("PK__ProductD__A58A9F8BD1DED1A6");

            entity.ToTable("ProductDescription");

            entity.Property(e => e.DescriptionId).ValueGeneratedNever();
            entity.Property(e => e.Description).HasMaxLength(100);
            entity.Property(e => e.Detail).HasMaxLength(50);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductDescriptions)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductDe__Produ__4CA06362");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("PK__ProductI__7516F70CB2E066B7");

            entity.ToTable("ProductImage");

            entity.Property(e => e.ImageId).ValueGeneratedNever();
            entity.Property(e => e.ImageProduct)
                .HasMaxLength(250)
                .IsFixedLength();

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__ProductIm__Produ__4D94879B");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A688D81D8");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__Supplier__4BE666B4A2315100");

            entity.ToTable("Supplier");

            entity.Property(e => e.SupplierId).ValueGeneratedNever();
            entity.Property(e => e.SupplierName).HasMaxLength(100);
        });

        modelBuilder.Entity<Unit>(entity =>
        {
            entity.HasKey(e => e.UnitId).HasName("PK__Unit__44F5ECB5C40BC0C3");

            entity.ToTable("Unit");

            entity.Property(e => e.UnitId).ValueGeneratedNever();
            entity.Property(e => e.UnitName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }
    private readonly IHttpContextAccessor _httpContextAccessor;
    public ShopManagementContext(DbContextOptions<ShopManagementContext> options, IHttpContextAccessor httpContextAccessor)
           : base(options)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public string GetSessionValue(string key)
    {
        return _httpContextAccessor.HttpContext.Session.GetString(key);
    }

    // Custom phương thức để lưu thông tin vào session
    public void SetSessionValue(string key, string value)
    {
        _httpContextAccessor.HttpContext.Session.SetString(key, value);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
