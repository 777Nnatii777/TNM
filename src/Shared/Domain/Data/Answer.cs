using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;


namespace Domain.Data
{
    public class Answer
    {
        public int Id { get; set; }  
        public string Text { get; set; }

        [ValidateNever]
        public bool IsCorrect { get; set; } = false;
        public int QuestionId { get; set; }

        [ValidateNever]
        public Question Question { get; set; }  
    }
}
