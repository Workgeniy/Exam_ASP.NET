using ExamProject.Data;
using ExamProject.Data.ShopDb;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ExamProject.Pages;

public class IndexModel : PageModel
{
    private ApplicationDbContext context;

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
    {
        _logger = logger;
        this.context = context;
    }


    public List<Product> Products { get; set; }


    public void OnGet () {
        Products = context.Products.ToList();
    }

    public async Task<IActionResult> OnPostDeleteAsync(int id) {
        Product? product = await context.Products.FindAsync(id);
        if (product != null) {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
            
        return RedirectToPage();
    }

   
    //public async Task<IActionResult> OnPostAddToCartAsync (int id) {
    //    string? UserIdCurrent = User.FindFirstValue(ClaimTypes.NameIdentifier); //получаем текущего пользователя

    //    Cart? cart = await context.Carts
    //        .Include(c => c.Items)
    //        .FirstOrDefaultAsync(c => UserIdCurrent == c.ApplicationUserId);

    //    if (cart == null) {
    //        cart = new Cart() {
    //            ApplicationUserId = UserIdCurrent,
    //            Items = new List<CartItem>()
    //        };
    //        context.Carts.Add(cart);
    //        await context.SaveChangesAsync();
    //    }

    //    CartItem? cartItem = cart.Items.FirstOrDefault(itemId => itemId.ProductId == id);
    //    if (cartItem == null) {
    //        cartItem = new CartItem() {
    //            CartId = cart.Id,
    //            ProductId = id,
    //            Quantity = 1
    //        };
    //        context.CartItems.Add(cartItem);
    //    }
    //    else
    //        cartItem.Quantity++;

    //    await context.SaveChangesAsync();

    //    return Page();
    //}
}

