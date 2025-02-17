using Microsoft.AspNetCore.Identity;

namespace ExamProject.Data.ShopDb
{
    public class ApplicationUser : IdentityUser
    {
        public Cart Cart { get; set; }
    }
}
