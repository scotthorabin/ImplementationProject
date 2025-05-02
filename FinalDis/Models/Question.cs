using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace DissertationProject.Models
{
   

    public class Question
    {
        public int QuestionID { get; set; } // Primary Key

        [Required]
        public string QuestionText { get; set; }

        public int QuizID { get; set; }

        public Quiz Quiz { get; set; } // Navigation Property

        public List<Answer> Answers { get; set; }
    }

}
