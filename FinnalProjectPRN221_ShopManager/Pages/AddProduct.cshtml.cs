using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class AddProductModel : PageModel
    {
        private readonly ShopManagementContext _context;
        public AddProductModel(ShopManagementContext context)
        {
            _context = context;
        }
        public List<Category> lstCategory;
        public List<Brand> lstBrand ;
        Product p;
        public List<Product> lstProduct;
        public void OnGet()
        {
            lstCategory=_context.Categories.ToList();
            lstBrand=_context.Brands.ToList();
        }
        public IActionResult OnPost(string? productName,string? price,string? quantity,int? categoryId,int? brandId)
        {
            p=new Product();
            p.ProductName = productName;
            p.Price = double.Parse(price);
            p.Quantity = int.Parse(quantity);
            p.CategoryId = categoryId;
            p.BrandId = brandId;
            _context.Products.Add(p);
            _context.SaveChanges();
            lstProduct = _context.Products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.ProductImages).ToList();
            return RedirectToPage("ProductsManager");
        }
    }
}
