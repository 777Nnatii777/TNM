using Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

public class EditTestController : Controller
{
    private readonly MyApplicationDbContext _context;

    public EditTestController(MyApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
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

        return View("~/Views/MyTests/EditTest.cshtml", test); 
    }

    [HttpPost]
    public async Task<IActionResult> EditTest(Test model)
    {
        Console.WriteLine("EditTest POST triggered.");
        Console.WriteLine($"Model ID: {model.Id}");
        Console.WriteLine($"Model Title: {model.Title}");
        Console.WriteLine($"Model Description: {model.Description}");

        if (!ModelState.IsValid)
        {
            Console.WriteLine("ModelState is invalid.");
            foreach (var error in ModelState)
            {
                Console.WriteLine($"Key: {error.Key}, Errors: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
            }
            TempData["Error"] = "Niepoprawne dane formularza.";
            return View("~/Views/MyTests/EditTest.cshtml", model);
        }

        var test = await _context.Tests
            .Include(t => t.Questions)
            .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(t => t.Id == model.Id);

        if (test == null)
        {
            Console.WriteLine("Nie znaleziono testu w bazie danych.");
            TempData["Error"] = "Nie znaleziono testu.";
            return RedirectToAction("Index");
        }

        
        test.Title = model.Title;
        test.Description = model.Description;

        foreach (var question in model.Questions)
        {
            var existingQuestion = test.Questions.FirstOrDefault(q => q.Id == question.Id);

            if (existingQuestion != null)
            {
                existingQuestion.QuestionTitle = question.QuestionTitle;
                existingQuestion.Type = question.Type;

                foreach (var answer in question.Answers)
                {
                    var existingAnswer = existingQuestion.Answers.FirstOrDefault(a => a.Id == answer.Id);

                    if (existingAnswer != null)
                    {
                        existingAnswer.Text = answer.Text;
                        existingAnswer.IsCorrect = answer.IsCorrect;
                    }
                    else
                    {
                        existingQuestion.Answers.Add(new Answer
                        {
                            Text = answer.Text,
                            IsCorrect = answer.IsCorrect,
                            QuestionId = existingQuestion.Id
                        });
                    }
                }
            }
            else
            {
                test.Questions.Add(new Question
                {
                    QuestionTitle = question.QuestionTitle,
                    Type = question.Type,
                    Answers = question.Answers
                });
            }
        }

        try
        {
            _context.Tests.Update(test);
            await _context.SaveChangesAsync();
            Console.WriteLine("Zapisano zmiany w bazie danych.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Błąd podczas zapisu: {ex.Message}");
            TempData["Error"] = "Wystąpił problem podczas zapisu zmian.";
            return View("~/Views/MyTests/EditTest.cshtml", model);
        }

        return RedirectToAction("Index", "MyTests");
    }

}
