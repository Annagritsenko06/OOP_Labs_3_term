using System;
using System.Linq;
using System.Net.WebSockets;
using System.Numerics;
using static System.Net.Mime.MediaTypeNames;
namespace Lab10
{
    class Phone
    {
        private readonly int Id;
        private string Surname;
        private string Name;
        private string MiddleName;
        private string Adress;
        private string CardNumber;
        private decimal Debit;
        private decimal Credit;
        private int TimeOfCallsINCity;
        private int TimeOfCallsOutOfCity;

        public const int MaxTimeOfCityCalls = 250;

        public static int ObjectCount { get; private set; }

        static Phone()
        {
            ObjectCount = 0;
        }
        private Phone(bool Itstrue = true)
        {
            Id = Guid.NewGuid().GetHashCode();

        }
        public Phone() : this("Неизвестная фамилия", "Неизвестное имя", "Неизвестное отчество", "Неизвестный адрес", "0000-0000-0000-0000", 0, 0, 0, 0)
        {

        }
        public Phone(string lastName, string firstName, string middlename, string adress, string cardNumber, decimal debit, decimal credit, int callsInCity, int callsOutOfCity)
        {
            Id = Guid.NewGuid().GetHashCode();
            Surname = lastName;
            Name = firstName;
            MiddleName = middlename;
            Adress = adress;
            CardNumber = cardNumber;
            Debit = debit;
            Credit = credit;
            TimeOfCallsINCity = callsInCity;
            TimeOfCallsOutOfCity = callsOutOfCity;
            ObjectCount++;
        }

