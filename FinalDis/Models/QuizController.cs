using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DissertationProject.Models;
using FinalDis.Data;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace DissertationProject.Controllers
{
    public class QuizController : Controller
    {
        private readonly FinalDisContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public QuizController(FinalDisContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Action to display the quiz
        public async Task<IActionResult> Index()
        {
            // Fetch topics and quizzes from the database
            var quizzes = await _context.Quizzes
                .Include(q => q.Topic)
                .ToListAsync();

            return View(quizzes);
        }

        // POST action to handle quiz submission
        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> SubmitQuiz(int quizId, List<int> selectedAnswers)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToAction("Login", "Account"); // Redirect to login if the user is not logged in
            }

            // Fetch the quiz and its related questions and answers
            var quiz = await _context.Quizzes
                .Include(q => q.Questions)
                    .ThenInclude(q => q.Answers)
                .FirstOrDefaultAsync(q => q.QuizID == quizId);

            if (quiz == null)
            {
                return NotFound();
            }

            int correctAnswersCount = 0;

            // Evaluate each question and check for correct answers
            foreach (var question in quiz.Questions)
            {
                // Get the selected answer IDs for this question
                var selectedAnswerIds = selectedAnswers.Where(ans => ans == question.QuestionID).ToList();

                // Check if the selected answer is correct
                foreach (var answerId in selectedAnswerIds)
                {
                    var answer = question.Answers.FirstOrDefault(a => a.AnswerID == answerId && a.IsCorrect);
                    if (answer != null)
                    {
                        correctAnswersCount++;
                    }
                }
            }

            // Assuming 10 points per correct answer
            int pointsEarned = correctAnswersCount * 10;

            // Award points to the user
            await AwardPointsAsync(user, pointsEarned);

            // Store the result message
            TempData["Message"] = $"You earned {pointsEarned} points for completing the quiz.";

            return RedirectToAction("Result");
        }

        private async Task AwardPointsAsync(IdentityUser user, int pointsEarned)
        {
            var userPoints = await _context.UserPoints
                .FirstOrDefaultAsync(up => up.UserId == user.Id);

            if (userPoints == null)
            {
                userPoints = new UserPoints
                {
                    UserId = user.Id,
                    Points = 0
                };
                _context.UserPoints.Add(userPoints);
            }

            userPoints.Points += pointsEarned; // Add points to existing total
            await _context.SaveChangesAsync();
        }


        public async Task<IActionResult> Result()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            var userPoints = await _context.UserPoints
                .FirstOrDefaultAsync(up => up.UserId == user.Id);

            ViewData["Message"] = TempData["Message"]?.ToString();
            ViewData["Points"] = userPoints?.Points ?? 0;

            return View();
        }

    }
}
