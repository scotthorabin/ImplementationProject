namespace DissertationProject.Models
{
    public class Topic
    {
        public int TopicID { get; set; }
        public string TopicName { get; set; }
        public string Description { get; set; }
        public List<Quiz> Quizzes { get; set; }
        public string WhatItIs { get; set; }

        public string Concepts { get; set; }
    }

}
