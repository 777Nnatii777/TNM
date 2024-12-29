using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Data
{
    public class TestAdded
    {

        public int Id { get; set; }  // Klucz główny

        public string Title { get; set; }

        public string Description { get; set; }
        public string AccessCode { get; set; }
        public string AuthorId { get; set; }

        public string AdedUserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string TestStatus { get; set; }
        public List<TestAssignment> Assignments { get; set; } = new List<TestAssignment>();

        public int TestId { get; set; }  // Klucz obcy do Test
        public Test Test { get; set; }

        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
