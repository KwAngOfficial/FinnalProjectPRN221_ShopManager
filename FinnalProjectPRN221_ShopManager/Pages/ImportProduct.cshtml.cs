using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class ImportProductModel : PageModel
    {
        private readonly ShopManagementContext _context;
        public ImportProductModel(ShopManagementContext context)
        {
            _context = context;
        }
        public List<Supplier> Suppliers;
        public List<Unit> units;
        public void OnGet()
        {
            Suppliers = _context.Suppliers.ToList();
            units = _context.Units.ToList();
        }
        public IActionResult OnPost(string? Id, string? date, string? ProductId, string? toltalPrice, string? quantity, string? supplierId, string? unitId)
        {
            ImportProductDetail imp = new ImportProductDetail();
            imp.ImportProductDetailId = _context.ImportProductDetails.Count() + 1;
            imp.ImportProductId = int.Parse(Id);
            imp.ProductId = int.Parse(ProductId);
            imp.Price = int.Parse(toltalPrice);
            imp.Quantity = int.Parse(quantity);
            imp.UnitId = int.Parse(unitId);
            imp.TotalPrice = int.Parse(toltalPrice) * int.Parse(quantity);
            _context.ImportProductDetails.Add(imp);
            _context.SaveChanges();
            return RedirectToPage("Index");
        }
    }
}
