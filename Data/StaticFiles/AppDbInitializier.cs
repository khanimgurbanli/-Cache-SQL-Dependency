using Cache_SQL_Dependency.Data.Contexts;
using Cache_SQL_Dependency.Models;

namespace Cache_SQL_Dependency.Data.StaticFiles
{
    public class AppDbInitializier
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                if (!context.Products.Any())
                {
                    new Product()
                    {
                        ProductName = "Chai",
                        SupplierID = 1,
                        CategoryID = 2,
                        QuantityPerUnit = "10 boxes x 20 bags",
                        UnitPrice = 18.00,
                        UnitsInStock = 39,
                        UnitsOnOrder = 0,
                        ReorderLevel = 0,
                        Discontinued = false
                    };
                    new Product()
                    {
                        ProductName = "Chang",
                        SupplierID = 2,
                        CategoryID = 3,
                        QuantityPerUnit = "7 boxes x 20 bags",
                        UnitPrice = 37.00,
                        UnitsInStock = 19,
                        UnitsOnOrder = 0,
                        ReorderLevel = 0,
                        Discontinued = false
                    };
                    new Product()
                    {
                        ProductName = "Anissed Syrup",
                        SupplierID = 4,
                        CategoryID = 3,
                        QuantityPerUnit = "7 boxes x 20 bags",
                        UnitPrice = 37.00,
                        UnitsInStock = 19,
                        UnitsOnOrder = 0,
                        ReorderLevel = 0,
                        Discontinued = false
                    };
                }

            }
        }
    }
}
