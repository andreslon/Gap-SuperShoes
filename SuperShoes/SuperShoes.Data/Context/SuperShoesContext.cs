using SuperShoes.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShoes.Data.Context
{
    public class SuperShoesContext : DbContext
    {
        public SuperShoesContext() : base("SuperShoesContext")
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
