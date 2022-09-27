using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Cache_SQL_Dependency.Models;
using Cache_SQL_Dependency.Data.Contexts;
using Microsoft.Extensions.Caching.Memory;

namespace Cache_SQL_Dependency.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IMemoryCache _memoryCache;
        public ProductsController(AppDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        #region ProductList
        List<Product> products = new()
                 {
                    new Product { ProductName = "Chai",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
                    new Product { ProductName = "Chang" ,SupplierID=15,CategoryID=7, UnitsInStock=1,UnitsOnOrder=0},
                    new Product { ProductName = "Alice Mutton",SupplierID=15,CategoryID=4, UnitsInStock=4,UnitsOnOrder=1 },
                    new Product { ProductName = "Aniseed Syrup" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
                    new Product { ProductName = "Boston Crab Meat" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
                    new Product { ProductName = "Camembert Pierrot",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
                    new Product { ProductName = "Carnarvon Tigers" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
                    new Product { ProductName = "Chai",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
                    new Product { ProductName = "Chang" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
                    new Product { ProductName = "Chartreuse verte",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
                    new Product { ProductName = "Chef Anton's Cajun Seasoning",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
                    new Product { ProductName = "Chef Anton's Gumbo Mix" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
                    new Product { ProductName = "Chocolade",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
                    new Product { ProductName = "Côte de Blaye",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
                    new Product { ProductName = "Escargots de Bourgogne" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
                    new Product { ProductName = "Filo Mix" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
                    new Product { ProductName = "Flotemysost" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
                    new Product { ProductName = "Geitost",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 }
                 };
        #endregion


        // GET: Products
        public async Task<IActionResult> Index()
        {
            List<Product> products;
            DateTime dateNow = DateTime.Now;
            if (!_memoryCache.TryGetValue("products", out products))
            {
                dateNow = DateTime.Now;
                _memoryCache.Set("products", await _context.Products.ToListAsync());
                _memoryCache.Set("date", dateNow);
            }
            products =  _memoryCache.Get("products") as List<Product>;
            ViewBag.beginDate = _memoryCache.Get("date");
            ViewBag.updatedDate = DateTime.Now;
            return View(products);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {

            //List<Product> products = new()
            //     {
            //        new Product { ProductName = "Chai",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
            //        new Product { ProductName = "Chang" ,SupplierID=15,CategoryID=7, UnitsInStock=1,UnitsOnOrder=0},
            //        new Product { ProductName = "Alice Mutton",SupplierID=15,CategoryID=4, UnitsInStock=4,UnitsOnOrder=1 },
            //        new Product { ProductName = "Aniseed Syrup" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
            //        new Product { ProductName = "Boston Crab Meat" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
            //        new Product { ProductName = "Camembert Pierrot",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
            //        new Product { ProductName = "Carnarvon Tigers" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
            //        new Product { ProductName = "Chai",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
            //        new Product { ProductName = "Chang" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
            //        new Product { ProductName = "Chartreuse verte",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
            //        new Product { ProductName = "Chef Anton's Cajun Seasoning",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
            //        new Product { ProductName = "Chef Anton's Gumbo Mix" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
            //        new Product { ProductName = "Chocolade",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
            //        new Product { ProductName = "Côte de Blaye",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 },
            //        new Product { ProductName = "Escargots de Bourgogne" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
            //        new Product { ProductName = "Filo Mix" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
            //        new Product { ProductName = "Flotemysost" ,SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0},
            //        new Product { ProductName = "Geitost",SupplierID=15,CategoryID=1, UnitsInStock=0,UnitsOnOrder=0 }
            //     };


            //_context.Products.AddRange(products);//add list to db
            //_context.SaveChanges();

            //var b = _context.Products.ToList().Count;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductName,SupplierID=15,CategoryID,UnitsInStock,UnitsOnOrder")] Product product)
        {
            if (ModelState.IsValid)
            {
               
                await _context.Products.AddAsync(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductID,ProductName,SupplierID=15,CategoryID,QuantityPerUnit,UnitPrice,UnitsInStock,UnitsOnOrder,ReorderLevel,Discontinued")] Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'AppDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductID == id);
        }
    }
}
