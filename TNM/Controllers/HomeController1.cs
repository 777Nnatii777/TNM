using Microsoft.AspNetCore.Mvc;
using TNM.Models; 
using System.Collections.Generic;

namespace TNM.Controllers 
{
    public class HomeController1 : Controller
    {
        public IActionResult Index()
        {
            
            var books = new List<Book>
            {
                new Book { Title = "1984", Author = "George Orwell" },
                new Book { Title = "To Kill a Mockingbird", Author = "Harper Lee" },
                new Book { Title = "The Great Gatsby", Author = "F. Scott Fitzgerald" }
            };

            
            return View(books);
        }
    }
}
