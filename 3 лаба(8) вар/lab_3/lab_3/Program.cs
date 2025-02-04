using System;

namespace Lab3 {
    public class Set<T>
    {
        private HashSet<T> collection;

        public HashSet<T> Collection{  get { return collection; } }
        public class Production
        {
            public int Id { get; set; }
            public string NameOfOrganazation { get; set; }

            public Production(int id, string name)
            {
                Id = id;
                NameOfOrganazation = name;  

            }
        }
        public class Developer
        {
            public string Name {  get; set; }
            public int Id_D { get; set; }
            public string Department { get; set; }
            public Developer(string name, int id_D, string department)
            {
                Name = name;
                Id_D = id_D;
                Department = department;
            }
        }
        public Production Prod { get; set; }
        public Developer Dev { get; set; }
        public Set(IEnumerable<T> elements)
        {
            collection = new HashSet<T>(elements);
            Prod = new Production(134564, "SuperCompany");
            Dev = new Developer("Gritsenko Anna", 4563, "IT Department");
        }

       
        public static bool operator >(T element, Set<T> set)
        {
            return set.collection.Contains(element);
        }
        public static bool operator <(T element, Set<T> set)
        {
            return !set.collection.Contains(element);
        }
        public static Set<T> operator *(Set<T> set1, Set<T> set2)
        {
            return new Set<T>(set1.collection.Intersect(set2.collection));
        }

        
        public static bool operator <(Set<T> subset, Set<T> set)
        {
            return subset.collection.IsSubsetOf(set.collection);
        }
        public static bool operator >(Set<T> superset, Set<T> set)
        {
            return superset.collection.IsSupersetOf(set.collection);
        }
        public override string ToString()
        {
            return "{" + string.Join(", ", collection) + "}";
        }
       
        public DateTime ToDate()
        {
          
            return DateTime.Now;
        }
        public T this[int index]
        {
            get
            {
                if(index < 0 || index >= collection.Count)
                {
                    throw new ArgumentOutOfRangeException("Индекс не попадает в диапазон");
                }
                return collection.ElementAt(index);
            }
        }

        public static class StaticOperation
        {
            public static T Sum(Set<T> set)
            {
                dynamic sum = 0;
                foreach (var element in set.collection) {
                    sum += element;
                }
                return sum;
            }
            public static T Difference(Set<T> set)
            {
                dynamic num1 = set.collection.Max();
                dynamic num2 = set.collection.Min();
                return num1 - num2;

            }
            public static T CountOfElements(Set<T> set)
            {
                dynamic number =  set.collection.Count();
                return number;
            }
        }
    }
    public static class ExtentionMethods 
    {
        public static int FindFirstNumber(this string str)
        {
            var numberString = new string(str.Where(char.IsDigit).ToArray());
            return numberString.Length > 0 ? int.Parse(numberString) : 0;
        }
        public static Set<int> RemovePositive(this Set<int> set)
        {
            set.Collection.RemoveWhere(element => element > 0);
            return set;
        }
    }
        public static class Program
        {
            public static void Main()
            {
                Set<int> collection = new Set<int> (new[] { 1, 2, 3, 4, 5, 6, -7 });
                Set<int> collection3 = new Set<int>(new[] { 1, 2, 3, 4 });
                Set<string> collection2 = new Set<string>(new[] { "a", "b", "c" });
                Set<string> collection4 = new Set<string>(new[] { "a", "b", "c","d","f" });

                Console.WriteLine(5 > collection);
                Console.WriteLine(collection > collection3);
                Console.WriteLine(collection4 < collection2);
                Console.WriteLine(collection4 * collection2);

                Console.WriteLine(collection.ToDate());
                Console.WriteLine(Set<int>.StaticOperation.Sum(collection));
                Console.WriteLine(Set<int>.StaticOperation.Difference(collection));
                Console.WriteLine(Set<int>.StaticOperation.CountOfElements(collection));
                string example = "fpfpfp896fpfppffp 96 fffpfpfpp";
                Console.WriteLine("Первое число в строке: " + example.FindFirstNumber()); 

            
                Console.WriteLine("Множество до удаления положительных элементов: " + collection3.ToString());
                collection3.RemovePositive(); 
                Console.WriteLine("Множество после удаления положительных элементов: " + collection3.ToString());
        }
    }
        }
    
