using System;
using System.Collections.Generic;

namespace Tasks
{
    public class TaskManager
    {
        private List<Task> Tasks = new List<Task>();

        public void AddNewTask()
        {
            var description = GetDescriptionTask();
            var dateTime = GetDate();
            var priority = GetPriority();
            Task task = new Task(description, dateTime, priority);
            Tasks.Add(task);
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

        public void ShowTasks()
        {
            Console.WriteLine("Задачи:");
            foreach (var task in Tasks)
            {
                Console.WriteLine(task.Description);
            }
        }
    }
}