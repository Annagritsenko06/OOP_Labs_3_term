using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public partial class Film : TelevisionProgram
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
}