using ExamProject.Data.ShopDb;
using ExamProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace ExamProject.Pages.PagesBuyer
{
    public class PageCartModel : PageModel
    {
        private readonly ApplicationDbContext context;
        public PageCartModel (ApplicationDbContext context) {
            this.context = context;
        }
        public double Summa {  get; set; }

        public Cart? Cart { get; set; }

        public async Task OnGet () {
            string UserIdCurrent = User.FindFirstValue(ClaimTypes.NameIdentifier); //получаем текущего пользователя

           Cart = await context.Carts
                .Include(item => item.Items)
                .ThenInclude(product => product.Product)
                .FirstOrDefaultAsync(user =>  user.ApplicationUserId == UserIdCurrent);

            if (Cart != null) {
                List<double> items = new List<double>();
                foreach (var item in Cart.Items) {
                    items.Add(item.Quantity * item.Product.Price);
                }

                Summa = items.Sum();
            }
        }
        public async Task<IActionResult> OnPostDeleteAsync (int id) {
            CartItem? item = await context.CartItems.FindAsync(id);
            if (item != null) {
                context.CartItems.Remove(item);
                await context.SaveChangesAsync();
            }

            return RedirectToPage();
        }

        public async void ClearCart () {

            if (Cart != null) {
                context.Carts.Remove(Cart);
                await context.SaveChangesAsync();
            }
        }
    }
}
