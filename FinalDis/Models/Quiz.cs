namespace DissertationProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Quiz
    {
        public int QuizID { get; set; }

        [Required]
        public string QuizTitle { get; set; }

        public int TopicID { get; set; }

        public Topic Topic { get; set; } // Navigation Property

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<Question> Questions { get; set; }
    }

}
