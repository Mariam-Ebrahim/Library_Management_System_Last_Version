﻿using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace LibrarySystem.Models
{
    public class CartItem
    {
      
        public int Id { get; set; }
        public Book Book { get; set; }
        public string CartId { get; set; }
    }
}
