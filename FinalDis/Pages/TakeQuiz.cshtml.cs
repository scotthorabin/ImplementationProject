
using DissertationProject.Models;
using FinalDis.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DissertationProject.Pages.Quizzes
{
    public class TakeQuizModel : PageModel
    {
        private readonly FinalDisContext _context;

        // Properties to store the quiz and user answers
        public Quiz Quiz { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }

        // Constructor to inject the database context
        public TakeQuizModel(FinalDisContext context)
        {
            _context = context;
        }

        // GET handler to fetch the quiz and its questions
        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Fetch the quiz with its questions and answers from the database
            Quiz = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuizID == id);

            if (Quiz == null)
            {
                return NotFound();
            }

            // Initialize an empty list for user answers
            UserAnswers = new List<UserAnswer>();
            foreach (var question in Quiz.Questions)
            {
                UserAnswers.Add(new UserAnswer
                {
                    QuestionID = question.QuestionID
                });
            }

            return Page();
        }

        // POST handler to process the submitted answers
        public async Task<IActionResult> OnPostAsync(int id, List<UserAnswer> userAnswers)
        {
            // Fetch the quiz again to check the answers
            Quiz = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuizID == id);

            if (Quiz == null)
            {
                return NotFound();
            }

            // Loop through the submitted answers and check correctness
            int correctAnswersCount = 0;
            foreach (var userAnswer in userAnswers)
            {
                var question = Quiz.Questions.FirstOrDefault(q => q.QuestionID == userAnswer.QuestionID);
                if (question != null)
                {
                    // Find the correct answer for the question
                    var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);
                    if (correctAnswer != null && correctAnswer.AnswerID == userAnswer.SelectedAnswerID)
                    {
                        correctAnswersCount++;
                    }
                }
            }

            // You can store or display the results as needed
            TempData["Message"] = $"You got {correctAnswersCount} out of {Quiz.Questions.Count} correct.";

            return RedirectToPage("/Result", new { id });
        }
    }

    // A helper class to hold user answers
    public class UserAnswer
    {
        public int QuestionID { get; set; }
        public int SelectedAnswerID { get; set; }
    }
}
