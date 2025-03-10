using Microsoft.AspNetCore.Identity;

namespace ServerExam.Data.ShopDb
{
    public class ApplicationUser : IdentityUser
    {
        public Cart Cart { get; set; }
    }
}