        public int _Id
        {
            get { return Id; }
        }
        public string LastName
        {
            get { return Surname; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Фамилия не может быть пустой.");
                }
                Surname = value;
            }
        }
        public string FirstName
        {
            get { return Name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Имя не может быть пустым.");
                }
                Name = value;
            }
        }
        public string middleName
        {
            get { return MiddleName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Отчество не может быть пустым.");
                }
                MiddleName = value;
            }
        }
        public string _adress
        {
            get { return Adress; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Адрес не может быть пустым.");
                }
                Adress = value;
            }
        }
        public string cardNum
        {
            get { return CardNumber; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Номер карты не может быть пустым.");
                }
                CardNumber = value;
            }
        }
        public decimal _debit
        {
            get { return Debit; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Дебет не может быть отрицательным");
                }
                Debit = value;
            }
        }
        public decimal _credit
        {
            get { return Credit; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Кредит не может быть отрицательным");
                }
                Credit = value;
            }
        }
        public int _callsInCity
        {
            get { return TimeOfCallsINCity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Это поле не может быть отрицательным");
                }
                TimeOfCallsINCity = value;
            }
        }
        public int _callsOutOfCity
        {
            get { return TimeOfCallsOutOfCity; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Это поле не может быть отрицательным");
                }
                TimeOfCallsOutOfCity = value;
            }
        }

        public decimal CalculateBalance()
        {
            return Debit - Credit;
        }

        public static void PrintClassInfo()
        {
            Console.WriteLine($"Класс Phone, количество созданных объектов: {ObjectCount}");
        }

        public void InformationAboutDebit(ref decimal MaxDebit, out bool message)
        {
            if (Debit > MaxDebit)
            {
                Console.WriteLine("Текущий дебит не превышает максимальный...");
                message = true;
            }
            else
            {
                {
                    Console.WriteLine("Текущий дебит превышает максимальный...");
                    message = false;
                }
            }
        }


        public override string ToString()
        {
            return $"ФИО:{Surname} {Name} {MiddleName},ID:{_Id}, баланс: {CalculateBalance()}";
        }
        public override bool Equals(object? obj)
        {
            if (obj is Phone phone)
            {
                return this.Id == phone._Id;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return _Id.GetHashCode();
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
           
            string[] months = { "December", "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November" };
            var selectedMonth = months.Where(p => p.Length < 6);
            foreach (string month in selectedMonth)
            {
                Console.WriteLine(month);
            }
            var SummerAndWinterMonth = months.Where(months => months == "December" || months == "January" || months == "February" || months== "June" || months == "July" || months == "August");
            Console.WriteLine("запрос, возвращающий только летние и зимние месяцы");
            foreach (string month in SummerAndWinterMonth)
            {
                Console.WriteLine(month);
            }
            var sortedMonths = months.OrderBy(month => month);
            Console.WriteLine($"Месяцы в алфавитном порядке: {string.Join(", ", sortedMonths)}");

            
            var monthsContainingU = months.Where(month => month.Contains('u') && month.Length >= 4);
            Console.WriteLine($"Месяцы, содержащие букву 'u' и длиной не менее 4-х: {string.Join(", ", monthsContainingU)}");
            
           
            
            List<Phone> listOfPhones = new List<Phone> {

        new Phone("Гриценко", "Анна", "Александровна", "ул. Шугаева, д. 13/1", "3744-4554-7531-0550", 1000m, 200m, 120, 50),
        new Phone("Иванова", "Ирина", "Михайловна", "ул. Нестерова, д. 3", "8904-5554-7123-4440", 200m, 400m, 160, 90),
        new Phone("Петров", "Юрий", "Сергеевич", "Ул. Свердлова, д. 12", "3454-4553-5117-0066", 123m, 333m, 100, 0),
        new Phone("Сидорова", "Мария", "Викторовна", "пр. Ленина, д. 15", "6789-1234-5678-9101", 1500m, 300m, 140, 60),
        new Phone("Кузнецов", "Алексей", "Иванович", "ул. Крылатая, д. 7", "2345-6789-0123-4567", 2500m, 500m, 180, 70),
        new Phone("Павлова", "Елена", "Петровна", "ул. Толстого, д. 5", "9876-5432-1098-7654", 1200m, 250m, 130, 40),
        new Phone("Семенов", "Дмитрий", "Анатольевич", "ул. Лермонтова, д. 21", "4567-8901-2345-6789", 800m, 150m, 110, 20),
        new Phone("Александров", "Олег", "Федорович", "ул. Цветочная, д. 9", "1234-5678-9012-3456", 600m, 180m, 115, 30),
        new Phone("Федорова", "Татьяна", "Игоревна", "пр. Победы, д. 10", "3210-9876-5432-1098", 950m, 220m, 125, 80)
        };
            var highDebitPhones = from phone in listOfPhones
                                  where phone._debit > 500
                                 select phone;

            Console.WriteLine("Телефоны с дебетом больше 500:");
            foreach (var phone in highDebitPhones)
            {
                Console.WriteLine(phone);
            }
            

            var callsInCityPhones = listOfPhones.Where(phone => phone._callsInCity > 100).ToList();
            Console.WriteLine("сведения об абонентах, у которых время внутригородских разговоров превышает 100:");
            foreach (var phone in callsInCityPhones)
            {
                Console.WriteLine(phone);
            }
            var callsOutCityPhones = listOfPhones.Where(phone => phone._callsOutOfCity > 0).ToList();
            Console.WriteLine("сведения об абонентах, которые пользовались междугородной связью:");
            foreach (var phone in callsOutCityPhones)
            {
                Console.WriteLine(phone);
            }
            var coutDebit=listOfPhones.Count(predicate => predicate._debit > 500);
            Console.WriteLine($"Количество абонентов с дебитом больше 500 : {coutDebit}");
            var max = listOfPhones.OrderByDescending(phone => phone._credit).FirstOrDefault();

            Console.WriteLine($"Абонент с максимальным значением кредита:{max}");
            
            var arrayBySurname=listOfPhones.OrderBy(phone => phone.LastName).ToList();
            Console.WriteLine("упорядоченный список абонентов по фамилии: ");
            foreach (var phone in arrayBySurname)
            {
                Console.WriteLine(phone);
            }
            var question = listOfPhones.Where(phone => phone._callsOutOfCity > 0).OrderBy(phone => phone.FirstName).Select(phone => new { phone.LastName, phone.FirstName, phone._credit }).Skip(1).All(phone => phone._credit == 100);
            Console.WriteLine(question);
            var monthCategories = new List<(string Month, string Category)>
        {
            ("December", "Winter"),
            ("January", "Winter"),
            ("February", "Winter"),
            ("March", "Spring"),
            ("April", "Spring"),
            ("May", "Spring"),
            ("June", "Summer"),
            ("July", "Summer"),
            ("August", "Summer"),
            ("September", "Fall"),
            ("October", "Fall"),
            ("November", "Fall")
        };
            
            var joinedMonths = from month in months
                               join category in monthCategories on month equals category.Month
                               select new { Month = month, Category = category.Category };

            Console.WriteLine("Месяцы с их категориями:");
            foreach (var item in joinedMonths)
            {
                Console.WriteLine($"{item.Month} - {item.Category}");
            }

        }
    }
}