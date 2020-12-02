using System;
using System.Collections.Generic;

namespace Tasks
{
    public class StateDone : State
    {
        private readonly string Name = "сделано";
        
        public void InProgress()
        {
            Console.WriteLine("Статус задачи изменить уже нельзя");
        }

        public void Done()
        {
            Console.WriteLine("Статус задачи изменить уже нельзя");
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