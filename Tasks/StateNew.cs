using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
    public class StateNew : State
    {
        public readonly string Name = "новая";

        private TaskManager _manager;

        public StateNew(TaskManager manager)
        {
            _manager = manager;
        }

        public void InProgress()
        {
            // _manager.ChangeState(new StateInProgress(_manager));
        }

        public void Done()
        {
            Console.WriteLine("Статус задачи нельзя изменить на \"Выполнено\"");
        }

        public void Remove()
        {
            if (_manager._tasks.Count > 0)
            {
                var tasks = _manager._tasks;
                Console.WriteLine("Выберите задачу");
                for (var i = 0; i < tasks.Count; i++)
                {
                    Console.WriteLine("{0}: {1}", i + 1, tasks[i].Description);
                }

                var taskIndex = _manager.GetNumber();
                if (Enumerable.Range(0, tasks.Count).Contains(taskIndex))
                {
                    var task = tasks[taskIndex - 1];
                    tasks.Remove(task);
                    _manager._tasks = tasks;
                }
                else
                {
                    Console.WriteLine("Такой задачи нет");
                }
            }
            else
            {
                Console.WriteLine("Задач для удаления нет!");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}