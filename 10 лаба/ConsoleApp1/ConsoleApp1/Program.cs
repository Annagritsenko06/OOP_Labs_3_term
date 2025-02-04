using System;
using System.Reflection;
namespace Test
{
    class Computer : IComparable<Computer>
    {

        public string Processor { get; set; }
        public string Ram { get; set; }
        public int Price { get; set; }

       
        public IComparable<Computer> CompareTo(Computer o, Computer a)
        {

            
        }
    }
    enum daysOfWeek  {Понедельник,Вторник,Среда,Четверг, Пятница,Суббота,Воскресенье };

class Program
    {
        
        static void Main(string[] args)
        {
        Array array = new int[] { 1, 2, 3, 4, 5 };
        Console.WriteLine(array.ToString());
            Console.WriteLine(daysOfWeek.Понедельник);

        }
    }

}