using Domain.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;

public class ProductsController : Controller
{
    private readonly MyApplicationDbContext _context;

    public ProductsController(MyApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); 

        
        var receivedTests = await _context.TestResults
            .Include(tr => tr.Test) 
            .Include(tr => tr.User) 
            .Where(tr => tr.Test.UserId == userId && tr.UserId != userId) 
            .ToListAsync();

        ViewBag.ReceivedTests = receivedTests; 
        return View();
    }
}
