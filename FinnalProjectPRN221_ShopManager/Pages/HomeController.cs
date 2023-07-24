using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class HomeController : Controller
    {
        
        private readonly ShopManagementContext _context;
        public HomeController(ShopManagementContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AddToCart(int? productId)
        {
            // Add to cart
            var product = _context.Products.Find(productId);

            // Lấy danh sách sản phẩm đã được lưu trong session (nếu có)
            List<Product> cartProducts = new List<Product>();
            var cartProductsJson = HttpContext.Session.GetString("CartProducts");
            if (!string.IsNullOrEmpty(cartProductsJson))
            {
                cartProducts = JsonConvert.DeserializeObject<List<Product>>(cartProductsJson);
            }

            // Kiểm tra xem sản phẩm đã có trong giỏ hàng hay chưa
            var existingProduct = cartProducts.Find(p => p.ProductId == productId);
            if (existingProduct != null)
            {
                existingProduct.Quantity++;
            }
            else
            {
                // Nếu sản phẩm chưa có trong giỏ hàng, thêm sản phẩm mới vào giỏ hàng
                product.Quantity = 1;
                cartProducts.Add(product);
            }
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            var serializedJson = JsonConvert.SerializeObject(cartProducts, settings);
            // Lưu danh sách sản phẩm vào session sau khi đã thay đổi
            HttpContext.Session.SetString("CartProducts", serializedJson);
            _context.SetSessionValue("test1", "Session test");
            return View();
            
        }
    }
}
