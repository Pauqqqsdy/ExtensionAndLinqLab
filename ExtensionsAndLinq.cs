using CarLibrary;
using DataStructuresLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExtensionAndLinqLab
{
    public class ExtensionsAndLinq
    {
        public static double FindMaxCostByYearLINQ(Queue<List<Transport>> transportQueue, int minYear)
        {
            return (from transportList in transportQueue
                    from transport in transportList
                    where transport is Truck && transport.Year > minYear
                    select transport.Cost).Max();
        }

        public static double FindMaxCostByYearExtension(Queue<List<Transport>> transportQueue, int minYear)
        {
            return transportQueue
                .SelectMany(transportList => transportList)
                .Where(transport => transport is Truck && ((Truck)transport).Year > minYear)
                .Max(transport => transport.Cost);
        }

        public static IEnumerable<Transport> UnionTrucksAndOffroadCarsLINQ(Queue<List<Transport>> transportQueue)
        {
            var trucks = (from transportList in transportQueue
                          from transport in transportList
                          where transport is Truck
                          select transport).ToList();

            var offroadCars = (from transportList in transportQueue
                               from transport in transportList
                               where transport is OffroadCar
                               select transport).ToList();

            return trucks.Union(offroadCars);
        }

        public static IEnumerable<Transport> UnionTrucksAndOffroadCarsExtension(Queue<List<Transport>> transportQueue)
        {
            var trucks = transportQueue
                .SelectMany(transportList => transportList)
                .Where(transport => transport is Truck);

            var offroadCars = transportQueue
                .SelectMany(transportList => transportList)
                .Where(transport => transport is OffroadCar);

            return trucks.Union(offroadCars);
        }

        public static IEnumerable<Transport> GroupingPassengerCarsBySeatsLINQ(Queue<List<Transport>> transportQueue)
        {
            TransportWorkshops.WriteColorMessage("LINQ метод: ", ConsoleColor.Cyan);
            var grouping = from transportList in transportQueue
                            from transport in transportList
                            where transport is PassengerCar
                            group transport by ((PassengerCar)transport).SeatsNumber;

            foreach(var transports in grouping)
            {
                TransportWorkshops.WriteColorMessage($"Автомобилей с {transports.Key} сиденьями: " + transports.Count(), ConsoleColor.Yellow);
                TransportWorkshops.WriteColorMessage("Список автомобилей:", ConsoleColor.Green);

                foreach (var transport in transports)
                {
                    Console.WriteLine(transport);
                }
            }

            return grouping.SelectMany(group => group);
        }

        public static IEnumerable<Transport> GroupingPassengerCarsBySeatsExtension(Queue<List<Transport>> transportQueue)
        {
            TransportWorkshops.WriteColorMessage("Метод расширения: ", ConsoleColor.Cyan);
            var grouping = transportQueue
                .SelectMany(transportList => transportList)
                .Where(transport => transport is PassengerCar)
                .GroupBy(transport => ((PassengerCar)transport).SeatsNumber);

            foreach (var transports in grouping)
            {
                TransportWorkshops.WriteColorMessage($"Автомобилей с {transports.Key} сиденьями: " + transports.Count(), ConsoleColor.Yellow);
                TransportWorkshops.WriteColorMessage("Список автомобилей:", ConsoleColor.Green);

                foreach (var transport in transports)
                {
                    Console.WriteLine(transport);
                }
            }

            return grouping.SelectMany(group => group);
        }

        public static void GetNewTypeOffroadCarsLINQ(Queue<List<Transport>> transportQueue)
        {
            var newType = from transportList in transportQueue
                          from transport in transportList
                          where transport is OffroadCar
                          select new { Марка = transport.Brand, Выпуск = transport.Year };

            TransportWorkshops.WriteColorMessage("LINQ метод:", ConsoleColor.Cyan);
            foreach (var transport in newType)
            {
                Console.WriteLine($"{transport}");
            }
        }

        public static void GetNewTypeOffroadCarsExtension(Queue<List<Transport>> transportQueue)
        {
            var newType = transportQueue
                .SelectMany(transportList => transportList)
                .Where(transport => transport is OffroadCar)
                .Select(transport => new { Марка = transport.Brand, Выпуск = transport.Year });

            TransportWorkshops.WriteColorMessage("Метод расширения:", ConsoleColor.Cyan);
            foreach (var transport in newType)
            {
                Console.WriteLine($"{transport}");
            }
        }

        public static void JoinWithOwnersLINQ(Queue<List<Transport>> transportQueue)
        {
            var carOwners = new[]
            {
                new { Brand = "Toyota", OwnerName = "Дилдорбек", Experience = 5 },
                new { Brand = "Nissan", OwnerName = "Аббасали", Experience = 8 },
                new { Brand = "Mercedes", OwnerName = "Радиохед", Experience = 12 },
                new { Brand = "BMW", OwnerName = "Джагджит", Experience = 3 },
                new { Brand = "Volvo", OwnerName = "Насрулло", Experience = 15 }
            };

            Console.WriteLine();
            TransportWorkshops.WriteColorMessage("Доступные машины:", ConsoleColor.Green);

            foreach (var owner in carOwners)
            {
                Console.WriteLine($"Марка автомобиля: {owner.Brand}, Владелец: {owner.OwnerName}, Стаж: {owner.Experience} лет");
            }

            Console.WriteLine();
            TransportWorkshops.WriteColorMessage("LINQ метод:", ConsoleColor.Cyan);

            var joinResult = from transportList in transportQueue
                             from transport in transportList
                             join owner in carOwners on transport.Brand equals owner.Brand
                             select new
                             {
                                 Марка = transport.Brand,
                                 Выпуск = transport.Year,
                                 Стоимость = transport.Cost,
                                 Владелец = owner.OwnerName,
                                 Стаж = owner.Experience
                             };

            Console.WriteLine("Результат:");
            foreach (var item in joinResult)
            {
                Console.WriteLine($"Марка автомобиля: {item.Марка}, Год выпуска: {item.Выпуск}, Стоимость - {item.Стоимость}, Владелец: {item.Владелец}, Стаж вождения: {item.Стаж} лет");
            }
        }
    }
}
