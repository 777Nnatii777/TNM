using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Domain.Data; 


public class MyTestsController : Controller
{ 
    private readonly MyApplicationDbContext _context;

    public MyTestsController(MyApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
       
        var myTests = await _context.TestAssignments
            .Include(ta => ta.Test)
            .Where(ta => ta.UserId == 1 && ta.AssignmentStatus == "New") 
            .ToListAsync();

        var doneTests = await _context.TestAssignments
            .Include(ta => ta.Test)
            .Where(ta => ta.UserId == 2 && ta.AssignmentStatus == "Done") 
            .ToListAsync();

        var newTests = await _context.Tests
            .Where(t => t.TestStatus == "New")
            .ToListAsync();

        
        ViewBag.MyTests = myTests;
        ViewBag.DoneTests = doneTests;
        ViewBag.NewTests = newTests;

        return View();
    }
}

