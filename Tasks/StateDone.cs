using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Tasks
{
    [DataContract]
    public class StateDone : State
    {
        public string Name = "сделано";
        
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