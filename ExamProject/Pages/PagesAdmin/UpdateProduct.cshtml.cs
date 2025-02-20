using ExamProject.Data.ShopDb;
using ExamProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace ExamProject.Pages.PagesAdmin
{
    [Authorize(Roles = "Admin")]
    public class UpdateProductModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public UpdateProductModel (ApplicationDbContext context) {
            this.context = context;
        }
        [BindProperty]
        public Product? Product { get; set; }


        public IEnumerable<SelectListItem> ListCategories { get; set; }
        public IEnumerable<SelectListItem> ListDescription { get; set; }

        public async Task<IActionResult> OnGet (int id) {

            Product = await context.Products.FindAsync(id);
            if (Product == null) 
                return NotFound();
            ListCategories = context.Categories.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
            ListDescription = context.Description.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
            return Page();
        }

        public async Task<IActionResult> OnPost (int id) {
      
            ListCategories = context.Categories.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
            ListDescription = context.Description.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
            if (ListCategories == null && ListDescription == null) {
                return Page();
            }
            Product.Id = id;

            context.Products.Update(Product);
            await context.SaveChangesAsync();
            
            return RedirectToPage("/Index");
        }
    }
}
