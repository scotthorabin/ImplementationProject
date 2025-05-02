using DissertationProject.Models;
using FinalDis.Data;
using FinalDis.Models;
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
        SeedFAQ(context);
    }

    // New Topics

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
            var quiz5 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "Databases Quiz");
            var quiz6 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "Mobile Development Quiz");
            var quiz7 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "Operating Systems Quiz");
            var quiz8 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "Cybersecurity Quiz");
            var quiz9 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "Cloud Computing Quiz");
            var quiz10 = context.Quizzes.FirstOrDefault(q => q.QuizTitle == "Machine Learning Quiz");

            // OOP Concepts Quiz
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

            // Data Structures Quiz
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

            // Web Development Quiz
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

            // Algorithms Quiz
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

            // Databases Quiz
            if (quiz5 != null)
            {
                var questionsQuiz5 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What does SQL stand for?",
                        QuizID = quiz5.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "Structured Query Language", IsCorrect = true },
                             new Answer { AnswerText = "Standard Question Language", IsCorrect = false },
                             new Answer { AnswerText = "Simple Query Language", IsCorrect = false },
                             new Answer { AnswerText = "Sequential Query Line", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "Which operation is NOT part of CRUD?",
                        QuizID = quiz5.QuizID,
                        Answers = new List<Answer>
                        {
                             new Answer { AnswerText = "Calculate", IsCorrect = false },
                             new Answer { AnswerText = "Create", IsCorrect = true },
                            new Answer { AnswerText = "Read", IsCorrect = true },
                            new Answer { AnswerText = "Delete", IsCorrect = true }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz5);
                context.SaveChanges();
            }

            // Mobile Development Quiz
            if (quiz6 != null)
            {
                var questionsQuiz6 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "Which language is used to develop iOS apps?",
                        QuizID = quiz6.QuizID,
                        Answers = new List<Answer>
                        {
                           new Answer { AnswerText = "Swift", IsCorrect = true },
                           new Answer { AnswerText = "Kotlin", IsCorrect = false },
                           new Answer { AnswerText = "Java", IsCorrect = false },
                           new Answer { AnswerText = "Dart", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "Which framework allows cross-platform mobile app development?",
                        QuizID = quiz6.QuizID,
                        Answers = new List<Answer>
                        {
                          new Answer { AnswerText = "Flutter", IsCorrect = true },
                          new Answer { AnswerText = "React", IsCorrect = false },
                          new Answer { AnswerText = "Angular", IsCorrect = false },
                          new Answer { AnswerText = "ASP.NET", IsCorrect = false }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz6);
                context.SaveChanges();
            }

            // Operating Systems Quiz
            if (quiz7 != null)
            {
                var questionsQuiz7 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "Which of the following is a core function of an operating system?",
                        QuizID = quiz7.QuizID,
                        Answers = new List<Answer>
                        {
                           new Answer { AnswerText = "Memory Management", IsCorrect = true },
                           new Answer { AnswerText = "Web Hosting", IsCorrect = false },
                           new Answer { AnswerText = "Data Mining", IsCorrect = false },
                           new Answer { AnswerText = "Game Design", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What does CPU scheduling refer to?",
                        QuizID = quiz7.QuizID,
                        Answers = new List<Answer>
                        {
                              new Answer { AnswerText = "Allocating CPU time to processes", IsCorrect = true },
                              new Answer { AnswerText = "Scheduling meetings", IsCorrect = false },
                              new Answer { AnswerText = "Data visualization", IsCorrect = false },
                              new Answer { AnswerText = "Creating threads", IsCorrect = false }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz7);
                context.SaveChanges();
            }

            // Cybersecurity Quiz
            if (quiz8 != null)
            {
                var questionsQuiz8 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What is the purpose of encryption?",
                        QuizID = quiz8.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "To protect data using algorithms", IsCorrect = true },
                            new Answer { AnswerText = "To remove viruses", IsCorrect = false },
                            new Answer { AnswerText = "To create user interfaces", IsCorrect = false },
                            new Answer { AnswerText = "To increase website speed", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText =  "Which of the following is considered malware?",
                        QuizID = quiz8.QuizID,
                        Answers = new List<Answer>
                        {
                              new Answer { AnswerText = "Ransomware", IsCorrect = true },
                              new Answer { AnswerText = "Firewall", IsCorrect = false },
                              new Answer { AnswerText = "VPN", IsCorrect = false },
                              new Answer { AnswerText = "Compiler", IsCorrect = false }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz8);
                context.SaveChanges();
            }
            
            // Cloud Computing Quiz
            if (quiz9 != null)
            {
                var questionsQuiz9 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "Which of the following is a cloud platform?",
                        QuizID = quiz9.QuizID,
                        Answers = new List<Answer>
                        {
                              new Answer { AnswerText = "AWS", IsCorrect = true },
                              new Answer { AnswerText = "Windows", IsCorrect = false },
                              new Answer { AnswerText = "Linux", IsCorrect = false },
                              new Answer { AnswerText = "Node.js", IsCorrect = false }
                        }
                    },
                    new Question
                    {
                        QuestionText = "What does virtualization in cloud computing mean?",
                        QuizID = quiz9.QuizID,
                        Answers = new List<Answer>
                        {
                               new Answer { AnswerText = "Running multiple virtual machines on one physical machine", IsCorrect = true },
                               new Answer { AnswerText = "Creating mobile apps", IsCorrect = false },
                               new Answer { AnswerText = "Designing graphics", IsCorrect = false },
                               new Answer { AnswerText = "Encrypting data", IsCorrect = false }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz9);
                context.SaveChanges();
            }

            // Machine Learning Quiz
            if (quiz7 != null)
            {
                var questionsQuiz10 = new List<Question>
                {
                    new Question
                    {
                        QuestionText = "What is supervised learning?",
                        QuizID = quiz10.QuizID,
                        Answers = new List<Answer>
                        {
                             new Answer { AnswerText = "Training a model on labeled data", IsCorrect = true },
                             new Answer { AnswerText = "Training on unstructured audio", IsCorrect = false },
                             new Answer { AnswerText = "Running queries on SQL", IsCorrect = false },
                             new Answer { AnswerText = "Training without any data", IsCorrect = false }

                        }
                    },
                    new Question
                    {
                        QuestionText = "Which of the following is a type of machine learning?",
                        QuizID = quiz10.QuizID,
                        Answers = new List<Answer>
                        {
                            new Answer { AnswerText = "Unsupervised Learning", IsCorrect = true },
                            new Answer { AnswerText = "Multithreading", IsCorrect = false },
                            new Answer { AnswerText = "Refactoring", IsCorrect = false },
                            new Answer { AnswerText = "Caching", IsCorrect = false }
                        }
                    }
                };
                context.Questions.AddRange(questionsQuiz10);
                context.SaveChanges();
            }

        }
    }

    // New static void that seeds the F&Q data
    private static void SeedFAQ(FinalDisContext context)
    {
        // Check if FAQ data already exists
        if (context.FAQs.Any())
        {
            return;   
        }

        // Add some FAQ data if not already seeded
        context.FAQs.AddRange(
            new FAQ
            {
                Question = "What is this website?",
                Answer = "This is a platform that allows students to revise and retain computing topics using Quizzes. Once a quiz has been completed, a achievement is geven."
            },
            new FAQ
            {
                Question = "How do I take a quiz?",
                Answer = "Simply select a quiz from the specific topic page and start answering the questions."
            },
            new FAQ
            {
                Question = "What are achievements?",
                Answer = "Achievements are badges you earn when completing certain tasks, such as finishing a quiz."
            },
            new FAQ
            {
                Question = "Can I retake a quiz?",
                Answer = "Yes, you can retake any quiz as many times as you like."
            }
        );

        context.SaveChanges();
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
