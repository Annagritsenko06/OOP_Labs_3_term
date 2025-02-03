using System;


namespace Program
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
        public Phone() : this("Неизвестная фамилия","Неизвестное имя","Неизвестное отчество","Неизвестный адрес","0000-0000-0000-0000",0,0,0,0)
        { 

        }  
        public Phone(string lastName,string firstName,string middlename, string adress,string cardNumber,decimal debit, decimal credit,int callsInCity, int callsOutOfCity)
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
                if (value<0)
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
                { Console.WriteLine("Текущий дебит превышает максимальный..."); 
                message= false;
                }
            }
        }


        public override string ToString()
        {
            return $"ФИО:{Surname} {Name} {MiddleName},ID:{_Id}, баланс: {CalculateBalance()}";
        }
        public override bool Equals(object? obj)
        {
            if(obj is Phone phone)
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
            Phone phone1 = new Phone("Гриценко", "Анна", "Александровна", "ул. Шугаева, д. 13/1", "3744-4554-7531-0550", 1000m, 200m, 120, 50);
            Phone phone4 = new Phone("Гриценко", "Анна", "Александровна", "ул. Шугаева, д. 13/1", "3744-4554-7531-0550", 1000m, 200m, 120, 50);
            Phone phone2 = new Phone();
            Phone phone5 = new Phone();
            Phone phone3 = new Phone("Петров", "Юрий", "Сергеевич","Ул.Свердлова, д.12","3454-4553-5117-0066", 123m, 333m, 100, 20);

            decimal MaxDebit = 1000m;
            bool message = true;
            Console.WriteLine("Для первого клиента:");
            phone1.InformationAboutDebit(ref MaxDebit, out message);

            Console.WriteLine(phone2.ToString());
            Console.WriteLine(phone2.Equals(phone3));
            Console.WriteLine(phone3);

            Console.WriteLine($"Сравнение первого и второго клиента : {phone1.Equals(phone2)}");

            Console.WriteLine($"Проверка типа объекта для первого клиента: {phone1 is Phone}");

            Console.WriteLine("Вывод статической информации о классе");
            Phone.PrintClassInfo();

            var anonymousPhone = new
            {
                phone1.middleName,
                phone1.FirstName,
                phone1.LastName,
                phone1.cardNum,
                phone1._callsInCity,
                phone1._callsOutOfCity
            };

            Console.WriteLine($"Анонимный тип: {anonymousPhone}");


            Phone[] phones = new Phone[] {
             new Phone("Гриценко", "Анна", "Александровна", "ул. Шугаева, д. 13/1", "3744-4554-7531-0550", 1000m, 200m, 120, 50),
             new Phone("Иванова", "Ирина", "Михайловна", "ул. Нестерова, д. 3", "8904-5554-7123-4440", 200m, 400m, 160, 90),
             new Phone("Петров", "Юрий", "Сергеевич", "Ул.Свердлова, д.12", "3454-4553-5117-0066", 123m, 333m, 100, 0)
        };
            Console.WriteLine("Абоненты с временем городских разговоров больше 100 минут:");
            foreach (var phone in phones)
            {
                if (phone._callsInCity > 100)
                {
                    Console.WriteLine(phone);
                }
            }

            Console.WriteLine("Абоненты, которые пользовались междугородной связью:");
            foreach (var phone in phones)
            {
                if (phone._callsOutOfCity > 0)
                {
                    Console.WriteLine(phone);
                }
            }
            ProgramBase NewEltment = new ProgramBase();
            NewEltment.Write();
            
        }

    }
}