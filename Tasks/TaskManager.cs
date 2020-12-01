using System;
using System.Collections.Generic;
using System.Linq;

namespace Tasks
{
    public class TaskManager : State
    {
        private State _state;

        // public List<Task> _tasks = new List<Task>();
        public List<Task> _tasks = new List<Task>()
        {
            new Task("Купить хлеб", DateTime.Now, "низкий"),
            new Task("Купить молоко", DateTime.Now, "низкий"),
            new Task("Погулять с собакой", DateTime.Now, "низкий"),
            new Task("Заказать ресторан", DateTime.Now, "высокий")
        };

        public TaskManager()
        {
            _state = new StateNew(this);
        }

        private void AddNewTask()
        {
            var description = GetDescriptionTask();
            var dateTime = GetDate();
            var priority = GetPriority();
            Task task = new Task(description, dateTime, priority);
            _tasks.Add(task);
        }

        private string GetDescriptionTask()
        {
            string description;
            while (true)
            {
                Console.WriteLine("Введите описание задачи:");
                var inputDescription = Console.ReadLine();
                if (inputDescription == "")
                {
                    Console.WriteLine("Нет описания задачи");
                }
                else
                {
                    description = inputDescription;
                    break;
                }
            }

            return description;
        }

        private DateTime GetDate()
        {
            int day;
            int month;
            int year;
            while (true)
            {
                Console.WriteLine("Введите дату завершения задачи в формате (DD.MM.YYYY):");
                var input = Console.ReadLine();
                var split = input.Split('.');
                try
                {
                    day = int.Parse(split[0]);
                    month = int.Parse(split[1]);
                    year = int.Parse(split[2]);
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Неверно введена дата");
                }
            }

            DateTime date1 = new DateTime(year, month, day);
            return date1;
        }

        private string GetPriority()
        {
            string[] priorities = {"низкий", "средний", "высокий"};
            var priority = priorities[0];

            Console.WriteLine("Выбирите приоритет задачи:");
            for (var i = 0; i < priorities.Length; i++)
            {
                Console.WriteLine("{0}: {1}", priorities[i], i + 1);
            }

            while (true)
            {
                var inputPriority = Console.ReadLine();
                if (inputPriority == "")
                {
                    return priority;
                }

                try
                {
                    var userChoice = int.Parse(inputPriority);
                    if (userChoice < 1 || userChoice > priorities.Length)
                    {
                        Console.WriteLine("Введены не верные данные");
                    }
                    else
                    {
                        priority = priorities[userChoice - 1];
                        break;
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Введены не верные данные");
                }
            }

            return priority;
        }

        public void ShowAllTasks()
        {
            Console.WriteLine("Задачи:");
            foreach (var task in _tasks)
            {
                Console.WriteLine(task.Description);
            }

            Console.WriteLine("____________________");
        }

        public int GetNumber()
        {
            while (true)
            {
                try
                {
                    var choice = int.Parse(Console.ReadLine());
                    return choice;
                }
                catch (Exception)
                {
                    Console.WriteLine("Нужно ввести число");
                }
            }
        }

        private void ShowATask()
        {
            int taskIndex;
            Console.WriteLine("Выберите задачу");
            for (var i = 0; i < _tasks.Count; i++)
            {
                Console.WriteLine("{0}: {1}", i + 1, _tasks[i].Description);
            }

            taskIndex = GetNumber();
            if (Enumerable.Range(0, _tasks.Count + 1).Contains(taskIndex))
            {
                var task = _tasks[taskIndex - 1];
                Console.WriteLine("Задача:\t\t\t {0}", task.Description);
                Console.WriteLine("Приоритет:\t\t {0}", task.Priority);
                Console.WriteLine("Статус:\t\t\t {0}", task._status);
                Console.WriteLine("Должна быть выполнена к: {0}", task.CompletionDate);
            }
            else
            {
                Console.WriteLine("Такой задачи нет");
            }

            Console.WriteLine("-------------------------");
        }

        public void ChangeState(State other_state) => _state = other_state;

        public void InProgress() => _state.InProgress();

        public void Done() => _state.Done();

        public void Remove()
        {
            _state.Remove();
            ShowAllTasks();
            Console.WriteLine("-------------------");
        }

        private static void ShowMenuVariants()
        {
            Console.WriteLine("Выберите вариант:");
            List<string> variants = new List<string>()
            {
                "Создать новую задчу",
                "Отобразить все задачи",
                "Отобразить задачу",
                "Удалить задачу",
                // "Изменить статус задачи"
            };

            for (var i = 0; i < variants.Count; i++)
            {
                Console.WriteLine("{0}: {1}", i + 1, variants[i]);
            }
        }

        public void Menu()
        {
            while (true)
            {
                ShowMenuVariants();
                var choice = GetNumber();
                switch (choice)
                {
                    case 1:
                        AddNewTask();
                        break;
                    case 2:
                        ShowAllTasks();
                        break;
                    case 3:
                        ShowATask();
                        break;
                    case 4:
                        Remove();
                        break;
                }
            }
        }
    }
}