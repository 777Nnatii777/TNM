using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
    public class Test
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string AccessCode { get; set; }  

        public int UserId { get; set; }  
        public User Creator { get; set; }  

        public List<Question> Questions { get; set; } = new List<Question>();
        public string TestStatus { get; set; } 
    }
}
