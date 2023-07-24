using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class EditProductModel : PageModel
    {
        private readonly ShopManagementContext _context;
        public EditProductModel(ShopManagementContext context)
        {
            _context = context;
        }
        
        public Product ProductJson;
        public List<Category> CategoriesEdit;
        public List<Brand> BrandEdit;
        public List<Product> lstProduct;
        public void OnGet()
        {
            CategoriesEdit=_context.Categories.ToList();
            BrandEdit=_context.Brands.ToList();
            var productEditJson = HttpContext.Session.GetString("productEdit");
            if (productEditJson != null)
            {
                ProductJson = JsonConvert.DeserializeObject<Product>(productEditJson);
            }
        }
        public IActionResult OnPost(int? productId, string? productName, string? price,string? quantity,string? categoryId, string? brandId, string? productImage) {
            
            Product pEdit = _context.Products.Include(x => x.Category).Include(x => x.Brand).FirstOrDefault(x => x.ProductId == productId);
            if(pEdit != null)
            {
                pEdit.ProductName = productName;
                pEdit.Price = double.Parse(price);
                pEdit.Quantity=int.Parse(quantity);
                _context.ProductImages.FirstOrDefault(p => p.ProductId == pEdit.ProductId).ImageProduct = productImage;
                pEdit.CategoryId =int.Parse(categoryId);
                pEdit.BrandId=int.Parse(brandId);

                _context.Products.Update(pEdit);
                _context.SaveChanges();
            }
            lstProduct = _context.Products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.ProductImages).ToList();
            return RedirectToPage("ProductsManager");
        }
    }
}
