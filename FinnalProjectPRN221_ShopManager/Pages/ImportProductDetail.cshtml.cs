using FinnalProjectPRN221_ShopManager.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FinnalProjectPRN221_ShopManager.Pages
{
    public class ImportProductDetailModel : PageModel
    {
        private readonly ShopManagementContext _context;
        public ImportProductDetailModel(ShopManagementContext context)
        {
            _context = context;
        }
        public List<ImportProductDetail> impDetail;
        public void OnGet()
        {
            impDetail = _context.ImportProductDetails.Include(x=>x.Product).Include(x=>x.ImportProduct).ToList();
        }
    }
}
