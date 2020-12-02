using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
    public class StateNew : State
    {
        private readonly string Name = "новая";
        private readonly Task _task;

        public StateNew(Task task) => _task = task;

        public void InProgress()
        {
            _task.ChangeState(new StateInProgress(_task));
            Console.WriteLine("Теперь статус задачи {0}:", _task._status);
        }

        public void Done()
        {
            Console.WriteLine("Статус задачи нельзя изменить на \"Выполнено\"");
        }

        public void Remove(List<Task> tasks)
        {
            tasks.Remove(_task);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}