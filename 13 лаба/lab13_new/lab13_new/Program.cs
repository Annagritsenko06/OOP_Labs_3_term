using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Xml.XPath;

public interface ISerializer
{
    void Serialize<T>(T obj, string filePath);
    T Deserialize<T>(string filePath);
}

public class BinarySerializer : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(fs, obj);
        }
    }

    public T Deserialize<T>(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Open))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            return (T)formatter.Deserialize(fs);
        }
    }
}

public class SoapSerializer : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.Create))
        {
            SoapFormatter formatter = new SoapFormatter();
            Film t = obj as Film;
            formatter.Serialize(fs, t);
        }
    }

    public T Deserialize<T>(string filePath)
    {
        using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
        {
            SoapFormatter formatter = new SoapFormatter();
            return (T)formatter.Deserialize(fs);
        }
    }
}

public class JsonSerializerImpl : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        string json = JsonSerializer.Serialize(obj);
        File.WriteAllText(filePath, json);
    }

    public T Deserialize<T>(string filePath)
    {
        string json = File.ReadAllText(filePath);
        return JsonSerializer.Deserialize<T>(json);
    }
}

public class XmlSerializerImpl : ISerializer
{
    public void Serialize<T>(T obj, string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (TextWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, obj);
        }
    }

    public T Deserialize<T>(string filePath)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(T));
        using (TextReader reader = new StreamReader(filePath))
        {
            return (T)serializer.Deserialize(reader);
        }
    }
}

[Serializable]
public abstract class TelevisionProgram
{
    public string Title { get; set; }
    public int Duration { get; set; }

    public TelevisionProgram() { }

    public TelevisionProgram(string title, int duration)
    {
        Title = title;
        Duration = duration;
    }

    public abstract void ShowDetails();
}

[Serializable]
public class Director
{
    public string Name { get; set; }

    public Director() { }

    public Director(string name)
    {
        Name = name;
    }
}

[Serializable]
public class Film : TelevisionProgram
{
    [NonSerialized] 
    public Director FilmDirector;

    public Film() { }

    public Film(string title, int duration, Director director) : base(title, duration)
    {
        FilmDirector = director;
    }

    public override void ShowDetails()
    {
        Console.WriteLine($"Фильм: {Title}, Длительность: {Duration} минут, Режиссер: {FilmDirector?.Name}");
    }
}

class Program
{
    static void Main()
    {
        List<Film> films = new List<Film>
        {
            new Film("Фильм 1", 120, new Director("Режиссер 1")),
            new Film("Фильм 2", 90, new Director("Режиссер 2"))
        };



        List<ISerializer> serializers = new List<ISerializer>
        {
            new BinarySerializer(),
            new SoapSerializer(),
            new JsonSerializerImpl(),
            new XmlSerializerImpl()
        };

        foreach (var serializer in serializers)
        {
            string filePath = $"{serializer.GetType().Name}.dat";
            if (serializer.GetType().Name == "SoapSerializer")
            {
                filePath = $"{serializer.GetType().Name}.soap";
                serializer.Serialize(films[0], filePath);
                Console.WriteLine($"Сериализация с использованием {serializer.GetType().Name} выполнена.");

                var deserializedFilm = serializer.Deserialize<Film>(filePath);
                Console.WriteLine($"Десериализация с использованием {serializer.GetType().Name}:");
                deserializedFilm.ShowDetails();
            }
            else
            {
                serializer.Serialize(films, filePath);
                Console.WriteLine($"Сериализация с использованием {serializer.GetType().Name} выполнена.");

                var deserializedFilms = serializer.Deserialize<List<Film>>(filePath);
                Console.WriteLine($"Десериализация с использованием {serializer.GetType().Name}:");
                foreach (var film in deserializedFilms)
                {
                    film.ShowDetails();
                }
            }
        }

        XDocument xmlDoc = new XDocument(
            new XElement("Films",
                new XElement("Film",
                    new XElement("Title", "Фильм 1"),
                    new XElement("Duration", 120),
                    new XElement("Director", "Режиссер 1")
                ),
                new XElement("Film",
                    new XElement("Title", "Фильм 2"),
                    new XElement("Duration", 90),
                    new XElement("Director", "Режиссер 2")
                )
            )
        );

        xmlDoc.Save("films.xml");
        Console.WriteLine("XML документ сохранен как films.xml");

        Console.WriteLine("XPath запросы:");
        var titles = xmlDoc.XPathSelectElements("//Film/Title");
        foreach (var title in titles) Console.WriteLine($"Название: {title.Value}");

        var durations = xmlDoc.XPathSelectElements("//Film/Duration");
        foreach (var duration in durations) Console.WriteLine($"Длительность: {duration.Value}");

        Console.WriteLine("Linq to XML запросы:");
        var linqTitles = xmlDoc.Descendants("Title").Select(t => t.Value);
        foreach (var title in linqTitles) Console.WriteLine($"Название: {title}");

        JsonObject jsonDoc = new JsonObject
        {
            ["Films"] = new JsonArray
            {
                new JsonObject { ["Title"] = "Film 1", ["Duration"] = 120, ["Director"] = "Dir 1" },
                new JsonObject { ["Title"] = "Film 2", ["Duration"] = 90, ["Director"] = "Dir 2" }
            }
        };

        File.WriteAllText("films.json", jsonDoc.ToString());
        Console.WriteLine("JSON документ сохранен как films.json");

        var jsonFilms = jsonDoc["Films"].AsArray();
        foreach (var film in jsonFilms)
        {
            Console.WriteLine($"Название: {film["Title"]}, Длительность: {film["Duration"]}");
        }
    }
}

