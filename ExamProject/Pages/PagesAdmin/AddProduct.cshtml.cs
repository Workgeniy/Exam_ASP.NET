using ExamProject.Data;
using ExamProject.Data.ShopDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamProject.Pages.PagesAdmin
{
    [Authorize(Roles = "Admin")]
    public class AddProductModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public AddProductModel (ApplicationDbContext context) {
            this.context = context;
        }
        [BindProperty]
        public Product Product { get; set; }

        public IEnumerable<SelectListItem> ListCategories { get; set; }
        public IEnumerable<SelectListItem> ListDescription { get; set; }

        public void OnGet()
        {
            ListCategories = context.Categories.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name}).ToList();
            ListDescription = context.Description.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
        }

        public async Task<IActionResult> OnPost () {
            if (!ModelState.IsValid) {
                ListCategories = context.Categories.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
                ListDescription = context.Description.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();

                return Page();
            }

            context.Products.Add(Product);
            await context.SaveChangesAsync();

            return RedirectToPage("//Index");
        }
    }
}
