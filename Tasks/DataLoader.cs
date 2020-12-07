using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Tasks
{
    public class DataLoader
    {
        const string directory = @"C:\Users\admin\C# projects\Tasks\Tasks";
        const string fileName = "mytasks.json";

        public static void Save(List<Task> tasks)
        {
            string json = JsonConvert.SerializeObject(tasks);
            SaveFile(json);
        }

        public static List<Task> Load()
        {
            try
            {
                string content = File.ReadAllText($"{directory}/{fileName}");
                if (string.IsNullOrEmpty(content))
                {
                    throw new ApplicationException($"файл {fileName} пустой");
                }
                var tasks = JsonConvert.DeserializeObject<List<Task>>(content);
                // foreach (Task task in tasks)
                // {
                //     task.SetState();
                // }

                return tasks;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Отсутствует директория {directory}");
                return new List<Task>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Отсутствует файл {fileName}");
                return new List<Task>();
            }
            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
                return new List<Task>();
            }
        }

        private static void SaveFile(string content)
        {
            try
            {
                File.WriteAllText($"{directory}/{fileName}", content);
            }
            catch (DirectoryNotFoundException)
            {
                Directory.CreateDirectory(directory);
                SaveFile(content);
            }
        }
    }
}