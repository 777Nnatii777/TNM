using Domain.Data;
using Microsoft.AspNetCore.Identity;

namespace Domain.Data
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AccessCode { get; set; }
        public string UserId { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public string TestStatus { get; set; }
        public List<TestAssignment> Assignments { get; set; } = new List<TestAssignment>();
    }
}

