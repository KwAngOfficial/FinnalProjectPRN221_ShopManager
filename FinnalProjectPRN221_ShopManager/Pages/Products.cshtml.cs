using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
            _httpContextAccessor = httpContextAccessor;
        }
        public List<Product> lstProductAddtoCart = new List<Product>();
        public List<Product> products = new List<Product>();
        public List<Category> categories = new List<Category>();
        private readonly IHttpContextAccessor _httpContextAccessor;

        public void OnGet(int? CategoryId)
        {
            categories = _context.Categories.ToList();
            if (CategoryId != null)
            {
                products = _context.Products.Include(x => x.ProductImages).Where(x => x.CategoryId == CategoryId).ToList();
            }
            else
            {
                products = _context.Products.Include(x=>x.ProductImages).ToList();
            }
        }
        public IActionResult OnPost(string searchItem)
        {
            categories = _context.Categories.ToList();

            if (!string.IsNullOrEmpty(searchItem))
            {
                products = _context.Products
                    .Include(x => x.Category)
                    .Where(x => x.ProductName.Contains(searchItem))
                    .ToList();
            }
            else
            {
                products = _context.Products
                    .Include(x => x.Category)
                    .ToList();
            }

            return Page();
        }
        private List<OrderDetail> cartItems = new List<OrderDetail>();
        public void OnGetSearch(string? searchItem)
        {
            categories = _context.Categories.ToList();
            products = _context.Products.Include(x=>x.Category).Where(x=>x.ProductName.Contains(searchItem)).ToList();
            //Product p = _context.Products.Where(x => x.ProductName == name).FirstOrDefault(); 
            //var newItem = new OrderDetail
            //{
            //    OrderId = cartItems.Count + 1,
            //    ProductId = p.ProductId,
            //    Price = price,
            //};
        }
        public IActionResult OnPostAddToCart(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            // Lấy danh sách sản phẩm đã được lưu trong session (nếu có)
            List<Product> cartProducts = _httpContextAccessor.HttpContext.Session.Get<List<Product>>("CartProducts") ?? new List<Product>();

            // Kiểm tra nếu sản phẩm đã có trong giỏ hàng, thì tăng số lượng
            var existingProduct = cartProducts.Find(p => p.ProductId == productId);
            if (existingProduct != null)
            {
                existingProduct.Quantity++;
            }
            else
            {
                // Nếu sản phẩm chưa có trong giỏ hàng, thêm sản phẩm mới
                product.Quantity = 1;
                cartProducts.Add(product);
            }

            // Lưu danh sách sản phẩm vào session
            _httpContextAccessor.HttpContext.Session.Set("CartProducts", cartProducts);

            // Trả về kết quả thành công
            return new JsonResult("Success");
        }
    }
}
