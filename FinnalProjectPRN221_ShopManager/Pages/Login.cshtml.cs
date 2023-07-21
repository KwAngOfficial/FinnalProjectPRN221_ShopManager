using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class LoginModel : PageModel
    {
        private ShopManagementContext _context;

        public LoginModel(ShopManagementContext context)
        {
            _context = context;
        }
      
        public void OnGet(int idProduct = 0)
        {
            if (idProduct != 0)
            {
                    Product product = _context.Products.FirstOrDefault(x => x.ProductId == idProduct);
                    if (product != null)
                    {
                        var newItem = new OrderDetail
                        {
                            OrderDetail1 = _context.OrderDetails.Count() + 1,
                            OrderId = 1,
                            ProductId = product.ProductId,
                            Price = product.Price,
                        };

                        _context.OrderDetails.Add(newItem);
                        _context.SaveChanges();
                    }
            }
        }
    }
}
