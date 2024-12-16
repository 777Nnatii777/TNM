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



        [HttpPost]
        public async Task<IActionResult> CreateTest(string testname, string testdescription, List<Question> questions)
        {
            
            foreach (var question in questions)
            {
                foreach (var answer in question.Answers)
                {
                    var key = $"questions[{questions.IndexOf(question)}].Answers[{question.Answers.IndexOf(answer)}].IsCorrect";
                    if (ModelState.ContainsKey(key))
                    {
                        ModelState.Remove(key);
                    }
                }
            }

            if (!ModelState.IsValid)
            {
                var modelStateErrors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                Console.WriteLine("Model state invalid: " + string.Join(", ", modelStateErrors));
                return View("Index");
            }

            var userID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userID))
            {
                Console.WriteLine("UserID is null or empty.");
                return RedirectToAction("Index");
            }

            Console.WriteLine($"Test Name: {testname}");
            Console.WriteLine($"Test Description: {testdescription}");

            var accessCode = await GenerateAccessCode();
            var newTest = new Test
            {
                Title = testname,
                Description = testdescription,
                CreatedAt = DateTime.Now,
                TestStatus = "New",
                UserId = userID,
                AccessCode = accessCode,
                Questions = new List<Question>()
            };

            if (questions != null && questions.Any())
            {
                foreach (var question in questions)
                {
                    question.Test = newTest;

                    if (question.Answers != null && question.Answers.Any())
                    {
                        foreach (var answer in question.Answers)
                        {
                            
                            var isCorrectValue = Request.Form[$"questions[{questions.IndexOf(question)}].Answers[{question.Answers.IndexOf(answer)}].IsCorrect"];
                            answer.IsCorrect = !string.IsNullOrEmpty(isCorrectValue) && isCorrectValue == "on";
                            answer.Question = question; 

                            Console.WriteLine($"Answer Text: {answer.Text}, IsCorrect: {answer.IsCorrect}");
                        }
                    }

                    newTest.Questions.Add(question);
                }
            }

            try
            {
                _dbContext.Tests.Add(newTest);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine("Test saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during SaveChanges: {ex.Message}");
                return View("Index");
            }

            return RedirectToAction("Index");
        }






    }
}
