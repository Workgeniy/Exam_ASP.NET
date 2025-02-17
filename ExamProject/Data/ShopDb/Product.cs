﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ExamProject.Data.ShopDb {
    public class Product {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public int Quantity { get; set; }

        public int DescriptionId { get; set; }
        public Description Description { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public int CartItemId {  get; set; }
        public CartItem CartItem { get; set; }
    }
}
