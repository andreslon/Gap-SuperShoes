using System;
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
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Display(Name = "Total In Shelf")]
        public int TotalInShelf { get; set; }
        [Display(Name = "Total In Vault")]
        public int TotalInVault { get; set; }
        [Required]
        public Store Store { get; set; }

    }
}
