using System;
using System.Collections.Generic;

namespace Tasks
{
    public class Task : State
    {
        public string Description { get; }
        public DateTime CompletionDate { get; }
        public string Priority { get; }
        public State _status;

        public Task(string description, DateTime completionDate, string priority)
        {
            Description = description;
            CompletionDate = completionDate;
            Priority = priority;
            _status = new StateNew(this);
        }
        
        public void ChangeState(State other_status) => _status = other_status;

        public void InProgress() => _status.InProgress();

        public void Done() => _status.Done();

        public void Remove(List<Task> tasks) => _status.Remove(tasks);
    }
}