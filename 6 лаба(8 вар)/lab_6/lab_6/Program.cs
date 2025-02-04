using System;
using System.Diagnostics;
using System.Xml.Linq;
using static ProgramContainer;


public interface ICloneable
{
    bool DoClone();
}


public abstract class BaseClone
{
    public abstract bool DoClone();
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
        return $"Телевизионная программа: {Title}, Длительность: {Duration} минут.";
    }
}


public class Director
{
    public string Name { get; set; }

    public Director(string name)
    {
        if (string.IsNullOrEmpty(name) || !char.IsUpper(name[0]))
        {
            throw new DirectorException("Имя режиссера должно начинаться с заглавной буквы.");
        }

        Name = name;
    }

    public override string ToString()
    {
        return $"Режиссер: {Name}";
    }
}

public class FeatureFilm : Film
{
    public string Genre { get; set; }
    public int Year { get; set; }

    public FeatureFilm(string title, int duration, Director director, string genre, int year) : base(title, duration, director)
    {
        if (year > 2024)
        {
            throw new FeatureFilmException("Неправильный год выхода фильма!!!");
        }
        Genre = genre;
        Year = year;
    }

    public override void ShowDetails()
    {
        Console.WriteLine($"Художественный фильм: {Title}, Жанр: {Genre}, Режиссер: {FilmDirector.Name}");
    }

    public override string ToString()
    {
        return $"Художественный фильм: {Title}, Жанр: {Genre}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name},год выхода:{Year}";
    }
}


public class Cartoon : Film
{
    public Cartoon(string title, int duration, Director director) : base(title, duration, director)
    {
        if (duration < 0)
        {
            throw new CartoonException("Длительность мультфильма не может быть отрицательной!!!");
        }
    }
    
public override void ShowDetails()
    {
        Console.WriteLine($"Мультфильм: {Title}, Режиссер: {FilmDirector.Name}");
    }

    public override string ToString()
    {
        return $"Мультфильм: {Title}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name}";
    }
}


public class News : TelevisionProgram
{
    public string NewsAnchor { get; set; }

    public News(string title, int duration, string newsAnchor) : base(title, duration)
    {
        
        NewsAnchor = newsAnchor;
    }

    public override void ShowDetails()
    {
        Console.WriteLine($"Новости: {Title}, Ведущий: {NewsAnchor}");
    }

    public override string ToString()
    {
        return $"Новости: {Title}, Длительность: {Duration} минут, Ведущий: {NewsAnchor}";
    }
}


public sealed class Advertisement : TelevisionProgram
{
    public Advertisement(string title, int duration) : base(title, duration)
    {
        if (duration < 1 || duration > 300)
        {
            throw new ArgumentOutOfRangeException(nameof(duration), "Длительность программы должна быть от 1 до 300 минут.");
        }
    }

    public override void ShowDetails()
    {
        Console.WriteLine($"Реклама: {Title}, Длительность: {Duration} минут");
    }

    public override string ToString()
    {
        return $"Реклама: {Title}, Длительность: {Duration} минут.";
    }
}


public class ClonableMovie : BaseClone, ICloneable
{
    public string MovieName { get; set; }

    public ClonableMovie(string movieName)
    {
        MovieName = movieName;
    }


    bool ICloneable.DoClone()
    {
        Console.WriteLine($"Интерфейсная реализация для фильма {MovieName}");
        return true;
    }


    public override bool DoClone()
    {
        Console.WriteLine($"Абстрактная реализация для фильма {MovieName}");
        return true;
    }

    public override string ToString()
    {
        return $"ClonableMovie: {MovieName}";
    }
}


public class Printer
{
    public void IAmPrinting(TelevisionProgram program)
    {
        Console.WriteLine(program.ToString());
    }
}

public enum ProgramType
{
    Film,
    Cartoon,
    News,
    Advertisement,
    Documentary
}

public struct TimeSlot
{
    public int Hour { get; set; }
    public int Minute { get; set; }



    public TimeSlot(int hour, int minute)
    {
        Hour = hour;
        Minute = minute;
    }

