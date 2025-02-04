using System;


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
        Name = name;
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

    public override void ShowDetails()
    {
        Console.WriteLine($"Фильм: {Title}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name}");
    }

    public override string ToString()
    {
        return $"Фильм: {Title}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name}";
    }
}


public class FeatureFilm : Film
{
    public string Genre { get; set; }

    public FeatureFilm(string title, int duration, Director director, string genre) : base(title, duration, director)
    {
        Genre = genre;
    }

    public override void ShowDetails()
    {
        Console.WriteLine($"Художественный фильм: {Title}, Жанр: {Genre}, Режиссер: {FilmDirector.Name}");
    }

    public override string ToString()
    {
        return $"Художественный фильм: {Title}, Жанр: {Genre}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name}";
    }
}


public class Cartoon : Film
{
    public Cartoon(string title, int duration, Director director) : base(title, duration, director)
    {
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


class Program
{
    static void Main(string[] args)
    {
        
        Director director1 = new Director("Сергей Михайлович");
        TelevisionProgram film = new FeatureFilm("Инопланетянин", 115, director1, "Фантастика");
        TelevisionProgram cartoon = new Cartoon("Губка Боб", 30, new Director("Стивен Хилленбёрг"));
        TelevisionProgram news = new News("Новости сегодня", 60, "Иван Иванов");
        TelevisionProgram ad = new Advertisement("Реклама парфюма Dior", 5);

       
        TelevisionProgram[] programs = { film, cartoon, news, ad };

        
        Printer printer = new Printer();

       
        foreach (var program in programs)
        {
            printer.IAmPrinting(program);
        }

        if(news is TelevisionProgram )
        {
            printer.IAmPrinting(news);
        }
        
        ClonableMovie cloneMovie = new ClonableMovie("новый фильм");
        ICloneable cloneInterface = cloneMovie;
        BaseClone cloneBase = cloneMovie;

        cloneInterface.DoClone(); 
        cloneBase.DoClone();      
    }
}
