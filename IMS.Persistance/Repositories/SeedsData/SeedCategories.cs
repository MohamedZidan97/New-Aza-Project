using IMS.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Persistance.Repositories.SeedsData
{
    public static class SeedCategories
    {
        public static async Task SeedCategoriesAsync(ApplicationDbContext context)
        {
            //var catss = await context.categories.ToListAsync();
            //if (catss == null)
            //{
                var cat = new List<Category> { new Category { CategoryId = 2, CategoryName = "Mobile" } };

                context.categories.AddRangeAsync(cat);
                await context.SaveChangesAsync();

                var subc = new List<SubCategory>()
{
    new SubCategory { SubCategoryId = 1, CategoryId = 1, Name= "Samsung" },
    new SubCategory { SubCategoryId = 2, CategoryId = 1, Name = "Apple" },
    new SubCategory { SubCategoryId = 3, CategoryId = 1, Name = "Xiaomi" },
    new SubCategory { SubCategoryId = 4, CategoryId = 1, Name = "OnePlus" },
    new SubCategory { SubCategoryId = 5, CategoryId = 1, Name = "Oppo" },
    new SubCategory { SubCategoryId = 6, CategoryId = 1, Name = "Vivo" },
    new SubCategory { SubCategoryId = 7, CategoryId = 1, Name = "Huawei" },
    new SubCategory { SubCategoryId = 8, CategoryId = 1, Name = "Sony" },
    new SubCategory { SubCategoryId = 9, CategoryId = 1, Name = "Nokia" },
    new SubCategory { SubCategoryId = 10, CategoryId = 1, Name = "Google Pixel" }
};

                await context.subCategories.AddRangeAsync(subc);
                await context.SaveChangesAsync();
           // }
        }
    }
}
