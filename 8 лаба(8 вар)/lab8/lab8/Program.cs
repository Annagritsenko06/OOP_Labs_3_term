using System;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using System.Xml.Linq;
namespace Lab8
{
   
    public class StringProcessor
    {
        public static  Func<string,string> RemovePunctuation = input =>
            Regex.Replace(input, @"[^\w\s]", "");

        public static string AddSymbols(string input) =>
            $"-------{input}****";

        public static string ToUpperCase(string input) =>
            input.ToUpper();

        public static string RemoveExtraSpaces(string input) =>
            Regex.Replace(input, @"\s+", " ").Trim();

        public static string ReplaceWord(string input) =>
            input.Replace("wwwww", "oooooooooo");

        public static string ProcessString(string input, params Func<string, string>[] actions)
        {
            string result = input;
            foreach (var action in actions)
            {
                result = action(result);
            }
            return result;
        }
    }
    public delegate void UpgradeDelegate();
    public delegate void TurnOnDelegate(int voltage);
    class Boss
    {
        public event UpgradeDelegate Upgrade;
        public event TurnOnDelegate TurnOn;
        public void InvokeUpgrade() => Upgrade?.Invoke();
        public void InvokeTurnOn(int voltage) => TurnOn?.Invoke(voltage);

    }
    class Machine
    {
        public string Name { get; private set; }
        public bool IsWorking { get; private set; } = true;
        private int VoltageLimit;

        public Machine(string name, int voltageLimit)
        {
            Name = name;
            VoltageLimit = voltageLimit;
        }
        public void Upgrade() => Console.WriteLine($"{Name} улучшен до следующей версии.");
        public void TurnOn( int voltage)
        {
            if (voltage > VoltageLimit)
            {
                IsWorking = false;
                Console.WriteLine($"{Name} не работает, слишком большое напряжение {voltage}...");
            }       
            else { Console.WriteLine($"{Name} успешна работает с напряжением{voltage} "); }

        }
        
    }
    class SuperMachine
    {
        public string Model { get; private set; }
        public bool IsWorking { get; private set; } = true;
        private int VoltageLimit;

        public SuperMachine(string model, int voltageLimit)
        {
            Model = model;
            VoltageLimit = voltageLimit;
        }
        public void Upgrade() => Console.WriteLine($"{Model} обновлен до последней модели.");
        public void TurnOn(int voltage)
        {
            if (voltage > VoltageLimit)
            {
                IsWorking = false;
                Console.WriteLine($"{Model} сломалась из-за слишком большое напряжение {voltage}...");
            }
            else { Console.WriteLine($"{Model} функционирует с напряжением{voltage} "); }

        }
    }
    class Program
    {
        public static void Main()
        {
           
        Boss boss = new Boss();

            
            Machine machine1 = new Machine("Машина1", 220);
            Machine machine2 = new Machine("Машина2", 240);
            SuperMachine machine3 = new SuperMachine("Супермашина1", 220);
            SuperMachine machine4 = new SuperMachine("Супермашина2", 240);

            boss.Upgrade += () => Console.WriteLine($"{machine1.Name} была улучшена лямбда-выражением.");
            boss.Upgrade += () => Console.WriteLine($"{machine3.Model} обновлена до последней модели (лямбда-выражение).");


            boss.Upgrade += machine1.Upgrade;
            boss.Upgrade += machine2.Upgrade;
            boss.TurnOn += machine1.TurnOn;
            boss.TurnOn += machine2.TurnOn;
            boss.TurnOn += machine3.TurnOn;
            boss.TurnOn += machine4.TurnOn;

            Console.WriteLine("Первая серия событий:");
            boss.InvokeUpgrade();
            boss.InvokeTurnOn(220);

            Console.WriteLine("\nВторая серия событий:");
            boss.InvokeUpgrade();
            boss.InvokeTurnOn(240);


            string text = "  d/'/'.'']cwc wwwww      efejfnivn!  ";
            Console.WriteLine("Исходная строка: " + text);

            string result = StringProcessor.ProcessString(text,
                StringProcessor.RemovePunctuation,
                StringProcessor.RemoveExtraSpaces,
                StringProcessor.ReplaceWord,
                StringProcessor.ToUpperCase,
                StringProcessor.AddSymbols
                
            );

            Console.WriteLine("Обработанная строка: " + result);
        
    }
    }
}





