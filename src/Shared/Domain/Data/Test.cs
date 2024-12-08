using Domain.Data;
using Microsoft.AspNetCore.Identity;
using static System.Collections.Specialized.BitVector32;

namespace Domain.Data
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }
        public string AccessCode { get; set; }
        public string UserId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public string TestStatus { get; set; }
        public List<TestAssignment> Assignments { get; set; } = new List<TestAssignment>();

        public List<Question> Questions { get; set; } = new List<Question>();
    }

}

