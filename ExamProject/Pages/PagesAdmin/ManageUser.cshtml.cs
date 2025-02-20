using ExamProject.Data.ShopDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExamProject.Pages.PagesAdmin
{
    [Authorize(Roles = "Admin")]
    public class ManageUserModel : PageModel
    {
        private readonly UserManager<ApplicationUser> user;

        public ManageUserModel(UserManager<ApplicationUser> user) {
          this.user = user;
        }

        public IList<UserDto> Users { get; set; } = new List<UserDto>();

        public async Task OnGetAsync()
        {
            var selectUser = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var AllUsers = await user.Users
                .Where(id => id.Id!= selectUser)
                .ToListAsync();

            foreach (var item in AllUsers) {
                var role = await user.GetRolesAsync(item);
                    Users.Add(new UserDto {
                        Id = item.Id,
                        Email = item.Email,
                        IsAdmin = role.Contains("Admin"),
                    });
                
            }

        }

        public async Task<IActionResult> OnPostGiveToAdmin (string id) {

            ApplicationUser? selectUser = await user.FindByIdAsync(id);
            if (selectUser == null) {
                return NotFound($"Пользователь с ID = {id} не найден.");
            }

            var result = await user.AddToRoleAsync(selectUser, "Admin");

            // Если возникли ошибки (например, роль не существует)
            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteToAdmin (string id) {

            ApplicationUser? selectUser = await user.FindByIdAsync(id);
            if (selectUser == null) {
                return NotFound($"Пользователь с ID = {id} не найден.");
            }

            var result = await user.RemoveFromRoleAsync(selectUser, "Admin");

            // Если возникли ошибки (например, роль не существует)
            if (!result.Succeeded) {
                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToPage();
        }
    }
}
