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

            List<int> numbers =new() { 1,2,3,4,5,6,7,8,9,10 };

            // Devolver cada numero multiplicado por 3
            //Devolver todos los números excepto el 9
            //Ordenarlos de manera ascendente

            var numbersX3 = numbers.Select(num => num*3)
                .Where(x => x != 9)
                .OrderBy(x => x)
                .ToList();
        }

        public static void SearchExamples()
        {
            List<string> textList = new() {"a","bx","c","d","je","f","c" };

            //Devolver el primer elemento

          var first=  textList.First();

            //Primer elemento que sea c

         var firtsj=   textList.First(text=> text.Equals("c"));

            //Primer elemento que contenga la j

           var containsj= textList.First(text=> text.Contains("j"));

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
            var myEvenNumbers =evenNumbers.Except(otherNumbers); //Devuelve 4,8

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
            enterprise.Employees.Any(employee=> employee.Salary>=2000));


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
                                 select new {Element = element, SecondElement=secondsElement} ;

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
            var myList = new[] {1,2,3,4,5};

            //Saltar los dos primeros o los dos ultimos

            var skip2 = myList.Skip(2); // 3,4,5

            var skiplast2 = myList.SkipLast(2); // 1,2,3

            //Saltar elementos con determinadas caracteristicas

            var skipWhile = myList.SkipWhile(e => e<3);

            //Funciones para coger determinados elementos

            var take2 = myList.Take(2); //Coge los dos primeros

            var takelast2 = myList.TakeLast(2); //Coge los dos ultimos

            var takeWhileSmallerThan4 = myList.TakeWhile(e => e<4);
        }

        //Variables

        //ZIP

        //Repeat

        //All

        //Aggregate

        //Distinct

        //Group by





    }
}