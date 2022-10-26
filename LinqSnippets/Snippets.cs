using System;
using System.Linq;

namespace LinqSnippets
{
    public class Snippets
    {
        public static void BasicLinq()
        {

            string[] cars = { "WF Golf", "Audi A3", "Audi A5", "Fiat Punto", "Seat Ibiza", "Seat León" };

            // 1. Select * from cars
            var carlist = from car in cars select car;

            //2. Select * from cars where Car in Audi

            var audiList = from car in cars where car.Contains("Audi") select car;
        }

        public static void NumbersLinq()
        {

            List<int> numbers = new() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            // Devolver cada numero multiplicado por 3
            //Devolver todos los números excepto el 9
            //Ordenarlos de manera ascendente

            var numbersX3 = numbers.Select(num => num * 3)
                .Where(x => x != 9)
                .OrderBy(x => x)
                .ToList();
        }

        public static void SearchExamples()
        {
            List<string> textList = new() { "a", "bx", "c", "d", "je", "f", "c" };

            //Devolver el primer elemento

            var first = textList.First();

            //Primer elemento que sea c

            var firtsj = textList.First(text => text.Equals("c"));

            //Primer elemento que contenga la j

            var containsj = textList.First(text => text.Contains("j"));

            //Primer elemento que contenga la z o default -> Devuelve el elemento o un valor vacio.

            var containsz = textList.FirstOrDefault(text => text.Contains("z"));

            //Ultimo elemento que contenga la z o default -> Devuelve el elemento o un valor vacio.

            var containsLastZ = textList.LastOrDefault(text => text.Contains("z"));

            //Single values

            var unique = textList.Single();
            var uniqueOrDefault = textList.SingleOrDefault();

            int[] evenNumbers = { 0, 2, 4, 6, 8 };
            int[] otherNumbers = { 0, 2, 6 };

            //Obtener los que no estén en el array de abajo
            var myEvenNumbers = evenNumbers.Except(otherNumbers); //Devuelve 4,8

        }

        public static void MultipleSelects()
        {
            //Select many

            string[] myOpinions = { "Opinion1, text1", "Opinion2, text2", "Opinion3, text3" };

            //Hacer split
            var myOpinionSelection = myOpinions.SelectMany(opinion => opinion.Split(","));

            Console.WriteLine(myOpinionSelection);

            //Operaciones con Enterprises

            var enterprises = new[]
            {
                //Primer enterprise
                new Enterprise()
                {
                    Id =1,
                    Name ="Enterprise1",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=1,
                            Name="Paco",
                            Email ="paco@gmail.com",
                            Salary = 2000
                        },
                          new Employee
                        {
                            Id=2,
                            Name="Paca",
                            Email ="pacapaca@gmail.com",
                            Salary = 1000
                        },
                            new Employee
                        {
                            Id=3,
                            Name="Pepa",
                            Email ="pepa@gmail.com",
                            Salary = 2200
                        },
                    }
                },
                //Segundo Enterprise
                 new Enterprise()
                {
                    Id =2,
                    Name ="Enterprise2",
                    Employees = new[]
                    {
                        new Employee
                        {
                            Id=4,
                            Name="Ana",
                            Email ="Ana@gmail.com",
                            Salary = 1500
                        },
                          new Employee
                        {
                            Id=5,
                            Name="Juana",
                            Email ="Juana@gmail.com",
                            Salary = 1800
                        },
                            new Employee
                        {
                            Id=6,
                            Name="Colombo",
                            Email ="Colombo@gmail.com",
                            Salary = 3100
                        },
                    }
                }

            };

            // Obtener todos los empleados de todas las empresas

            var employeeList = enterprises.SelectMany(e => e.Employees);

            //Saber si cualquier lista esta vacia -> Comprobar si hay empresas // empleados

            bool hasEnterprises = enterprises.Any();

            bool hasEmployees = enterprises.Any(enterprise => enterprise.Employees.Any());

            // Devolver los enterprises cuyos employees ganen al menos 2000 euros

