using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Newtonsoft.Json;
using Microsoft.CodeAnalysis;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ShopManagementContext _context;
        public List<Product> lstProductAddtoCart = new List<Product>();
        public List<Product> products = new List<Product>();
        public List<Category> categories = new List<Category>();
        
        private readonly IHttpContextAccessor _httpContextAccessor;
        private List<OrderDetail> cartItems = new List<OrderDetail>();
        public IPagedList<Product> PagedProducts { get; set; }
        int pageSize = 6;

        public ProductsModel(ShopManagementContext context)
        {
            _context = context;
        }
        
        public void OnGet(int? CategoryId, int pageNumber)
        {
            categories = _context.Categories.ToList();
            
            if (CategoryId != null)
            {
                products = _context.Products.Include(x => x.Category).Include(x=>x.ProductImages).Where(x => x.CategoryId == CategoryId).ToList();
            }
            else
            {
                products = _context.Products.Include(x=>x.Category).Include(x => x.ProductImages).ToList();
            }
            
            // Thêm chức năng phân trang
            if (pageNumber != 0)
            {
                // Số sản phẩm hiển thị trên mỗi trang
                PagedProducts = products.ToPagedList(pageNumber, pageSize);
            }
            else
            {
                PagedProducts = products.ToPagedList(1, pageSize);
            }
            
        }
        
        public void OnPost(string? searchItem, int pageNumber, int? productId)
        {
            categories = _context.Categories.ToList();
            if(searchItem!= null)
            {
                products = _context.Products.Include(x => x.Category).Include(x => x.ProductImages).Where(x => x.ProductName.Contains(searchItem)).ToList();

            }
            else
            {
                products = _context.Products.Include(x => x.Category).Include(x => x.ProductImages).ToList();

            }
            // Thêm chức năng phân trang
            if (pageNumber != 0)
                {
                    // Số sản phẩm hiển thị trên mỗi trang
                    PagedProducts = products.ToPagedList(pageNumber, pageSize);
                }
                else
                {
                    PagedProducts = products.ToPagedList(1, pageSize);
                }
            
                Product product=new Product();
                // Find the product by productId
                 product = _context.Products.Find(productId);
                // Get the current cart from session (if exists)
                List<Product> cartProducts = new List<Product>();
                var cartProductsJson = HttpContext.Session.GetString("CartProducts");
                if (!string.IsNullOrEmpty(cartProductsJson))
                {
                    cartProducts = JsonConvert.DeserializeObject<List<Product>>(cartProductsJson);
                }

                // Check if the product already exists in the cart
                var existingProduct = cartProducts.Find(p => p.ProductId == productId);
                if (existingProduct != null)
                {
                    existingProduct.Quantity++;
                }
                else
                {
                product.Quantity = 1;
                    // If the product does not exist in the cart, add it with a quantity of 1
                    
                    cartProducts.Add(product);
                }

                // Serialize the cart products to JSON and save it in the session
                var settings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                };
                var serializedJson = JsonConvert.SerializeObject(cartProducts, settings);
                HttpContext.Session.SetString("CartProducts", serializedJson);
            Redirect("/Products/List");
            

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
            return RedirectToPage("/Products/List");

        }

    }
}
