using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Data.ShopDb {
    public class Category {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Collection<Product> Products { get; set; }
    }
}
