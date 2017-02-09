using SuperShoes.Data.Context;
using SuperShoes.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperShoes.Data.Migrations
{
    public class SuperShoesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SuperShoesContext>
    {

        protected override void Seed(SuperShoesContext context)
        {


            var stores = new List<Store>
            {
                new Store { Name= "Main Store", Address="Cra 56 c 34-33" }
            };

            stores.ForEach(s => context.Stores.Add(s));
            context.SaveChanges();

            var articles = new List<Article>
            {
                new Article() { Name="Article Base", Description="article initial of main store", Price=1000, Store=stores[0], TotalInShelf=2, TotalInVault=1 }
            };

            articles.ForEach(s => context.Articles.Add(s));
            context.SaveChanges();
        }
    }
}
