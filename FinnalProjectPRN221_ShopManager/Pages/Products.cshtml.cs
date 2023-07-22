using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
using Newtonsoft.Json;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class ProductsModel : PageModel
    {
        private readonly ShopManagementContext _context;
        public List<Product> lstProductAddtoCart = new List<Product>();
        public List<Product> products = new List<Product>();
        public List<Category> categories = new List<Category>();
        public List<ProductImage> productsImage;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private List<OrderDetail> cartItems = new List<OrderDetail>();
        public IPagedList<Product> PagedProducts { get; set; }
        int pageSize = 2;

        public ProductsModel(ShopManagementContext context)
        {
            _context = context;
        }
        
        public void OnGet(int? CategoryId, int pageNumber)
        {
            categories = _context.Categories.ToList();
            productsImage = _context.ProductImages.ToList();
            if (CategoryId != null)
            {
                products = _context.Products.Include(x => x.Category).Where(x => x.CategoryId == CategoryId).ToList();
            }
            else
            {
                products = _context.Products.Include(x=>x.Category).ToList();
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
        
        public IActionResult OnPost(string? searchItem, int pageNumber, int? productId)
        {
            categories = _context.Categories.ToList();
            products = _context.Products.Include(x=>x.Category).Where(x=>x.ProductName.Contains(searchItem)).ToList();
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
            return Redirect("/Products/List");
        }
    }
}
