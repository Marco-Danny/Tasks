using System;

namespace Tasks
{
    public class Task
    {
        public string Description { get; }
        public DateTime CompletionDate { get; }
        public string Priority { get; }
        public State _status;
        
        // TODO не понятно почему работает, всегда = null.
        private TaskManager _manager;

        public Task(string description, DateTime completionDate, string priority)
        {
            Description = description;
            CompletionDate = completionDate;
            Priority = priority;
            _status = new StateNew(_manager);
        }
    }
}