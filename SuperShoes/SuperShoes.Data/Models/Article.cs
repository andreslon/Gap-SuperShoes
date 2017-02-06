﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShoes.Data.Models
{
    public class Article
    {
        public Article() {
            Id = Guid.NewGuid();
        }
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Descripcion { get; set; }
        [Required]
        public double Price { get; set; }
        public int TotalInShelf { get; set; }
        public int TotalInVault { get; set; }
        [Required]
        public Store Store { get; set; }

    }
}