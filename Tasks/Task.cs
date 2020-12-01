using System;

namespace Tasks
{
    public class Task
    {
        public string Description { get; }
        private DateTime CompletionDate { get; }
        private string Priority { get; }

        public Task(string description, DateTime completionDate, string priority)
        {
            Description = description;
            CompletionDate = completionDate;
            Priority = priority;
        }
    }
}