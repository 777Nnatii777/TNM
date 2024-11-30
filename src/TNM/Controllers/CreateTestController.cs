using Domain.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace TNM.Controllers
{
    public class CreateTestController : Controller
    {

        

        private readonly MyApplicationDbContext _dbContext;

        public CreateTestController(MyApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        protected string GenerateRandomCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();
        }

        private async Task<string> GenerateAccessCode()
        {
            string accessCode;

            do
            {

                accessCode = GenerateRandomCode();
            }

            while (await _dbContext.Tests.AnyAsync(t => t.AccessCode == accessCode));

            return accessCode;
        }
        




        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult CreateTest()
        {
            return View("Index");
        }

        private string test;
        [HttpPost]
        public async Task<IActionResult> CreateTest(string testname, string testdescription)

        {
            if (!ModelState.IsValid)
            {
                var modelStateErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                test = string.Join("; ", modelStateErrors);
                return View("Index");
            }


            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);



            if (string.IsNullOrEmpty(userID))
            {
                Console.WriteLine("UserID is null or empty.");
            }
            

            var accessCode = await GenerateAccessCode();

            var newTest = new Test()
            {

                Title = testname,
                Description = testdescription,
                CreatedAt = DateTime.Now,
                TestStatus = "New",
                UserId = userID,
                AccessCode = accessCode
               

            };
            Console.WriteLine($"Title: {testname}, Description: {testdescription}");

            try
            {
                _dbContext.Tests.Add(newTest);
                var result = await _dbContext.SaveChangesAsync();
                Console.WriteLine($"Rows affected: {result}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during SaveChanges: {ex.Message}");
            }


            return RedirectToAction("Index");
        }
        
       
       
    }
}