            bool hasEmployeesMore2000 = enterprises.Any(enterprise =>
            enterprise.Employees.Any(employee => employee.Salary >= 2000));


        }

        public static void LinqCollections()
        {
            var firstList = new List<string>() { "a", "b", "c" };
            var secondList = new List<string>() { "a", "c", "d" };

            //Inner Join (elementos comunes a ambas listas) Dos posibilidades:

            var commons = from element in firstList
                          join secondsElement in secondList
                          on element equals secondsElement
                          select new { element, secondsElement };

            var commonResult2 = firstList.Join(
                secondList,
                element => element,
                secondElement => secondElement,
                (element, secondsElement) => new { element, secondsElement }
                );

            //OuterJoin (Left/Right)

            //Left Join
            var LeftOuterJoin = from element in firstList
                                join secondsElement in secondList
                                on element equals secondsElement
                                into temporalList //Hasta aquí el join normal, ahora hay que restar los comunes
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where element != temporalElement
                                select new { Element = element };


            var LeftOuterJoin2 = from element in firstList
                                 from secondsElement in secondList //Aqui ya tenemos el join normal
                                 .Where(e => e == element).DefaultIfEmpty()
                                 select new { Element = element, SecondElement = secondsElement };

            //Right Join
            var RightOuterJoin =
                                from secondsElement in secondList
                                join element in firstList
                                on secondsElement equals element
                                into temporalList
                                from temporalElement in temporalList.DefaultIfEmpty()
                                where secondsElement != temporalElement
                                select new { SecondElement = secondsElement };

            //Union

            // var UnionList = LeftOuterJoin.Union(RightOuterJoin);

        }

        public static void SkipTakeLinq()
        {
            var myList = new[] { 1, 2, 3, 4, 5 };

            //Saltar los dos primeros o los dos ultimos

            var skip2 = myList.Skip(2); // 3,4,5

            var skiplast2 = myList.SkipLast(2); // 1,2,3

            //Saltar elementos con determinadas caracteristicas

            var skipWhile = myList.SkipWhile(e => e < 3);

            //Funciones para coger determinados elementos

            var take2 = myList.Take(2); //Coge los dos primeros

            var takelast2 = myList.TakeLast(2); //Coge los dos ultimos

            var takeWhileSmallerThan4 = myList.TakeWhile(e => e < 4);
        }

        //Paginación

        public IEnumerable<T> GetPage<T>(IEnumerable<T> collection, int pageNumber, int resultsPerPage)
        {
            int startIndex = (pageNumber - 1) * resultsPerPage;

            return collection.Skip(startIndex).Take(resultsPerPage);
        }

        //Generacion de variables en LINQ

        public static void LinqVariables()
        {
            int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Numeros mayores que la media
            var mayorQuelamedia = from number in numbers
                                  let average = numbers.Average()
                                  where number > average
                                  select number;

            foreach (int number in numbers)
            {
                Console.WriteLine(number);
            }
        }


        //ZIP -> Como una cremallera, coge alternativamente de dos listas

        public static void ZipLinq()
        {
            int[] numbers = { 1, 2, 3, 4, 5 };
            string[] stringNumbers = { "one", "two", "three", "four", "five" };

            IEnumerable<string> zipNumbers = numbers.Zip(stringNumbers, (number, word) => number + " = " + word);

            //{ "1 = one" , "2=two" ...}
        }

        //Repeat & Range

        public static void repeatRangeLinq()
        {
            //Generate collection from 1 - 1000

            var first1000 = Enumerable.Range(0, 1000);

            //Repeat a value N times

            var fiveXs = Enumerable.Repeat("X", 5); //{ "X","X", "X", "X", "X" }

        }


        public static void studentsLinq()
        {

            var classRoom = new[]
            {
                new Student
                {
                    Id=1,
                    Name="Paco",
                    Grade=90,
                    Certified=true
                },
                      new Student
                {
                    Id=2,
                    Name="Marta",
                    Grade=100,
                    Certified=true
                },
                            new Student
                {
                    Id=3,
                    Name="Paca",
                    Grade=40,
                    Certified=false
                },
                                  new Student
                {
                    Id=4,
                    Name="Juana",
                    Grade=49,
                    Certified=false
                },
                                        new Student
                {
                    Id=5,
                    Name="Paula",
                    Grade=60,
                    Certified=true
                }
            };

            var certifiedStudents = from student in classRoom
                                    where student.Certified
                                    select student;

            var notCertifiedStudents = from student in classRoom
                                       where !student.Certified
                                       select student;

            var NameApprobedStudents = from student in classRoom
                                       where student.Grade >= 50 && student.Certified
                                       select student.Name;
        }

        //All -> Booleano que dice si todos los elementos iterados cumplen una condicion

        public static void AllLinq()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5 };

            bool allAreSmallerThan10 = numbers.All(x => x < 10); // True

            bool allAreBiggerOrEqualThan2 = numbers.All(x => x >= 2); //False

        }

        //Aggregate -> Secuencias acumulativas de funciones

        public static void AGregateLinq()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Sum all numbers

            int sum = numbers.Aggregate((sumaActual, valorActual) => sumaActual + valorActual);

            //Aggregate all words
            string[] words = { "hello,", "my", "name", "is", "Pablo" };

            string phrase = words.Aggregate((phraseActual, newWord) => phraseActual + " " + newWord);


        }

        //Distinct -> Devuelve los valores sin repetirlos

        public static void DistinctValues()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1, 3, 5, 6 };

            IEnumerable<int> numbersNotRepeated = numbers.Distinct();

            IEnumerable<int> numbersNotRepeatedGreaterThan5 = numbers.Distinct().Where(x => x > 5);

        }

        //Group by -> Agrupar por condiciones

        public static void groupByExamples()
        {
            var numbers = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            //Obtener solo los valores pares y generar dos grupos

            var groups = numbers.GroupBy(x => x % 2 == 0);
            //Esto devuelve dos grupos:
            // 1º Los que no cumplen la condicion
            // 2º Los que la cumplen

            foreach (var group in groups)
            {
                foreach (var value in group)
                {
                    Console.WriteLine(value); // {1,3,5,7,9} , {2,4,6,8,10}
                }
            }

            var classRoom = new[]
          {
                new Student
                {
                    Id=1,
                    Name="Paco",
                    Grade=90,
                    Certified=true
                },
                      new Student
                {
                    Id=2,
                    Name="Marta",
                    Grade=100,
                    Certified=true
                },
                            new Student
                {
                    Id=3,
                    Name="Paca",
                    Grade=40,
                    Certified=false
                },
                                  new Student
                {
                    Id=4,
                    Name="Juana",
                    Grade=49,
                    Certified=false
                },
                                        new Student
                {
                    Id=5,
                    Name="Paula",
                    Grade=60,
                    Certified=true
                }
            };

            var certifiedQuery = classRoom.GroupBy(s => s.Certified);

            //Comentarios relacionados con post

            List<Post> post = new List<Post>()
        {
            new Post()
            {
                Id = 1,
                Title="My first post",
                Content = "My first content",
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 1,
                        Title="My first comment",
                        Content = "My first comment content"
                    },
                      new Comment()
                    {
                        Id = 2,
                        Title="My second comment",
                        Content = "My second comment content"
                    }
                }

            },

                new Post()
            {
                Id = 2,
                Title="My second post",
                Content = "My second content",
                Comments = new List<Comment>()
                {
                    new Comment()
                    {
                        Id = 3,
                        Title="My third comment",
                        Content = "My third comment content"
                    },
                      new Comment()
                    {
                        Id = 4,
                        Title="My fourth comment",
                        Content = "My fourth comment content"
                    }
                }

            }
        };

            //Todos los comentarios que tengan contenido de un post

            var commentsWithContent = post.SelectMany
            (post => post.Comments,
            (post, comment) => new { PostId = post.Id, CommentContent = post.Content });

        }

    }
}