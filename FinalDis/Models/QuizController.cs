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

    public async Task<IActionResult> Index()
    {
        var quizzes = await _context.Quizzes.Include(q => q.Topic).ToListAsync();
        return View(quizzes);
    }

    public async Task<IActionResult> SubmitQuiz(int quizId, List<int> selectedAnswers)
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
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
            var correctAnswer = question.Answers.FirstOrDefault(a => a.IsCorrect);
            if (correctAnswer != null && selectedAnswers.Contains(correctAnswer.AnswerID))
            {
                correctAnswersCount++;
            }
        }

        TempData["Message"] = $"You got {correctAnswersCount} out of {quiz.Questions.Count} correct.";

        // Checks if the user already has the Achievement
        var alreadyHasAchievement = await _context.UserAchievements
            .AnyAsync(a => a.UserId == user.Id && a.Badge == "FirstQuizCompleted");

        if (!alreadyHasAchievement)
        {
            // Give them the "achievement
            var achievement = new UserAchievement
            {
                UserId = user.Id,
                Badge = "FirstQuizCompleted",
                DateAchieved = DateTime.UtcNow
            };

            _context.UserAchievements.Add(achievement);
            await _context.SaveChangesAsync();
        }

        return RedirectToAction("Result");
    }

    public async Task<IActionResult> Result()
    {
        var user = await _userManager.GetUserAsync(User);
        if (user == null) return RedirectToAction("Login", "Account");

        var userAchievements = await _context.UserAchievements
            .Where(a => a.UserId == user.Id)
            .ToListAsync();

        ViewData["Message"] = TempData["Message"];
        ViewData["UserAchievements"] = userAchievements;

        return View();
    }

    public async Task<IActionResult> Achievements()
    {
        // Get the currently logged-in user
        var user = await _userManager.GetUserAsync(User);
        if (user == null)
        {
            return RedirectToAction("Login", "Account");
        }

        // Fetch all achievements associated with the user
        var userAchievements = await _context.UserAchievements
            .Where(a => a.UserId == user.Id)
            .ToListAsync();

        // Pass the achievements to the view
        ViewData["UserAchievements"] = userAchievements;

        return View();
    }

}


