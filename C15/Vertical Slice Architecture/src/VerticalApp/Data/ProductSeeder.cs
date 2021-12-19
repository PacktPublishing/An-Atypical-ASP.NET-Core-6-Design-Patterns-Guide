using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VerticalApp.Models;

namespace VerticalApp.Data;

internal static class ProductSeeder
{
    public static Task SeedAsync(ProductContext db)
    {
        db.Products.Add(new Product
        {
            Id = 1,
            Name = "Banana",
            QuantityInStock = 50
        });
        db.Products.Add(new Product
        {
            Id = 2,
            Name = "Apple",
            QuantityInStock = 20
        });
        db.Products.Add(new Product
        {
            Id = 3,
            Name = "Habanero Pepper",
            QuantityInStock = 10
        });
        return db.SaveChangesAsync();
    }
}
