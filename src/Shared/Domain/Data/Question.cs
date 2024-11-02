using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Data
{
    public class Question
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public int TestId { get; set; }
        public Test Test { get; set; }  

        public List<Answer> Answers { get; set; } = new List<Answer>();  // Lista odpowiedzi
    }
}

