using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
    public class StateNew : State
    {
        public readonly string Name = "новая";
        private Task _task;

        private TaskManager _manager;

        public StateNew(Task task) => _task = task;

        public void InProgress()
        {
            // _manager.ChangeState(new StateInProgress(_manager));
        }

        public void Done()
        {
            Console.WriteLine("Статус задачи нельзя изменить на \"Выполнено\"");
        }

        public void Remove(List<Task> tasks, Task task)
        {
            tasks.Remove(task);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}