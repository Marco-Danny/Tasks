using System;

namespace Tasks
{
    public class Task
    {
        public string Description { get; }
        private DateTime CompletionDate { get; }
        private string Priority { get; }
        private State _state;

        public Task(string description, DateTime completionDate, string priority)
        {
            Description = description;
            CompletionDate = completionDate;
            Priority = priority;
        }

        void ChangeState(State other_state) => _state = other_state;

        void Inprogres() => _state.InProgress();

        void Done() => _state.Done();

        void Remove() => _state.Remove();
    }
}