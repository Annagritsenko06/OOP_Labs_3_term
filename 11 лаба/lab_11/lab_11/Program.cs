using System;
using System.Reflection;

namespace Lab11
{
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
            return $"Телевизионная программа: {Title}, Длительность: {Duration} минут.";
        }
    }


    public class Director
    {
        public string Name { get; set; }

        public Director(string name)
        {
            Name = name;
        }
        public Director()
        {
            Name = "Неизвестный режиссер";
        }
        public override string ToString()
        {
            return $"Режиссер: {Name}";
        }
    }


    public class Film : TelevisionProgram
    {
        public Director FilmDirector { get; set; }

        public Film(string title, int duration, Director director) : base(title, duration)
        {
            FilmDirector = director;
        }
        public Film() : base("Неизвестный фильм", 0)
        {
            FilmDirector = new Director();
        }
        public override void ShowDetails()
        {
            Console.WriteLine($"Фильм: {Title}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name} ShowDetails()");
        }
        public override string ToString()
        {
            return $"Фильм: {Title}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name}";
        }
        public double Cout(int duration)
        {
            double durationInHours = (duration / 60);

            Console.WriteLine($"Длительность фильма в часах: {durationInHours}");
            return durationInHours;
            //}
        }

        public static class Reflector
        {
            public static string AssemblyName(string nameOfClass)
            {
                Type type = Type.GetType(nameOfClass);

                if (type == null) { return null; }
                return type.Assembly.FullName;


            }
            public static bool FindPublicConstructors(string nameOfClass)
            {
                Type type = Type.GetType(nameOfClass, true);
                if (type == null) { return false; }
                var constructors = type.GetConstructors(BindingFlags.Public);
                return constructors.Any();
            }
            public static IEnumerable<string> FindMethods(string nameOfClass)
            {
                Type type = Type.GetType(nameOfClass, true);
                if (type == null) { return Enumerable.Empty<string>(); }
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
                return methods.Select(m => m.Name);

            }
            public static IEnumerable<string> FindInterfaces(string nameOfClass)
            {
                Type type = Type.GetType(nameOfClass, true);
                if (type == null) { return Enumerable.Empty<string>(); }
                var interfaces = type.GetInterfaces();
                return interfaces.Select(m => m.Name);
            }
            public static IEnumerable<string> AllFieldsAndProperties(string nameOfClass)
            {
                Type type = Type.GetType(nameOfClass, true);
                if (type == null) { return Enumerable.Empty<string>(); }
                var fields = type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                var collection = new List<string>();
                collection.AddRange(fields.Select(m => $"Field: {m.Name}"));
                collection.AddRange(properties.Select(m => $"Property: {m.Name}"));
                return collection;
            }

            public static IEnumerable<string> FindMethodsWithParameterType(string className, string parameterType)
            {
                Type type = Type.GetType(className);
                if (type == null) return Enumerable.Empty<string>();
                var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);

                Type paramType = Type.GetType(parameterType);
                if (paramType == null) return Enumerable.Empty<string>();

                var result = methods.Where(m => m.GetParameters().Any(p => p.ParameterType == paramType))
                                    .Select(m => m.Name);

                return result;
            }


            public static void Invoke(string className, string methodName, string parametersFilePath)
            {
                Type type = Type.GetType(className);
                if (type == null)
                {
                    Console.WriteLine("Класс не найден.");
                    return;
                }

                MethodInfo method = type.GetMethod(methodName);
                if (method == null)
                {
                    Console.WriteLine("Метод не найден.");
                    return;
                }

                var parameters = ReadParametersFromFile(parametersFilePath, method);

                var instance = Activator.CreateInstance(type);

                method.Invoke(instance, parameters);
            }

            private static object[] ReadParametersFromFile(string parametersFilePath, MethodInfo method)
            {
                if (!File.Exists(parametersFilePath))
                    throw new FileNotFoundException($"Файл '{parametersFilePath}' не найден.");

                var lines = File.ReadAllLines(parametersFilePath);
                var parameters = new List<object>();

                var methodParams = method.GetParameters();
                for (int i = 0; i < methodParams.Length; i++)
                {
                    var paramType = methodParams[i].ParameterType;
                    var value = Convert.ChangeType(lines[i], paramType);
                    parameters.Add(value);
                }

                return parameters.ToArray();
            }

            public static void SaveToTextFile(string className, string filePath)
            {
                var analysisResult = new List<string>
            {
                $"Class: {className}",
                $"Assembly Name: {AssemblyName(className)}",
                $"Has Public Constructors: {FindPublicConstructors(className)}",
                "Public Methods:"
            };
                analysisResult.AddRange(FindMethods(className).Select(method => $"- {method}"));
                analysisResult.Add("Fields and Properties:");
                analysisResult.AddRange(AllFieldsAndProperties(className).Select(f => $"- {f}"));
                analysisResult.Add("Implemented Interfaces:");
                analysisResult.AddRange(FindInterfaces(className).Select(i => $"- {i}"));

                File.WriteAllLines(filePath, analysisResult);
            }

            public static object Create(string className)
            {

                Type type = Type.GetType(className);
                Console.WriteLine(type);
                if (type == null)
                {
                    Console.WriteLine($"Тип '{className}' не найден. Проверьте имя класса и наличие сборки.");
                    return null;
                }

                var constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor == null)
                {
                    Console.WriteLine($"Класс '{className}' не имеет конструктора без параметров.");
                    return null;
                }
                object newObject = Activator.CreateInstance(type);
                return newObject;
            }

        }
        class Program
        {
            static void Main(string[] args)
            {
                Director director = new Director("Дана Леду Миллер");
                Film film = new Film("Моана 2", 120, director);
                var typeOfClass = film.GetType().FullName;


                Console.WriteLine("Assembly Name: " + Reflector.AssemblyName(typeOfClass));
                Console.WriteLine("Has Public Constructors: " + Reflector.FindPublicConstructors(typeOfClass));
                Console.WriteLine("Public Methods: " + string.Join(", ", Reflector.FindMethods(typeOfClass)));
                Console.WriteLine("Fields and Properties: " + string.Join(", ", Reflector.AllFieldsAndProperties(typeOfClass)));
                Console.WriteLine("Implemented Interfaces: " + string.Join(", ", Reflector.FindInterfaces(typeOfClass)));
                Console.WriteLine("Methods with string parameter: " +
                                  string.Join(", ", Reflector.FindMethodsWithParameterType(typeOfClass, typeof(string).FullName)));


                var demoInstance = Reflector.Create(typeOfClass);

                Console.WriteLine(demoInstance);


                File.WriteAllLines("params.txt", new string[] {  "60" });
                Reflector.Invoke(typeOfClass, "Cout", "params.txt");
            }
        }

    }
}