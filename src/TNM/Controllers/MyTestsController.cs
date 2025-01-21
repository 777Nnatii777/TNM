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
        
        var myTests = await _context.Tests
            
            .Where(t => t.UserId == userId)
            .ToListAsync();

        var adedTests = await _context.TestAddeds
            
            .Where(ta => ta.AdedUserId == userId)
            .ToListAsync();
       

        var doneTests = await _context.Tests
            .Where(t => t.UserId == userId && t.TestStatus == "New")
            .ToListAsync();

        

        var doneuserTests = await _context.TestResults
        .Include(tr => tr.Test) 
        .Where(tr => tr.UserId == userId) 
        .ToListAsync();

        
        ViewBag.DoneUserTests = doneuserTests;
        ViewBag.MyTests = myTests;
        ViewBag.AdedTests = adedTests;
        ViewBag.DoneTests = doneTests;
       

        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> AddTestByCode(string accessCode)
    {

        Console.WriteLine("Kontroler myTestController i akcja AddTestByCode sie wykonuje");
        if (string.IsNullOrEmpty(accessCode))
        {
            Console.WriteLine("Kod dostępu jest pusty");
            return RedirectToAction("Index");
        }
        else
        {
            Console.WriteLine($"Odczytany kod testu w kontrolerze: {accessCode}");
        }
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var test = await _context.Tests.FirstOrDefaultAsync(t => t.AccessCode == accessCode);
        if (test == null)
        {
            TempData["Error"] = "Nie znaleziono testu o podanym kodzie.";
            return RedirectToAction("Index");
        }
        var testAdded = new TestAdded
        {
            Title = test.Title,
            Description = test.Description,
            AccessCode = test.AccessCode,
            AuthorId = test.UserId,
            AdedUserId = userId,
            CreatedAt = DateTime.Now,
            TestStatus = test.TestStatus,
            TestId = test.Id
        };
        _context.TestAddeds.Add(testAdded);
        await _context.SaveChangesAsync();
        TempData["Success"] = "Test został pomyślnie dodany.";
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> EditTest(int id)
    {
        var test = await _context.Tests
            .Include(t => t.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (test == null)
        {
            TempData["Error"] = "Nie znaleziono testu.";
            return RedirectToAction("Index");
        }

        return View(test); 
    }


    public async Task<IActionResult> ShowCodePage([FromQuery] string code)
    {
        Console.WriteLine("Akcja ShowCodePage została wywołana");
        Console.WriteLine($"Code received: {code}");

        if (string.IsNullOrEmpty(code))
        {
            TempData["Error"] = "Nie znaleziono kodu testu.";
            return RedirectToAction("Index");
        }

        return View("ShowCodePage", code);
    }

    public async Task<IActionResult> StartTest(int id)
    {
        
        var test = await _context.Tests
            .Include(t => t.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(t => t.Id == id);

        
        if (test == null)
        {
            return NotFound();
        }

        
        
        return View("StartTest", test);
    }

   


    [HttpPost]
    public async Task<IActionResult> SubmitAnswers(TestSubmissionDto submission)
    {
        var test = await _context.Tests
            .Include(t => t.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(t => t.Id == submission.TestId);

        if (test == null)
        {
            TempData["Error"] = "Nie znaleziono testu.";
            return RedirectToAction("Index");
        }

        var correctAnswers = test.Questions
            .SelectMany(q => q.Answers)
            .Where(a => a.IsCorrect)
            .Select(a => a.Id)
            .ToList();

        var correctCount = submission.SelectedAnswers
            .Count(answerId => correctAnswers.Contains(answerId));

        var totalQuestions = test.Questions.Count;

        
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var testResult = new TestResult
        {
            TestId = test.Id,
            UserId = userId,
            CorrectAnswers = correctCount,
            TotalQuestions = totalQuestions,
            CompletedAt = DateTime.Now
        };

        _context.TestResults.Add(testResult);
        await _context.SaveChangesAsync();

        TempData["Result"] = $"Poprawne odpowiedzi: {correctCount} z {totalQuestions}.";
        return RedirectToAction("Index");
    }



}
