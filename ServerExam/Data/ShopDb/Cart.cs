﻿using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ServerExam.Data.ShopDb {
    public class Cart {

        [Key]
        public int Id { get; set; }

        public ICollection<CartItem> Items { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser User { get; set; }
    }
}
