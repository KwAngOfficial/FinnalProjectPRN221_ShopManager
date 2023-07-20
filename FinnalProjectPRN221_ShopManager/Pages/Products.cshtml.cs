using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ShopManagementContext _context;
        public ProductsModel(ShopManagementContext context)
        {
            _context = context;
        }
        public List<Product> lstProductAddtoCart = new List<Product>();
        public List<Product> products = new List<Product>();
        public List<Category> categories = new List<Category>();

        public void OnGet(int? CategoryId)
        {
            categories = _context.Categories.ToList();
            if (CategoryId != null)
            {
                products = _context.Products.Include(x => x.Category).Include(x => x.ProductImages).Where(x => x.CategoryId == CategoryId).ToList();
            }
            else
            {
                products = _context.Products.Include(x=>x.Category).Include(x=>x.ProductImages).ToList();
            }
        }

        private List<OrderDetail> cartItems = new List<OrderDetail>();
        public void OnPost(string name, double price)
        {
            Product p = _context.Products.Where(x => x.ProductName == name).FirstOrDefault(); 
            var newItem = new OrderDetail
            {
                OrderId = cartItems.Count + 1,
                ProductId = p.ProductId,
                Price = price,
            };

            //HttpContext.Session.
            //cartItems.Add(newItem);
        }
    }
}
