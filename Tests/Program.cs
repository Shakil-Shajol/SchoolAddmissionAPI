using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            List<MyClass> list = new List<MyClass>
            {
                new MyClass{id=1,Name="Zamal", Score=95},
                new MyClass{id=2,Name="Mamal", Score=98},
                new MyClass{id=3,Name="Kamal", Score=100},
                new MyClass{id=4,Name="Bamal", Score=99}
            };
            Console.WriteLine("Main");
            foreach (var item in list)
            {
                Console.WriteLine(item.Score);
            }
            Console.WriteLine("Sorted");
            list = list.OrderByDescending(x => x.Score).ToList();
            foreach (var item in list)
            {
                Console.WriteLine(item.Score);
            }
            GC.Collect();
            Console.ReadLine();
            List<MyClass> classes = new List<MyClass>();
            GC.Collect();
            Console.WriteLine("Top 2 value");
            classes = list.Take(2).ToList();
            foreach (var item in classes)
            {
                Console.WriteLine(item.Score);
            }
            Console.WriteLine("after 2 value");
            classes.Clear();
            classes = list.Skip(2).Take(1).ToList();
            foreach (var item in classes)
            {
                Console.WriteLine(item.Score);
            }
            Console.ReadLine();
        }
    }
    class MyClass
    {
        public int id { get; set; }
        public string Name { get; set; }
        public double Score { get; set; }
    }

}
