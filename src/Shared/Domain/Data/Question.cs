using System.Collections.Generic;

namespace Domain.Data
{
    public class Question
    {
        public int Id { get; set; }

        public string QuestionTitle { get; set; } = string.Empty; 
        public string? Text { get; set; } 
        public QuestionType Type { get; set; } 

        public int TestId { get; set; }  
        public Test? Test { get; set; }  

        public List<Answer>? Answers { get; set; } = new List<Answer>(); 
    }


    public enum QuestionType
    {
        SingleChoice,
        MultipleChoice, 
        TrueFalse
    }
}
