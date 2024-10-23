
using Microsoft.AspNetCore.Mvc;
using TNM.Models;
using System.Collections.Generic;

namespace TNM.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult List()
        {
            var movies = new List<Movie>
            {
                new Movie { Title = "Inception", Director = "Christopher Nolan", Year = 2010 },
                new Movie { Title = "The Matrix", Director = "The Wachowskis", Year = 1999 },
                new Movie { Title = "Interstellar", Director = "Christopher Nolan", Year = 2014 }
            };

            return View(movies);
        }
    }
}
