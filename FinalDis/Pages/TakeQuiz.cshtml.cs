using DissertationProject.Models;
using FinalDis.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace DissertationProject.Pages.Quizzes
{
    public class TakeQuizModel : PageModel
    {
        private readonly FinalDisContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public Quiz Quiz { get; set; }
        public List<UserAnswer> UserAnswers { get; set; }

        public TakeQuizModel(FinalDisContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Quiz = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuizID == id);

            if (Quiz == null)
            {
                return NotFound();
            }

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
        public async Task<IActionResult> OnPostAsync(int id, List<UserAnswer> userAnswers)
        {
            var user = await _userManager.GetUserAsync(User);

            // Fetches the quiz with its questions and answers, along with the associated topic
            Quiz = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .Include(q => q.Topic)  // Include the Topic related to the quiz
                .FirstOrDefaultAsync(q => q.QuizID == id);

            if (Quiz == null)
            {
                return NotFound();
            }

            // Counts the number of correct answers
            int correctAnswersCount = 0;
            foreach (var userAnswer in userAnswers)
            {
                var question = Quiz.Questions.FirstOrDefault(q => q.QuestionID == userAnswer.QuestionID);
                if (question != null)
                {
                    var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);
                    if (correctAnswer != null && correctAnswer.AnswerID == userAnswer.SelectedAnswerID)
                    {
                        correctAnswersCount++;
                    }
                }
            }

            // Checks if user got all questions correct
            if (correctAnswersCount == Quiz.Questions.Count)
            {
                // New badge
                var badgeName = $"QuizCompleted_{Quiz.Topic.TopicName}";

                // Check if the user has already received the badge for this topic
                var alreadyHasBadge = await _context.UserAchievements
                    .AnyAsync(a => a.UserId == user.Id && a.Badge == badgeName);

                if (!alreadyHasBadge)
                {
                    // Create a new achievement record for the quiz completion
                    var achievement = new UserAchievement
                    {
                        UserId = user.Id,
                        Badge = badgeName,  // Use the dynamic badge name with topic name
                        DateAchieved = DateTime.UtcNow
                    };

                    // Save changes
                    _context.UserAchievements.Add(achievement);
                    await _context.SaveChangesAsync();
                }
            }

            // Message that calculates and displays correct answers out of total questions
            TempData["Message"] = $"You got {correctAnswersCount} out of {Quiz.Questions.Count} right.";

            // Redirect to the Result page to display the result
            return RedirectToPage("/Result", new { id });
        }





        public class UserAnswer
        {
            public int QuestionID { get; set; }
            public int SelectedAnswerID { get; set; }
        }
    }
}
