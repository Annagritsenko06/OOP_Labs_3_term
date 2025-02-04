using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Collections.Specialized;


namespace Lab_9
{
    
    class Furniture
    {
        public string Name { get; set; }
        public string Material { get; set; }
        public decimal Price { get; set; }

        public Furniture(string name, decimal price, string material)
        {
            Name = name;
            Price = price;
            Material = material;
        }

        public override string ToString()
        {
            return $"Наименование продукта: {Name}, Материал продукта: {Material}, Цена: {Price:C}";
        }
    }

    class FurnitureCollection : IList
    {
        private ArrayList _list = new ArrayList();

        
        public object this[int index]
        {
            get => _list[index];
            set
            {
                if (value is Furniture furniture)
                {
                    _list[index] = furniture;
                }
                else
                {
                    throw new ArgumentException("Объект должен быть типа Furniture.");
                }
            }
        }

        
        public int Count => _list.Count;
        public bool IsReadOnly => false;
        public bool IsFixedSize => false;
        public object SyncRoot => this;
        public bool IsSynchronized => false;


        public int Add(object value)
        {
            if (value is Furniture furniture)
            {
                return _list.Add(furniture);
            }
            throw new ArgumentException("Объект должен быть типа Furniture.");
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(object value)
        {
            if (_list.Contains(value))
            {
                Console.WriteLine($"Элемент {value} является элементом коллекции") ;
                return true;
            }
            Console.WriteLine($"Элемент {value} отсутствует в коллекции");

            return false;
        }

        public void CopyTo(Array array, int index)
        {
            _list.CopyTo(array, index);
        }

        public IEnumerator GetEnumerator()
        {
            return _list.GetEnumerator();
        }
        
        public int IndexOf(object value)
        {
            return _list.IndexOf(value);
        }

        public void Insert(int index, object value)
        {
            if (value is Furniture furniture)
            {
                _list.Insert(index, furniture);
            }
            else
            {
                throw new ArgumentException("Объект должен быть типа Furniture");
            }
        }

        public void Remove(object value)
        {
            _list.Remove(value);
        }

        public void RemoveAt(int index)
        {
            _list.RemoveAt(index);
        }
    }

    class Program
    {
       static void Furniture_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) 
        {
            switch(e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    if (e.NewItems?[0] is Furniture newFurniture)
                    {
                        Console.WriteLine($"Добавлен новый объект: {newFurniture.Name}");
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (e.OldItems?[0] is Furniture oldFurniture)
                    {
                        Console.WriteLine($"Удален объект: {oldFurniture.Name}");
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    if ((e.NewItems?[0] is Furniture replacingFurniture) &&
                       e.OldItems?[0] is Furniture replacedFurniture) 
                       Console.WriteLine($"Объект {replacedFurniture.Name} заменен объектом {replacingFurniture.Name}");
                    break;

            }
        }
        static void Main(string[] args)
        {

            FurnitureCollection furnitureCollection = new FurnitureCollection();

            Furniture chair = new Furniture("Стул", 50.00m, "дерево");
            Furniture table = new Furniture("Стол", 150.00m, "Прессованные опилки");
            Furniture sofa = new Furniture("Диван", 300.00m, "Кожа,текстиль");

            furnitureCollection.Add(chair);
            furnitureCollection.Add(table);
            furnitureCollection.Add(sofa);

            Console.WriteLine("Содержимое коллекции мебели:");
            foreach (Furniture furniture in furnitureCollection)
            {
                Console.WriteLine(furniture);
            }

            furnitureCollection.Remove(table);

            Console.WriteLine("\nПосле удаления стола:");
            foreach (Furniture furniture in furnitureCollection)
            {
                Console.WriteLine(furniture);
            }
            
            Console.WriteLine("\nПоиск элемента:");
            furnitureCollection.Contains(table);
            
            ArrayList list = new ArrayList();
            list.Add(1);
            list.Add(2);
            list.Add("hhhhh");
            list.Add("i");
            list.Add(7.6);
            foreach (object o in list)
            {
                Console.WriteLine(o);
            }
            list.RemoveRange(0, 2);
            Console.WriteLine("Коллекция после удаления:");
            foreach (object o in list)
            {
                Console.WriteLine(o);
            }

            list.AddRange(new string[] { "apple", "bdeibei" });
            List<object> list2 = new List<object>();
            list2.AddRange( list.Cast<object>());
            Console.WriteLine("Коллекция List:");
                foreach (object o in list2)
            {
                Console.WriteLine(o);
            }
            Console.WriteLine("Поиск заданного значения:");
            if (list2.Contains("apple"))
            {
                Console.WriteLine("Коллекция содержит элемент apple");
            }
            else Console.WriteLine("Коллекция не содержит элемент apple");
            
            var collection = new ObservableCollection<Furniture>()
            {
                new Furniture("nnenoe",333,"eeme"),
                new Furniture("deede",999,"eineoxneoxne")

            };
            collection.CollectionChanged += Furniture_CollectionChanged;
            collection.Add(new Furniture("yyyyy", 99876, "xmex"));
            collection.RemoveAt(0);
            collection[0] = new Furniture("u", 34, "poih");
            Console.WriteLine("СПИСОК ПОЛЬЗОВАТЕЛЕЙ:");
            foreach(object o in collection)
            {
                Console.WriteLine(o.ToString());
            }
        }

        
    }
}
