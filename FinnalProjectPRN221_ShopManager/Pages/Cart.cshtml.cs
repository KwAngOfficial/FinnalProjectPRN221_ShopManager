using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class CartModel : PageModel
    {


        private readonly ShopManagementContext _context;


        public CartModel(ShopManagementContext context)
        {
            _context = context;
        }

        public List<Product> CartProducts { get; set; }

        public void OnGet()
        {
            // Lấy danh sách sản phẩm trong giỏ hàng từ session
            var cartProductsJson =_context.GetSessionValue("CartProducts");
            string test= _context.GetSessionValue("test");
            string test1 = HttpContext.Session.GetString("test1");
            if (!string.IsNullOrEmpty(cartProductsJson))
            {
                // Deserialize chuỗi JSON thành danh sách sản phẩm
                CartProducts = JsonConvert.DeserializeObject<List<Product>>(cartProductsJson);
            }
            else
            {
                // Nếu chuỗi JSON không tồn tại hoặc không có dữ liệu, khởi tạo danh sách sản phẩm rỗng
                CartProducts = new List<Product>();
            }
        }

        //public IActionResult OnPostAddToCart(int productId)
        //{// Add to cart
        //    var product = _context.Products.Find(productId);

        //    // Lấy danh sách sản phẩm đã được lưu trong session (nếu có)
        //    List<Product> cartProducts = new List<Product>();
        //    var cartProductsJson = _httpContextAccessor.HttpContext.Session.GetString("CartProducts");
        //    if (!string.IsNullOrEmpty(cartProductsJson))
        //    {
        //        cartProducts = JsonConvert.DeserializeObject<List<Product>>(cartProductsJson);
        //    }

        //    // Kiểm tra xem sản phẩm đã có trong giỏ hàng hay chưa
        //    var existingProduct = cartProducts.Find(p => p.ProductId == productId);
        //    if (existingProduct != null)
        //    {
        //        existingProduct.Quantity++;
        //    }
        //    else
        //    {
        //        // Nếu sản phẩm chưa có trong giỏ hàng, thêm sản phẩm mới vào giỏ hàng
        //        product.Quantity = 1;
        //        cartProducts.Add(product);
        //    }

        //    // Lưu danh sách sản phẩm vào session sau khi đã thay đổi
        //    _httpContextAccessor.HttpContext.Session.SetString("CartProducts", JsonConvert.SerializeObject(cartProducts));
        //    HttpContext.Session.SetString("test1", "Session test");

        //    // Trả về kết quả thành công
        //    return new JsonResult("Success");
        //}

    }
}
