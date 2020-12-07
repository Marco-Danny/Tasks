using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Tasks
{
    public class DataLoader
    {
        private readonly string _directory;
        private readonly string _fileName;
        public string _path;
        private DataContractJsonSerializer _jsonFormatter = new DataContractJsonSerializer(typeof(List<Task>));
        public List<Task> _tasks;

        public DataLoader(string path)
        {
            _path = path;
        }

        public DataLoader(string directory, string fileName)
        {
            _directory = directory;
            _fileName = fileName;
            _path = $"{directory}/{fileName}";
        }

        public List<Task> ReadDataToList(string path)
        {
            try
            {
                var content = File.ReadAllText(path);
                // if (string.IsNullOrEmpty(content))
                if (content.Length <= 1)
                {
                    throw new ApplicationException($"файл {_fileName} пустой");
                }

                var tasks = JsonSerializer.Deserialize<List<Task>>(content);
                return tasks;
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Отсутствует директория {_fileName}");
                // return new List<Task>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Отсутствует файл {_fileName}");
                // return new List<Task>();
            }
            catch (ApplicationException ex)
            {
                // Console.WriteLine(ex.Message);
                // return new List<Task>();
            }
            catch (JsonException)
            {
                Console.WriteLine("Не та структура файла");
            }

            throw new InvalidOperationException();
        }

        public void SerealizeToList(List<Task> tasks)
        {
            using var file = new FileStream(_path, FileMode.OpenOrCreate);
            _jsonFormatter.WriteObject(file, tasks);
        }

        public List<Task> DeserealizeToList()
        {
            using var file = new FileStream(_path, FileMode.OpenOrCreate);
            var newTasks = _jsonFormatter.ReadObject(file) as List<Task>;
            if (newTasks != null)
            {
                foreach (var task in newTasks)
                {
                    Console.WriteLine(task.ToString());
                }

                return newTasks;
            }

            return null;
        }
    }
}