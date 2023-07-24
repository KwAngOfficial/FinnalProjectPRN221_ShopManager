using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class IndexModel : PageModel
    {
        
        public IList<ImportProduct> Imports { get; set; }
        private readonly ShopManagementContext _context;
        public IndexModel(ShopManagementContext context)
        {
            _context = context;
        }
        
        public async Task OnGetAsync()
        {
            Imports = await _context.ImportProducts
                .Include(i => i.Supplier)
                .Include(i => i.Account)
                .ToListAsync();
        }
        public IActionResult OnPost(string? action, int? importId)
        {
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            switch (action)
            {
                case "add":
                    return RedirectToPage("ImportProduct");
                    break;

                case "edit":
                    if (importId != null)
                    {
                        ImportProduct productImport = _context.ImportProducts.Where(x => x.ImportId == importId).FirstOrDefault();
                        HttpContext.Session.SetString("productImport", JsonConvert.SerializeObject(productImport, settings));

                    }
                    return RedirectToPage("EditImportProduct");
                    break;
                

                default: return Page();
            }
            return Page();
        }
    }
}
