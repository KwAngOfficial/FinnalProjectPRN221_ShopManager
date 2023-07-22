using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Http;

namespace FinnalProjectPRN221_ShopManager.Hubs
{
    public class ProductHubs : Hub
    {
        private ShopManagementContext _context;

        public ProductHubs(ShopManagementContext context)
        {
            _context = context;
        }
        List<Product> _products = new List<Product>();
        public async Task AddToCart(int? id)
        {
            if (id != null)
            {
                Product product = _context.Products.FirstOrDefault(x => x.ProductId == id);
                if (product != null)
                {
                    var newItem = new OrderDetail
                    {
                        OrderDetail1 = _context.OrderDetails.Count() + 1,
                        OrderId = 1 ,
                        ProductId = product.ProductId,
                        Price = product.Price,
                    };
                    
                    _context.OrderDetails.Add(newItem);
                    _context.SaveChanges();
                }
            }

            await Clients.All.SendAsync("ProductAddToCart",id);
        }
    }
}
