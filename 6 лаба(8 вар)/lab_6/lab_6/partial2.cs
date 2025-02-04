using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public partial class Film
{

    public override string ToString()
    {
        return $"Фильм: {Title}, Длительность: {Duration} минут, Режиссер: {FilmDirector.Name}";
    }
}
