using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Lab3
{
    public interface ICollection<T>
    {
        void Add(T item);
        void Delete(T item);
        T Get(int index);
    }
    public class Collection<T> : ICollection<T> where T : struct
    {
        private HashSet<T> collection;

        public HashSet<T> _Collection { get { return collection; } }

        public Collection(IEnumerable<T> elements)
        {
            collection = new HashSet<T>(elements);
        }

        public void Add(T element)
        {
            try
            {
                collection.Add(element);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении элемента: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Метод Add завершён.");
            }
        }

        public void Delete(T element)
        {
            try
            {
                if (!collection.Remove(element))
                {
                    Console.WriteLine($"Элемент {element} не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении элемента: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Метод Delete завершён.");
            }
        }

        public T Get(int index)
        {
            try
            {
                if (index < 0 || index >= collection.Count)
                    throw new ArgumentOutOfRangeException("Индекс не попадает в диапазон");
                return collection.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return default(T);
            }
            finally
            {
                Console.WriteLine("Метод Get завершён.");
            }
        }



        public void SaveToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (T element in collection)
                    {
                        writer.WriteLine(element.ToString());
                    }
                }
                Console.WriteLine($"Коллекция сохранена в файл: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в файл: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Метод SaveToFile завершён.");
            }
        }
        public void ReadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Файл не существует", fileName);

            collection.Clear();

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(fileName);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"Прочитали из файла: {line}");

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении из файла: {ex.Message}");
            }
            finally
            {
                reader?.Close();
                Console.WriteLine("Метод ReadFromFile завершён.");
            }
        }

        public override string ToString()
        {
            return "{" + string.Join(", ", collection) + "}";
        }
    }

    

    public class CollectionType<T> : ICollection<T> where T : class
    {
        private HashSet<T> collection;

        public HashSet<T> Collection { get { return collection; } }

        public CollectionType(IEnumerable<T> elements)
        {
            collection = new HashSet<T>(elements);
        }

        public void Add(T element)
        {
            try
            {
                collection.Add(element);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при добавлении элемента: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Метод Add завершён.");
            }
        }

        public void Delete(T element)
        {
            try
            {
                if (!collection.Remove(element))
                {
                    Console.WriteLine($"Элемент {element} не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении элемента: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Метод Delete завершён.");
            }
        }

        public T Get(int index)
        {
            try
            {
                if (index < 0 || index >= collection.Count)
                    throw new ArgumentOutOfRangeException("Индекс не попадает в диапазон");
                return collection.ElementAt(index);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
                return null;
            }
            finally
            {
                Console.WriteLine("Метод Get завершён.");
            }
        }

       

        public void SaveToFile(string fileName)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName))
                {
                    foreach (T element in collection)
                    {
                        writer.WriteLine(element.ToString());
                    }
                }
                Console.WriteLine($"Коллекция сохранена в файл: {fileName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при сохранении в файл: {ex.Message}");
            }
            finally
            {
                Console.WriteLine("Метод SaveToFile завершён.");
            }
        }
        public void ReadFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                throw new FileNotFoundException("Файл не существует", fileName);

            collection.Clear(); 

            StreamReader reader = null;
            try
            {
                reader = new StreamReader(fileName);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine($"Прочитали из файла: {line}");
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при чтении из файла: {ex.Message}");
            }
            finally
            {
                reader?.Close();
                Console.WriteLine("Метод ReadFromFile завершён.");
            }
        }

        public override string ToString()
        {
            return "{" + string.Join(", ", collection) + "}";
        }
    }

    public class Director
    {
        public string Name { get; set; }

        public Director(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return $"Режиссер: {Name}";
        }
    }

    public abstract class TelevisionProgram
    {
        public string Title { get; set; }
        public int Duration { get; set; }

        public TelevisionProgram(string title, int duration)
        {
            Title = title;
            Duration = duration;
        }

        public abstract void ShowDetails();

        public override string ToString()
        {
            return $"Программа: {Title}, Длительность: {Duration} минут";
        }
    }

    public class Film : TelevisionProgram
    {
        public Director FilmDirector { get; set; }

        public Film(string title, int duration, Director director) : base(title, duration)
        {
            FilmDirector = director;
        }
        public override bool Equals(object obj)
        {
            if (obj is Film otherTitle)
            {
                return this.Title == otherTitle.Title;
            }
            return false;
        }

       
        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }

        public override void ShowDetails()
        {
            Console.WriteLine($"Фильм: {Title}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name}");
        }

        public override string ToString()
        {
            return $"Фильм: {Title}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name}";
        }
    }

    public static class Program
    {
        public static void Main()
        {
            var collect = new Collection<int>(new HashSet<int> { 2,4,5,67,8 });
            collect.Add(11);
            collect.Delete(2);
            collect.Get(3);
            collect.SaveToFile("numbers.txt");
            collect.ReadFromFile("numbers.txt");

            var collect_double = new Collection<double>(new HashSet<double> { 3.11,7.555,5.99 });
            collect_double.Add(11.99);
            collect_double.Delete(3.0);
            collect_double.Get(3);
            collect_double.SaveToFile("double.txt");
            collect_double.ReadFromFile("double.txt");
            var director = new Director("Ivanov Ivan");
            var filmCollection = new CollectionType<Film>(new HashSet<Film>
            {
                new Film("Ittttttt", 148, director),
                new Film("Uuuuuuuur", 169, director),
                new Film("Ffffffffffff", 9, director)
            });
            filmCollection.Add(new Film("Ieinnenee", 199, director));
            filmCollection.Delete(new Film("Ittttttt", 148, director));
            filmCollection.Get(3);
            filmCollection.SaveToFile("films.txt");
            filmCollection.ReadFromFile("films.txt");
           
        }
    }
}
