using Domain.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

[Authorize]
public class MyTestsController : Controller
{
    private readonly MyApplicationDbContext _context;

    public MyTestsController(MyApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var myTests = await _context.TestAssignments
            .Include(ta => ta.Test)
            .Where(ta => ta.UserId == userId && ta.AssignmentStatus == "New")
            .ToListAsync();

        var doneTests = await _context.TestAssignments
            .Include(ta => ta.Test)
            .Where(ta => ta.UserId == userId && ta.AssignmentStatus == "Done")
            .ToListAsync();

        var newTests = await _context.Tests
            .Where(t => t.UserId == userId && t.TestStatus == "New")
            .ToListAsync();

        ViewBag.MyTests = myTests;
        ViewBag.DoneTests = doneTests;
        ViewBag.NewTests = newTests;

        return View();
    }
}
