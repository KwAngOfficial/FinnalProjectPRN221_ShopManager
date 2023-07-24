using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class ProductsManagerModel : PageModel
    {
        private readonly ShopManagementContext _context;
        public ProductsManagerModel(ShopManagementContext context)
        {
            _context = context;
        }
        public List<Product> lstProduct;
        public void OnGet(string? productName)
        {
            
            if (productName != null)
            {
                lstProduct = _context.Products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.ProductImages).Where(x => x.ProductName.Contains(productName)).ToList();

            }
            else
            {
                lstProduct = _context.Products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.ProductImages).ToList();
            }
            
        }
        public IActionResult OnPost(string? productName, string? action,int? productId)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            switch (action)
            {
                case "edit":
                    if (productId != null)
                    {
                        Product productEdit = _context.Products.Where(x=>x.ProductId == productId).FirstOrDefault();
                        HttpContext.Session.SetString("productEdit", JsonConvert.SerializeObject(productEdit,settings));
                        
                    }
                    return RedirectToPage("EditProduct");

                    break;
                case "delete":
                    if (productId != null)
                    {
                        List<OrderDetail> orderDetail = _context.OrderDetails.Where(x => x.ProductId == productId).ToList();
                        _context.OrderDetails.RemoveRange(orderDetail);
                        List<ImportProductDetail> impProductDetail = _context.ImportProductDetails.Where(x => x.ProductId == productId).ToList();
                        _context.ImportProductDetails.RemoveRange(impProductDetail);
                        List<ProductImage> productImage = _context.ProductImages.Where(x => x.ProductId == productId).ToList();
                        _context.ProductImages.RemoveRange(productImage);
                        Product productEdit = _context.Products.Where(x => x.ProductId == productId).FirstOrDefault();
                        _context.Products.Remove(productEdit);
                        _context.SaveChanges();
                    }
                    break;

                default: return Page();
            }
            if (productName != null)
            {
                lstProduct = _context.Products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.ProductImages).Where(x => x.ProductName.Contains(productName)).ToList();

            }
            else
            {
                lstProduct = _context.Products.Include(x => x.Category).Include(x => x.Brand).Include(x => x.ProductImages).ToList();
            }
            return Page();
        }
    }

}
