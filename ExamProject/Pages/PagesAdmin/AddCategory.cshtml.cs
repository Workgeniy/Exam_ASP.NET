using ExamProject.Data;
using ExamProject.Data.ShopDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExamProject.Pages.PagesAdmin
{
    [Authorize(Roles = "Admin")]
    public class AddCategoryModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public AddCategoryModel (ApplicationDbContext context) {
            this.context = context;
        }

        [BindProperty]
        public Category Category { get; set; }

        public static IList<string> ListCategories { get; set; }

        public void OnGet()
        {
            ListCategories = context.Categories.Select(item => item.Name).ToList();
        }

        public async Task<IActionResult> OnPost () { 
            if (ListCategories.Contains(Category.Name)) {
                ModelState.AddModelError("Category.Name",
           "Такая категория уже существует. Пожалуйста, введите другое название.");
                return Page();
            }

                context.Categories.Add(Category);
                await context.SaveChangesAsync();

                return RedirectToPage("AddProduct");
            
        }
    }
}
