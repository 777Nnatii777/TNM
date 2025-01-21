using Domain.Data;
using Microsoft.AspNetCore.Identity;

namespace Domain.Data
{
    public class TestResult
    {
        public int Id { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public int CorrectAnswers { get; set; }
        public int TotalQuestions { get; set; }

        public DateTime CompletedAt { get; set; }
    }
}