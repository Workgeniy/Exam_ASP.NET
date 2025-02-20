using ExamProject.Data.ShopDb;
using ExamProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace ExamProject.Pages.PagesAdmin
{
    [Authorize(Roles = "Admin")]
    public class AddDescriptionModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public AddDescriptionModel (ApplicationDbContext context) {
            this.context = context;
        }

        [BindProperty]
        public Description Description { get; set; }

        public static IList<string> ListDescription { get; set; }

        public void OnGet () {
            ListDescription = context.Description.Select(item => item.Name).ToList();
        }

        public async Task<IActionResult> OnPost () {
            if (ListDescription.Contains(Description.Name)) {
                ModelState.AddModelError("Description.Name",
           "Такой поставщик уже существует. Пожалуйста, введите другой.");
                return Page();
            }

            context.Description.Add(Description);
            await context.SaveChangesAsync();

            return RedirectToPage("AddProduct");

        }
    }
}
