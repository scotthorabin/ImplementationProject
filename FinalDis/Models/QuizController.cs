using DissertationProject.Models;
using FinalDis.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        var quizzes = await _context.Quizzes
            .Include(q => q.Topic)
            .ToListAsync();

        return View(quizzes);
    }

    public async Task<IActionResult> SubmitQuiz(int quizId, List<int> selectedAnswers)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");  // Redirect to login if not logged in
        }

        var quiz = await _context.Quizzes
            .Include(q => q.Questions)
                .ThenInclude(q => q.Answers)
            .FirstOrDefaultAsync(q => q.QuizID == quizId);

        if (quiz == null)
        {
            return NotFound();
        }

        int correctAnswersCount = 0;

        foreach (var question in quiz.Questions)
        {
            foreach (var answer in question.Answers)
            {
                if (selectedAnswers.Contains(answer.AnswerID) && answer.IsCorrect)
                {
                    correctAnswersCount++;
                }
            }
        }

        int pointsEarned = correctAnswersCount * 10;

        if (correctAnswersCount == quiz.Questions.Count)
        {
            await AwardBadgeAsync(user, "PerfectQuizTaker");
        }

        // ✅ NEW LOGIC HERE: First-time quiz completion badge
        var hasCompletedBadge = await _context.UserAchievements
            .AnyAsync(ua => ua.UserId == user.Id && ua.Badge == "FirstQuizCompleted");

        if (!hasCompletedBadge)
        {
            await AwardBadgeAsync(user, "FirstQuizCompleted");
        }

        TempData["Message"] = $"You earned {pointsEarned} points for completing the quiz.";

        return RedirectToAction("Result");
    }


    private async Task AwardBadgeAsync(IdentityUser user, string badge)
    {
        // Check if the user already has the badge
        var existingBadge = await _context.UserAchievements
            .FirstOrDefaultAsync(ua => ua.UserId == user.Id && ua.Badge == badge);

        // Only award the badge if the user hasn't already earned it
        if (existingBadge == null)
        {
            var userAchievement = new UserAchievement
            {
                UserId = user.Id,
                Badge = badge,
                DateAchieved = DateTime.UtcNow
            };

            _context.UserAchievements.Add(userAchievement);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<IActionResult> Result()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");  // Redirect to login if not logged in
        }

        // Get the user's achievements (badges earned)
        var userAchievements = await _context.UserAchievements
            .Where(ua => ua.UserId == user.Id)
            .ToListAsync();

        ViewData["Message"] = TempData["Message"]?.ToString();
        ViewData["UserAchievements"] = userAchievements;  // Pass achievements to the view

        return View();
    }

}

