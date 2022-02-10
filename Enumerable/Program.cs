using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;


namespace Test_Enumerable
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var r = new Random();

            IEnumerable<int> numbers = Enumerable.Repeat(0, 4).Select(x => r.Next(20)).ToList();
            numbers.ToList().ForEach(x => Console.WriteLine(x));

            Console.WriteLine();

            int sum = numbers.Aggregate((x, y) => { return x + y; });
            int max = numbers.MyAggregate((x, y) => Math.Max(x, y));


            
            ////  agregacia romelic momidzebnis mxolod kenti ricxvebis maximums
            int oddMax = numbers.MyAggregate((x, y) => y % 2 != 0 ? Math.Max(x, y) : x);
            
            ////SelectMany 
            //ToDictionary 
            //Zip da Lookup 
            //GroupBy
            //OfType
            
            
            /////////
            

            ////SelectMany - daareserchet da daweret
            //var list = new List<string>() { "abcd", "abcd" };
            //var methodSyntax = list.MySelectMany(x => x);


            //ToDictionary - daareserchet da daweret
            //var myDictionary = numbers.MyToDictionary(x => x, x => true);


            ////Zip da Lookup - ertad gavarchiot
            /*      
                  int[] numbersSequence = { 10, 20, 30, 40, 50 };
                  string[] wordsSequence = { "Ten", "Twenty", "Thirty", "Fourty" };
                  var resultSequence = numbersSequence.MyZip(wordsSequence, (first, second) => first + " - " + second);
                  foreach (var item in resultSequence)
                  {
                      Console.WriteLine(item);
                  }
            */

            /*
                        ////GroupBy
                        IList<Student> studentList = new List<Student>()
                        {
                            new() { StudentID = 1, StudentName = "John", Age = 18 } ,
                            new() { StudentID = 2, StudentName = "Steve",  Age = 21 } ,
                            new() { StudentID = 3, StudentName = "Bill",  Age = 18 } ,
                            new() { StudentID = 4, StudentName = "Ram" , Age = 20 } ,
                            new() { StudentID = 5, StudentName = "Abram" , Age = 21 }
                        };

                        //var groupedResult1 = from s in studentList group s by s.Age;

                        var groupedResult2 = studentList.MyGroupBy(s => s.Age);
            */

            ////GroupBy Example2
            /*          
                      List<Pet> petsList =
                              new List<Pet>{ new Pet { Name="Barley", Age=8.3 },
                              new Pet { Name="Boots", Age=4.9 },
                              new Pet { Name="Whiskers", Age=1.5 },
                              new Pet { Name="Daisy", Age=4.3 } };

                      var query = petsList.MyGroupBy(
                          pet => Math.Floor(pet.Age),
                          (age, pets) => new
                          {
                              Key = age,
                              Count = pets.Count(),
                              Min = pets.Min(pet => pet.Age),
                              Max = pets.Max(pet => pet.Age)
                          });

                      foreach (var result in query)
                      {
                          Console.WriteLine("\nAge group: " + result.Key);
                          Console.WriteLine("Number of pets in this age group: " + result.Count);
                          Console.WriteLine("Minimum age: " + result.Min);
                          Console.WriteLine("Maximum age: " + result.Max);
                      }
          */


            //OfType

            /*
            object[] array = new object[4];
            array[0] = new StringBuilder();
            array[1] = "example";
            array[2] = new int[1];
            array[3] = "another";

            // Filter the objects by their type.
            // ... Only match strings.
            // ... Print those strings to the screen.
            var result = array.MyOfType<string>();
            foreach (var element in result)
            {
                Console.WriteLine(element);
            }
            
            */
        }

        public class Student
        {
            public Student()
            {
            }

            public int StudentID { get; set; }
            public string StudentName { get; set; }
            public int Age { get; set; }
        }

        public class Pet
        {
            public string Name { get; set; }
            public double Age { get; set; }
        }

        public class Product
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public double Price { get; set; }
        }
    }
}


/*
 * foreach (var item in groupedResult2)
               {
                   Console.WriteLine($"KEY: {item.Key}:");
                   foreach (var element in item)
                   {
                       Console.WriteLine($"\t {element.Age}, {element.StudentName}, {element.StudentName}, {element.StudentID}");
                   }
               }
 */