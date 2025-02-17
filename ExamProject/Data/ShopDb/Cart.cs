using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Data.ShopDb {
    public class Cart {

        [Key]
        public int Id { get; set; }

        public Collection<CartItem> Items { get; set; }

        public string ApplicationUserId {  get; set; }
        public ApplicationUser User { get; set; }
    }
}
