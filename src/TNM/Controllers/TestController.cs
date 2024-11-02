using Domain.Data;
using Microsoft.AspNetCore.Mvc;

namespace TNM.Controllers
{
    public class TestController : Controller
    {
        private readonly MyApplicationDbContext _context;

        public TestController(MyApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> CreateTest(string title, int userId)
        {
            var test = new Test
            {
                Title = title,
                UserId = userId,
                AccessCode = GenerateAccessCode()
            };

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return RedirectToAction("List");
        }

        private string GenerateAccessCode()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 8).ToUpper();  
        }
    }

}
