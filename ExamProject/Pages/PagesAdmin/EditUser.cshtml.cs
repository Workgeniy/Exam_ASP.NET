using ExamProject.Data.ShopDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Pages.PagesAdmin
{
    [Authorize(Roles = ("Admin"))]
    public class EditUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public EditUserModel (UserManager<ApplicationUser> userManager) {
            _userManager = userManager;
        }

        [BindProperty]
        public EditUserInput Input { get; set; }

        public class EditUserInput {
            public string Id { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string UserName { get; set; }
        }

        public async Task<IActionResult> OnGetAsync (string id) {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) {
                return NotFound($"Пользователь с ID = {id} не найден.");
            }

            Input = new EditUserInput {
                Id = user.Id,
                Email = user.Email,
                UserName = user.UserName
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync (string id) {

            var user = await _userManager.FindByIdAsync(id);
            if (user == null) {
                return NotFound($"Пользователь с ID = {Input.Id} не найден.");
            }

            user.Email = Input.Email;
            user.UserName = Input.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
                return Page();
            }


            return RedirectToPage("ManageUser");
        }
    }
}
