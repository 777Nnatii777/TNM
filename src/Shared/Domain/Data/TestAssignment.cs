using Microsoft.AspNetCore.Identity;
using System;

namespace Domain.Data
{
    public class TestAssignment
    {
        public int AssignmentId { get; set; }  // Klucz główny

        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        public int TestId { get; set; }
        public Test Test { get; set; }

        public string AssignmentStatus { get; set; }
        public DateTime AssignedAt { get; set; }
    }
}
