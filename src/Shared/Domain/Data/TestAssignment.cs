using System;

namespace Domain.Data
{
    public class TestAssignment
    {
        public int AssignmentId { get; set; } 
        public int UserId { get; set; }
        public User User { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }
        public string Status { get; set; } 
        public DateTime AssignedAt { get; set; } = DateTime.Now; 
    }
}
