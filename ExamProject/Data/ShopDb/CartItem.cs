using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Data.ShopDb {
    public class CartItem {

        [Key]
        public int Id { get; set; }

        public int Quantity { get; set; }

        public int CartId { get; set; }
        public Cart Cart { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
