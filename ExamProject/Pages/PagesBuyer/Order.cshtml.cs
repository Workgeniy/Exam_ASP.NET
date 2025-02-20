using ExamProject.Data;
using ExamProject.Data.ShopDb;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExamProject.Pages.PagesBuyer
{
    public class OrderModel : PageModel
    {
        private readonly ApplicationDbContext context;

        public double Summa { get; set; }

        public OrderModel(ApplicationDbContext context) {
            this.context = context;
        }

        public void OnGet(double summa)
        {
            Summa = summa;
        }

        public async Task<IActionResult> OnPostAsync () {
            string UserIdCurrent = User.FindFirstValue(ClaimTypes.NameIdentifier); //получаем текущего пользователя

            Cart? cart = await context.Carts
                .Include(item => item.Items)
                .ThenInclude(product => product.Product)
                 .FirstOrDefaultAsync(user => user.ApplicationUserId == UserIdCurrent);

            if (cart == null) {
                return RedirectToPage("/Index");
            }

            foreach (var item in cart.Items)
            {
                item.Product.Quantity -= item.Quantity;
                if (item.Product.Quantity <= 0) {
                    context.Products.Remove(item.Product);
                }
            }

            context.Carts.Remove(cart);

            await context.SaveChangesAsync();

            return RedirectToPage("/Index");
        }
    }
}
