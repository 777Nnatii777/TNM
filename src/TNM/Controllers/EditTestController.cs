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


    [HttpPost]
    public async Task<IActionResult> SaveTest(Test model)
    {
        if (!ModelState.IsValid)
        {
            foreach (var state in ModelState)
            {
                foreach (var error in state.Value.Errors)
                {
                    Console.WriteLine($"Key: {state.Key}, Error: {error.ErrorMessage}");
                }
            }
            return RedirectToAction("Index", "MyTests");
        }

        try
        {
            var existingTest = await _context.Tests
                .Include(t => t.Questions)
                .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(t => t.Id == model.Id);

            if (existingTest == null)
            {
                Console.WriteLine("zwraca ze nei znajduje testu do pobrania orginału");
                return NotFound("Test not found");
            }

            
            existingTest.Title = model.Title;
            existingTest.Description = model.Description;

            foreach (var question in model.Questions)
            {
                var existingQuestion = existingTest.Questions.FirstOrDefault(q => q.Id == question.Id);

                if (existingQuestion != null)
                {
                    
                    existingQuestion.QuestionTitle = question.QuestionTitle;
                    existingQuestion.Type = question.Type;

                    foreach (var answer in question.Answers)
                    {
                        var existingAnswer = existingQuestion.Answers.FirstOrDefault(a => a.Id == answer.Id);

                        if (existingAnswer != null)
                        {
                            Console.WriteLine($"Aktualizacja odpowiedzi ID: {existingAnswer.Id}");
                            Console.WriteLine($"Stare dane - Tekst: {existingAnswer.Text}, IsCorrect: {existingAnswer.IsCorrect}");
                            Console.WriteLine($"Nowe dane - Tekst: {answer.Text}, IsCorrect: {answer.IsCorrect}");
                            existingAnswer.Text = answer.Text;
                            existingAnswer.IsCorrect = answer.IsCorrect;
                            
                        }
                        else
                        {

                            
                            Console.WriteLine($"existingAnswer in null");
                        }
                    }

                    
                    var answersToRemove = existingQuestion.Answers
                        .Where(ea => !question.Answers.Any(a => a.Id == ea.Id))
                        .ToList();

                    foreach (var answerToRemove in answersToRemove)
                    {
                        _context.Answers.Remove(answerToRemove);
                    }
                }
                else
                {
                    
                    existingTest.Questions.Add(new Question
                    {
                        QuestionTitle = question.QuestionTitle,
                        Type = question.Type,
                        Answers = question.Answers.Select(a => new Answer
                        {
                            Text = a.Text,
                            IsCorrect = a.IsCorrect
                        }).ToList()
                    });
                }
            }

            await _context.SaveChangesAsync();
            Console.WriteLine("Udało się zapisac aktualziaje testu");
            return RedirectToAction("Index", "MyTests");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error updating test: {ex.Message}");
            return View("Error");
        }
    }



}