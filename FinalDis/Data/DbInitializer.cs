using DissertationProject.Models;
using FinalDis.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    public static void Initialize(FinalDisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        context.Database.Migrate();

        SeedTopics(context);
        SeedQuizzes(context);
        SeedQuestions(context);
        SeedUserAchievements(context);
        SeedRoles(roleManager);
        SeedUsers(userManager, roleManager, context);
    }

    private static void SeedRoles(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { "Admin", "User", "Manager" };
        foreach (var role in roles)
        {
            var roleExist = roleManager.RoleExistsAsync(role).Result;
            if (!roleExist)
            {
                roleManager.CreateAsync(new IdentityRole(role)).Wait();
            }
        }
    }

    private static void SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, FinalDisContext context)
    {
        var users = new[]
        {
            new { Email = "admin@admin.com", UserName = "admin@admin.com", Role = "Admin" },
            new { Email = "user@user.com", UserName = "user@user.com", Role = "User" },
            new { Email = "Test@Test", UserName = "Test@Test", Role = "User" }
        };

        foreach (var userInfo in users)
        {
            var user = userManager.FindByEmailAsync(userInfo.Email).Result;
            if (user == null)
            {
                user = new IdentityUser
                {
                    UserName = userInfo.UserName,
                    Email = userInfo.Email,
                };

                var result = userManager.CreateAsync(user, "Test@123").Result;
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, userInfo.Role).Wait();
                    var userPoints = new UserPoints
                    {
                        UserId = user.Id,
                        Points = 0
                    };
                    context.UserPoints.Add(userPoints);
                    context.SaveChanges();
                }
            }
        }
    }

    private static void SeedTopics(FinalDisContext context)
    {
        if (!context.Topics.Any())
        {
            var topics = new List<Topic>
            {
                new Topic
                {
                    TopicName = "Object-Oriented Programming",
                    Description = "Intro to OOP concepts.",
                    WhatItIs = "Object-Oriented Programming (OOP) is a programming paradigm based on the concept of objects, which contain data in the form of fields and code in the form of procedures.",
                    Concepts = "Classes, Objects, Inheritance, Polymorphism",
                    Information = "OOP is a way of organizing code into 'objects'—self-contained units that include both data and behavior. The four main principles of OOP are:\n\n- Encapsulation: Bundling data and methods.\n- Abstraction: Hiding complexity.\n- Inheritance: Deriving new classes.\n- Polymorphism: Objects taking multiple forms."
                },
                new Topic
                {
                    TopicName = "Data Structures",
                    Description = "Learn about lists, stacks, and queues.",
                    WhatItIs = "Data structures are ways of organizing and storing data to perform operations efficiently.",
                    Concepts = "Stacks, Queues, Linked Lists, Arrays",
                    Information = "A data structure is a collection of data values and operations on them. Key types include:\n\n- Stacks: Last-in, first-out (LIFO) structure.\n- Queues: First-in, first-out (FIFO) structure.\n- Linked Lists: Linear data structure with nodes connected by links.\n- Arrays: Contiguous memory location."
                },
                new Topic
                {
                    TopicName = "Algorithms",
                    Description = "Study basic algorithms used in programming.",
                    WhatItIs = "Algorithms are step-by-step procedures for solving a problem or performing a task.",
                    Concepts = "Sorting, Searching, Recursion, Time Complexity",
                    Information = "Algorithms help us solve problems efficiently. Types include:\n\n- Sorting: Arranging data in a specific order (e.g., Bubble Sort, Merge Sort).\n- Searching: Finding elements in a data structure (e.g., Binary Search).\n- Recursion: A function calling itself.\n- Time Complexity: Analyzing how an algorithm scales with input size."
                },
                new Topic
                {
                    TopicName = "Web Development",
                    Description = "Learn how to build websites and web apps.",
                    WhatItIs = "Web development involves designing and creating websites and web applications.",
                    Concepts = "HTML, CSS, JavaScript, Frontend, Backend",
                    Information = "Web development is divided into:\n\n- Frontend: What users see (HTML, CSS, JavaScript).\n- Backend: Server-side logic (Node.js, PHP, databases).\n- Full Stack: Combines both frontend and backend development."
                },
                new Topic
                {
                    TopicName = "Databases",
                    Description = "Understand relational and non-relational databases.",
                    WhatItIs = "Databases store, manage, and retrieve data using structured methods.",
                    Concepts = "SQL, NoSQL, CRUD operations, Indexing, Normalization",
                    Information = "Databases help manage vast amounts of data. Types include:\n\n- SQL: Structured Query Language (relational databases).\n- NoSQL: Non-relational databases (e.g., MongoDB).\n- CRUD: Create, Read, Update, Delete operations.\n- Normalization: Reducing redundancy."
                },
                new Topic
                {
                    TopicName = "Mobile Development",
                    Description = "Create applications for mobile platforms.",
                    WhatItIs = "Mobile app development involves creating software for mobile devices.",
                    Concepts = "Android, iOS, React Native, Flutter",
                    Information = "Mobile apps can be developed for different platforms:\n\n- Android: Uses Java/Kotlin.\n- iOS: Uses Swift/Objective-C.\n- Cross-Platform: React Native, Flutter for building apps that run on both Android and iOS."
                },
                new Topic
                {
                    TopicName = "Operating Systems",
                    Description = "Understand how operating systems work.",
                    WhatItIs = "Operating systems manage computer hardware and software resources.",
                    Concepts = "Processes, Memory Management, File Systems, Scheduling",
                    Information = "An operating system is the software that supports basic functions:\n\n- Processes: Running programs.\n- Memory Management: Allocating system memory.\n- File Systems: Organizing data storage.\n- Scheduling: Managing CPU time for processes."
                },
                new Topic
                {
                    TopicName = "Cybersecurity",
                    Description = "Learn about securing data and systems.",
                    WhatItIs = "Cybersecurity is protecting systems and networks from digital attacks.",
                    Concepts = "Encryption, Firewalls, Malware, Authentication",
                    Information = "Cybersecurity is crucial to safeguard data and systems:\n\n- Encryption: Protecting data using algorithms.\n- Firewalls: Preventing unauthorized access.\n- Malware: Malicious software.\n- Authentication: Verifying user identity."
                },
                new Topic
                {
                    TopicName = "Cloud Computing",
                    Description = "Learn the principles behind cloud-based systems.",
                    WhatItIs = "Cloud computing involves using remote servers to store, manage, and process data.",
                    Concepts = "AWS, Azure, GCP, Virtualization, Cloud Services",
                    Information = "Cloud services provide remote infrastructure:\n\n- AWS: Amazon Web Services.\n- Azure: Microsoft's cloud platform.\n- GCP: Google Cloud Platform.\n- Virtualization: Running multiple virtual machines on a physical server."
                },
                new Topic
                {
                    TopicName = "Machine Learning",
                    Description = "Understand the basics of machine learning algorithms.",
                    WhatItIs = "Machine learning enables computers to learn from data and improve over time.",
                    Concepts = "Supervised Learning, Unsupervised Learning, Neural Networks, Regression",
                    Information = "Machine learning algorithms include:\n\n- Supervised Learning: Training models on labeled data.\n- Unsupervised Learning: Finding patterns in unlabeled data.\n- Neural Networks: Algorithms modeled after the human brain.\n- Regression: Predicting continuous values."
                }
            };

            context.Topics.AddRange(topics);
            context.SaveChanges();
        }
    }

    private static void SeedQuizzes(FinalDisContext context)
    {
        if (!context.Quizzes.Any())
        {
            var quizzes = new List<Quiz>
            {
                new Quiz { QuizTitle = "OOP Concepts Quiz", TopicID = context.Topics.First(t => t.TopicName == "Object-Oriented Programming").TopicID },
                new Quiz { QuizTitle = "Data Structures Quiz", TopicID = context.Topics.First(t => t.TopicName == "Data Structures").TopicID },
                new Quiz { QuizTitle = "Algorithms Quiz", TopicID = context.Topics.First(t => t.TopicName == "Algorithms").TopicID },
                new Quiz { QuizTitle = "Web Development Quiz", TopicID = context.Topics.First(t => t.TopicName == "Web Development").TopicID },
                new Quiz { QuizTitle = "Databases Quiz", TopicID = context.Topics.First(t => t.TopicName == "Databases").TopicID },
                new Quiz { QuizTitle = "Mobile Development Quiz", TopicID = context.Topics.First(t => t.TopicName == "Mobile Development").TopicID },
                new Quiz { QuizTitle = "Operating Systems Quiz", TopicID = context.Topics.First(t => t.TopicName == "Operating Systems").TopicID },
                new Quiz { QuizTitle = "Cybersecurity Quiz", TopicID = context.Topics.First(t => t.TopicName == "Cybersecurity").TopicID },
                new Quiz { QuizTitle = "Cloud Computing Quiz", TopicID = context.Topics.First(t => t.TopicName == "Cloud Computing").TopicID },
                new Quiz { QuizTitle = "Machine Learning Quiz", TopicID = context.Topics.First(t => t.TopicName == "Machine Learning").TopicID }
            };
            context.Quizzes.AddRange(quizzes);
            context.SaveChanges();
        }
    }

    private static void SeedQuestions(FinalDisContext context)
    {
        if (!context.Questions.Any())
        {
            var quiz1 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "OOP Concepts Quiz");
            var quiz2 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "Data Structures Quiz");
            var quiz3 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "Web Development Quiz");
            var quiz4 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "Algorithms Quiz");

            if (quiz1 != null)
            {
                var questionsQuiz1 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What is inheritance in OOP?",
                        QuizID = quiz1.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "A way for a class to inherit properties and methods from another.", IsCorrect = true },
                            new Answer { AnswerText = "A data structure.", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is Polymorphism?",
                        QuizID = quiz1.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "Ability of objects to take many forms.", IsCorrect = true },
                            new Answer { AnswerText = "The process of structuring data.", IsCorrect = false }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz1);
                context.SaveChanges();
            }

            if (quiz2 != null)
            {
                var questionsQuiz2 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What is a stack data structure?",
                        QuizID = quiz2.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "A data structure where elements are added and removed from the same end.", IsCorrect = true },
                            new Answer { AnswerText = "A list of ordered elements.", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What is a queue data structure?",
                        QuizID = quiz2.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "A data structure where elements are added at the back and removed from the front.", IsCorrect = true },
                            new Answer { AnswerText = "A data structure where elements are removed from the back.", IsCorrect = false }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz2);
                context.SaveChanges();
            }

            if (quiz3 != null)
            {
                var questionsQuiz3 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What is the correct HTML tag to define a hyperlink?",
                        QuizID = quiz3.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "<a>", IsCorrect = true },
                            new Answer { AnswerText = "<link>", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "Which of the following is a CSS property?",
                        QuizID = quiz3.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "background-color", IsCorrect = true },
                            new Answer { AnswerText = "font-size", IsCorrect = true },
                            new Answer { AnswerText = "text-align", IsCorrect = true },
                            new Answer { AnswerText = "None of the above", IsCorrect = false }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz3);
                context.SaveChanges();
            }

            if (quiz4 != null)
            {
                var questionsQuiz4 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What is the time complexity of bubble sort?",
                        QuizID = quiz4.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "O(n^2)", IsCorrect = true },
                            new Answer { AnswerText = "O(n log n)", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "Which of the following algorithms is used for sorting?",
                        QuizID = quiz4.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "Quick Sort", IsCorrect = true },
                            new Answer { AnswerText = "Binary Search", IsCorrect = false }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz4);
                context.SaveChanges();
            }
        }
    }

    private static void SeedUserAchievements(FinalDisContext context)
    {
        var adminUser = context.Users.FirstOrDefault(u => u.Email == "admin@admin.com");
        var regularUser = context.Users.FirstOrDefault(u => u.Email == "Test@test");

        if (adminUser != null && regularUser != null)
        {
            var userAchievements = new List<UserAchievement>
            {
                new UserAchievement
                {
                    UserId = adminUser.Id,
                    Badge = "PerfectQuizTaker",
                    DateAchieved = DateTime.UtcNow
                },
                new UserAchievement
                {
                    UserId = regularUser.Id,
                    Badge = "FirstQuizCompleted",
                    DateAchieved = DateTime.UtcNow
                }
            };

            foreach (var achievement in userAchievements)
            {
                var existingAchievement = context.UserAchievements
                    .FirstOrDefault(ua => ua.UserId == achievement.UserId && ua.Badge == achievement.Badge);

                if (existingAchievement == null)
                {
                    context.UserAchievements.Add(achievement);
                    Console.WriteLine($"Achievement {achievement.Badge} added for user {achievement.UserId}");
                }
                else
                {
                    Console.WriteLine($"Achievement {achievement.Badge} already exists for user {achievement.UserId}");
                }
            }

            context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Error: One or more users not found.");
        }
    }
}
