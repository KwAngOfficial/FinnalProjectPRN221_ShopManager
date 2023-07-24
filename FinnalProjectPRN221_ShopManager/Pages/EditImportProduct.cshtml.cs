using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class EditImportProductModel : PageModel
    {
        private readonly ShopManagementContext _context;


        public EditImportProductModel(ShopManagementContext context)
        {
            _context = context;
        }
        public List<ImportProduct> imp;
        public List<ImportProduct> Imports;
        public void OnGet()
        {
            var productEditJson = HttpContext.Session.GetString("productImport");
            if (productEditJson != null)
            {
                var ProductJson = JsonConvert.DeserializeObject<ImportProduct>(productEditJson);
            }
             imp= _context.ImportProducts.Include(x=>x.Account).Include(x=>x.Supplier).ToList();
        }
        public IActionResult OnPost(string? importDate,string? totalPrice,int? supplierId,int? accountId)
        {
            ImportProduct p=new ImportProduct();
            p.SupplierId= supplierId;
            p.AccountId= accountId;
            p.TotalPrice=int.Parse(totalPrice);
            p.ImportDate = DateTime.Parse(importDate);
            _context.ImportProducts.Update(p);
            _context.SaveChanges();
            Imports=_context.ImportProducts.Include(x=>x.Supplier).Include(x=>x.Account).ToList();
            return RedirectToPage("Index");
        }
    }
}
