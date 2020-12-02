using System;
using System.Collections.Generic;

namespace Tasks
{
    public class StateInProgress : State
    {
        private readonly string Name = "в работе";
        private readonly Task _task;

        public StateInProgress(Task task) => _task = task;

        public void InProgress()
        {
            Console.WriteLine("Задача и так в работе");
        }

        public void Done()
        {
            _task.ChangeState(new StateDone());
        }

        public void Remove(List<Task> tasks)
        {
            Console.WriteLine("Задачу нельзя удалить!");
        }
        
        public override string ToString()
        {
            return Name;
        }
    }
}