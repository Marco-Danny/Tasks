using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;

namespace Tasks
{
    public class TaskManager
    {
        private State _state;

        private bool IsCorrectUserChoice<T>(int input, ICollection data) => input > 0 && input <= data.Count;

        private List<Task> _tasks = new List<Task>();
        // private List<Task> _tasks = new List<Task>()
        // {
        //     new Task("Купить хлеб", DateTime.Now, "низкий"),
        //     new Task("Купить молоко", DateTime.Now, "низкий"),
        //     new Task("Погулять с собакой", DateTime.Now, "низкий"),
        //     new Task("Заказать ресторан", DateTime.Now, "высокий")
        // };
        private bool IsHaveTask => _tasks.Count > 0;

        private void MoveOn()
        {
            Console.WriteLine("Для продолжения нажмите клавишу.");
            Console.WriteLine("________________________________");
            Console.ReadKey();
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

        private void AddNewTask()
        {
            var description = GetDescriptionTask();
            var dateTime = GetDate();
            var priority = GetPriority();
            Task task = new Task(description, dateTime, priority);
            _tasks.Add(task);
            Console.WriteLine("Задача была добавлена.");
            ShowTasks();
            MoveOn();
        }

        private void ShowTasks()
        {
            Console.WriteLine("Задачи:");
            for (var i = 0; i < _tasks.Count; i++)
            {
                Console.WriteLine("{0} : {1}", i + 1, _tasks[i].Description);
            }

            Console.WriteLine("-------------------------");
        }

        private void ShowAllTasks()
        {
            if (IsHaveTask)
            {
                ShowTasks();
            }
            else
            {
                Console.WriteLine("Задач нет!");
            }

            MoveOn();
        }

        private int GetNumber()
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

        private void ShowSpecificTask()
        {
            if (IsHaveTask)
            {
                Task task = GetTask();
                if (task != null)
                {
                    Console.WriteLine("Задача:\t\t\t {0}", task.Description);
                    Console.WriteLine("Приоритет:\t\t {0}", task.Priority);
                    Console.WriteLine("Статус:\t\t\t {0}", task._status);
                    Console.WriteLine("Должна быть выполнена к: {0}", task.CompletionDate);
                }
                else
                {
                    Console.WriteLine("Такой задачи нет");
                }
            }

            Console.WriteLine("Задач нет!");
            MoveOn();
        }

        private Task GetTask()
        {
            if (!IsHaveTask) return null;
            while (true)
            {
                Console.WriteLine("Выберите задачу:");
                ShowTasks();
                var task_index = GetNumber();
                if (IsCorrectUserChoice<Task>(task_index, _tasks))
                {
                    return _tasks[task_index - 1];
                }
                else
                {
                    Console.WriteLine("Введён неверный номер задачи!");
                }
            }
        }

        private void RemoveTask()
        {
            if (IsHaveTask)
            {
                var taskForeRemove = GetTask();
                taskForeRemove._status.Remove(_tasks);
                Console.WriteLine("-------------------");
                Console.WriteLine("Задача была удалена: ");
                ShowTasks();
                MoveOn();
            }
            else
            {
                Console.WriteLine("Задач нет!");
                MoveOn();
            }
        }

        private State GetStatusForChange(Task task)
        {
            var statusesForChange = new List<State>()
            {
                new StateInProgress(task),
                new StateDone()
            };

            while (true)
            {
                Console.WriteLine("Выберите номер статуса: ");
                for (var i = 0; i < statusesForChange.Count; i++)
                {
                    Console.WriteLine("{0} :{1}", i + 1, statusesForChange[i]);
                }

                int stateIndex = GetNumber();
                if (IsCorrectUserChoice<State>(stateIndex, statusesForChange))
                {
                    return statusesForChange[stateIndex - 1];
                }
                else
                {
                    Console.WriteLine("Такого номера статуса нет");
                }
            }
        }

        private void ChangeTaskStatus()
        {
            if (IsHaveTask)
            {
                var task = GetTask();
                var state = GetStatusForChange(task);
                switch (state.ToString())
                {
                    case "в работе":
                        task.InProgress();
                        break;
                    case "сделано":
                        task.Done();
                        break;
                }
            }
            else
            {
                Console.WriteLine("Задач нет!!!");
            }

            MoveOn();
        }

        private void GetTasksFromJsonFile()
        {
            _tasks = DataLoader.Load();

            MoveOn();
        }

        private void LoadTasksInJsonFile()
        {
            DataLoader.Save(_tasks);
            MoveOn();
        }

        private int GetMenuVariants()
        {
            List<string> variants = new List<string>()
            {
                "Создать новую задчу",
                "Отобразить все задачи",
                "Отобразить задачу",
                "Удалить задачу",
                "Изменить статус задачи",
                "Загрузить данные из файла",
                "Выгрузить данные в json файл"
            };
            while (true)
            {
                Console.WriteLine("Выберите действие: ");
                for (var i = 0; i < variants.Count; i++)
                {
                    Console.WriteLine("{0}: {1}", i + 1, variants[i]);
                }

                var userChoice = GetNumber();
                if (IsCorrectUserChoice<string>(userChoice, variants))
                {
                    return userChoice;
                }
                else
                {
                    Console.WriteLine("Такого действия нет в списке меню!");
                    Console.WriteLine();
                }
            }
        }

        public void Menu()
        {
            while (true)
            {
                var choice = GetMenuVariants();
                switch (choice)
                {
                    case 1:
                        AddNewTask();
                        break;
                    case 2:
                        ShowAllTasks();
                        break;
                    case 3:
                        ShowSpecificTask();
                        break;
                    case 4:
                        RemoveTask();
                        break;
                    case 5:
                        ChangeTaskStatus();
                        break;
                    case 6:
                        GetTasksFromJsonFile();
                        break;
                    case 7:
                        LoadTasksInJsonFile();
                        break;
                }
            }
        }
    }
}