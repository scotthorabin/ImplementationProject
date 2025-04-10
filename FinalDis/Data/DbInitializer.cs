using DissertationProject.Models;
using FinalDis.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

public static class DbInitializer
{
    // Initialize method to apply migrations and seed data
    public static void Initialize(FinalDisContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Apply any pending migrations
        context.Database.Migrate();

        // Seed Topics
        SeedTopics(context);

        // Seed Quizzes
        SeedQuizzes(context);

        // Seed Questions
        SeedQuestions(context);

        // Seed Answers
        SeedAnswers(context);

        // Seed Identity Roles
        SeedRoles(roleManager);

        // Seed Users
        SeedUsers(userManager, roleManager, context);
    }

    // Method to seed roles
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

    // Method to seed users
    private static void SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, FinalDisContext context)
    {
        var users = new[]
        {
            new { Email = "admin@admin.com", UserName = "admin@admin.com", Role = "Admin" },
            new { Email = "user@user.com", UserName = "user@user.com", Role = "User" }
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

                    // Initialize points for the user
                    var userPoints = new UserPoints
                    {
                        UserId = user.Id,
                        Points = 0 // Start with 0 points
                    };
                    context.UserPoints.Add(userPoints);
                    context.SaveChanges();
                }
            }
        }
    }

    // Method to seed topics
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
                    WhatItIs = "Detailed explanation of OOP.",
                    Concepts = "Classes, Objects, Inheritance, Polymorphism"
                },
                new Topic
                {
                    TopicName = "Data Structures",
                    Description = "Learn about lists, stacks, and queues.",
                    WhatItIs = "Detailed explanation of data structures.",
                    Concepts = "Stacks, Queues, Linked Lists, Arrays"
                },
                new Topic
                {
                    TopicName = "Algorithms",
                    Description = "Study basic algorithms used in programming.",
                    WhatItIs = "Explanation of common algorithms and their applications.",
                    Concepts = "Sorting, Searching, Recursion, Time Complexity"
                },
                new Topic
                {
                    TopicName = "Web Development",
                    Description = "Learn how to build websites and web apps.",
                    WhatItIs = "Fundamentals of web development.",
                    Concepts = "HTML, CSS, JavaScript, Frontend, Backend"
                },
                new Topic
                {
                    TopicName = "Databases",
                    Description = "Understand relational and non-relational databases.",
                    WhatItIs = "Learn how databases work and how to interact with them.",
                    Concepts = "SQL, NoSQL, CRUD operations, Indexing, Normalization"
                },
                new Topic
                {
                    TopicName = "Mobile Development",
                    Description = "Create applications for mobile platforms.",
                    WhatItIs = "Learn mobile application development.",
                    Concepts = "Android, iOS, React Native, Flutter"
                },
                new Topic
                {
                    TopicName = "Operating Systems",
                    Description = "Understand how operating systems work.",
                    WhatItIs = "Detailed explanation of OS components and their functions.",
                    Concepts = "Processes, Memory Management, File Systems, Scheduling"
                },
                new Topic
                {
                    TopicName = "Cybersecurity",
                    Description = "Learn about securing data and systems.",
                    WhatItIs = "Introduction to cybersecurity fundamentals.",
                    Concepts = "Encryption, Firewalls, Malware, Authentication"
                },
                new Topic
                {
                    TopicName = "Cloud Computing",
                    Description = "Learn the principles behind cloud-based systems.",
                    WhatItIs = "Introduction to cloud computing technologies.",
                    Concepts = "AWS, Azure, GCP, Virtualization, Cloud Services"
                },
                new Topic
                {
                    TopicName = "Machine Learning",
                    Description = "Understand the basics of machine learning algorithms.",
                    WhatItIs = "An introduction to machine learning and its applications.",
                    Concepts = "Supervised Learning, Unsupervised Learning, Neural Networks, Regression"
                }
            };
            context.Topics.AddRange(topics);
            context.SaveChanges();
        }
    }

    // Method to seed quizzes
    private static void SeedQuizzes(FinalDisContext context)
    {
        if (!context.Quizzes.Any())
        {
            var quizzes = new List<Quiz>
            {
                new Quiz
                {
                    QuizTitle = "OOP Concepts Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Object-Oriented Programming").TopicID
                },
                new Quiz
                {
                    QuizTitle = "Data Structures Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Data Structures").TopicID
                },
                new Quiz
                {
                    QuizTitle = "Algorithms Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Algorithms").TopicID
                },
                new Quiz
                {
                    QuizTitle = "Web Development Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Web Development").TopicID
                },
                new Quiz
                {
                    QuizTitle = "Databases Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Databases").TopicID
                },
                new Quiz
                {
                    QuizTitle = "Mobile Development Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Mobile Development").TopicID
                },
                new Quiz
                {
                    QuizTitle = "Operating Systems Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Operating Systems").TopicID
                },
                new Quiz
                {
                    QuizTitle = "Cybersecurity Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Cybersecurity").TopicID
                },
                new Quiz
                {
                    QuizTitle = "Cloud Computing Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Cloud Computing").TopicID
                },
                new Quiz
                {
                    QuizTitle = "Machine Learning Quiz",
                    TopicID = context.Topics.First(t => t.TopicName == "Machine Learning").TopicID
                }
            };
            context.Quizzes.AddRange(quizzes);
            context.SaveChanges();
        }
    }

    // Method to seed questions
    private static void SeedQuestions(FinalDisContext context)
    {
        if (!context.Questions.Any())
        {
            // Get the quizzes that were seeded
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
                Console.WriteLine("OOP Concepts Quiz questions seeded successfully.");
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
                Console.WriteLine("Data Structures Quiz questions seeded successfully.");
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
                Console.WriteLine("Web Development Quiz questions seeded successfully.");
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
                Console.WriteLine("Algorithms Quiz questions seeded successfully.");
            }
        }
    }


    // Method to seed answers
    private static void SeedAnswers(FinalDisContext context)
    {
        // Answers are already seeded within questions, but if needed,
        // they can be added separately here.
    }
}
