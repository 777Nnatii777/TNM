using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;

namespace TNM.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult List()
        {
            
            var products = new List<Product>
            {
                new Product { Id = 1, Name = "Produkt 1", Price = 100.00m },
                new Product { Id = 2, Name = "Produkt 2", Price = 150.50m },
                new Product { Id = 3, Name = "Produkt 3", Price = 200.75m }
            };

            
            return View(products);
        }
    }
}