    public override string ToString()
    {
        return $"{Hour:D2}:{Minute:D2}";
    }
}

public class ProgramContainer
{
    private List<TelevisionProgram> programs1 = new List<TelevisionProgram>();
    public List<TelevisionProgram> programs
    {
        get { return programs1; }
        set { programs1 = value; }
    }
    public ProgramContainer()
    {
        programs = new List<TelevisionProgram>();
    }
    public void AddProgram(TelevisionProgram program)
    {
        programs.Add(program);
    }
    public void RemoveProgram(TelevisionProgram program)
    {
        programs.Remove(program);
    }
    public void ShowPrograms()
    {
        foreach (var program in programs)
        {
            Console.WriteLine($"{program.ToString()}");
        }
    }

    public class ProgramController
    {
        private ProgramContainer _container;

        public ProgramController(ProgramContainer container)
        {
            _container = container;
        }


        public void ShowPrograms()
        {
            _container.ShowPrograms();
        }

        public int GetTotalDuration()
        {
            int totalDuration = 0;

            foreach (var program in _container.programs)
            {
                totalDuration += program.Duration;
            }

            return totalDuration;
        }


        public int GetAdvertisementCount()
        {
            int adCount = 0;

            foreach (var program in _container.programs)
            {
                if (program is Advertisement)
                {
                    adCount++;
                }
            }

            return adCount;
        }
        public void ShowFilms(int year)
        {
            Console.WriteLine($"Все фильмы, снятые в {year} году:");

            foreach (var program in _container.programs)
            {
                if (program is FeatureFilm film && film.Year == year)
                {
                    Console.WriteLine(film.ToString());
                }
            }
        }

    }

    
    public class FilmException : Exception
    {
        public string _message { get; set; }
        public FilmException(string message)
        {
            _message = message;
        }
        public bool Func()
        {
            Console.WriteLine(_message);
            return true;
        }
    }
     public class CartoonException : FilmException  
    {
        public  CartoonException(string message) : base(message) { }
       
    }
    public class DirectorException : FilmException
    {
        public DirectorException(string message) : base(message) { }
        
    }
    public class FeatureFilmException : FilmException
    {
        public FeatureFilmException(string message) : base(message) { }
        
    }
}


class Program
{
    static void Main(string[] args)
    {
        try
        {
            TelevisionProgram cartoon = new Cartoon("Губка Боб", -30, new Director("Стивен Хилленбёрг"));
            TelevisionProgram news = new News("Новости сегодня", 60, "Иван Иванов");
            TelevisionProgram ad = new Advertisement("Реклама парфюма Dior", 5);

        }
        catch (CartoonException ex)

        {
            Console.WriteLine(ex.Func());
            Console.WriteLine(ex.ToString());

        }

        try
        {
            Director director1 = new Director("Александр Михайлович");
            TelevisionProgram film2 = new FeatureFilm("Дневник памяти", 115, director1, "Мелодрама", 2011);
            TelevisionProgram film3 = new FeatureFilm("Вий", 155, director1, "Ужасы", 2055);
        }
        catch (FeatureFilmException ex)
        {
            Console.WriteLine(ex.Func());
        }
        try
        {

            Director director2 = new Director("сергей Михайлович");
        }
        catch (DirectorException ex)
        {
            Console.WriteLine(ex.Func());
            Console.WriteLine(ex.ToString());
        }
        try
        {

            TelevisionProgram ad = new Advertisement("Реклама парфюма Dior", -885);
            if (ad.Duration < 1 || ad.Duration > 300)
            {
                throw new ArgumentOutOfRangeException(nameof(ad.Duration), "Длительность программы должна быть от 1 до 300 минут.");
            }
        }
        catch (ArgumentOutOfRangeException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");

        }
        try
        {

            Director director = null;
            Debug.Assert(director != null, "Объект director не должен быть null!");
            Console.WriteLine(director.Name);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.ToString()}");

        }

        finally { Console.WriteLine("Блок  finally"); }
    }
}