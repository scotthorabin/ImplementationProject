using System.ComponentModel.DataAnnotations;
namespace DissertationProject.Models
{
    

    public class Answer
    {
        public int AnswerID { get; set; }

        [Required]
        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        public int QuestionID { get; set; }

        public Question Question { get; set; } // Navigation Property
    }

}
