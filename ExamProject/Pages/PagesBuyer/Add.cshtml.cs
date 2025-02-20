using ExamProject.Data.ShopDb;
using ExamProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ExamProject.Pages.PagesBuyer
{
    [Authorize]
    public class AddModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public AddModel (ApplicationDbContext context) {
            this.context = context;
        }

        public async Task<IActionResult> OnGetAsync(int id) {
            string? UserIdCurrent = User.FindFirstValue(ClaimTypes.NameIdentifier); //получаем текущего пользователя

            Cart? cart = await context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => UserIdCurrent == c.ApplicationUserId);

            if (cart == null) {
                cart = new Cart() {
                    ApplicationUserId = UserIdCurrent,
                    Items = new List<CartItem>()
                };
                context.Carts.Add(cart);
                await context.SaveChangesAsync();
            }

            CartItem? cartItem = cart.Items.FirstOrDefault(itemId => itemId.ProductId == id);
            if (cartItem == null) {
                cartItem = new CartItem() {
                    CartId = cart.Id,
                    ProductId = id,
                    Quantity = 1
                };
                context.CartItems.Add(cartItem);
            }
            else
                cartItem.Quantity++;

            await context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
