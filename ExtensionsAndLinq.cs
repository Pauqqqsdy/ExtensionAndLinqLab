using CarLibrary;
using DataStructuresLab.BinaryTree;
using DataStructuresLab.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace ExtensionAndLinqLab
{
    public class ExtensionsAndLinq
    {
        #region 1 часть
        public static double FindMaxCostTrucksByYearLINQ(Queue<List<Transport>> transportQueue, int minYear)
        {
            return (from transportList in transportQueue
                    from transport in transportList
                    where transport is Truck && transport.Year > minYear
                    select transport.Cost).Max();
        }

        public static double FindMaxCostTrucksByYearExtension(Queue<List<Transport>> transportQueue, int minYear)
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
            TransportWorkshops.WriteColorMessage("LINQ запрос: ", ConsoleColor.Cyan);
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

            TransportWorkshops.WriteColorMessage("LINQ запрос:", ConsoleColor.Cyan);
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
                new { Brand = "Mercedes-Benz", OwnerName = "Радиохед", Experience = 12 },
                new { Brand = "BMW", OwnerName = "Джагджит", Experience = 3 },
                new { Brand = "Volvo", OwnerName = "Насрулло", Experience = 15 }
            };

            TransportWorkshops.WriteColorMessage("Доступные машины:", ConsoleColor.Green);

            foreach (var owner in carOwners)
            {
                Console.WriteLine($"Марка автомобиля: {owner.Brand}, Владелец: {owner.OwnerName}, Стаж: {owner.Experience} лет");
            }

            TransportWorkshops.WriteColorMessage("Соединение:", ConsoleColor.Cyan);

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

            foreach (var item in joinResult)
            {
                Console.WriteLine($"Марка автомобиля: {item.Марка}, Год выпуска: {item.Выпуск}, " +
                    $"Стоимость - {item.Стоимость}, Владелец: {item.Владелец}, Стаж вождения: {item.Стаж} лет");
            }
        }

        public static void JoinWithOwnersExtension(Queue<List<Transport>> transportQueue)
        {
            var carOwners = new[]
            {
                new { Brand = "Toyota", OwnerName = "Дилдорбек", Experience = 5 },
                new { Brand = "Nissan", OwnerName = "Аббасали", Experience = 8 },
                new { Brand = "Mercedes-Benz", OwnerName = "Радиохед", Experience = 12 },
                new { Brand = "BMW", OwnerName = "Джагджит", Experience = 3 },
                new { Brand = "Volvo", OwnerName = "Насрулло", Experience = 15 }
            };

            TransportWorkshops.WriteColorMessage("Доступные машины:", ConsoleColor.Green);

            foreach (var owner in carOwners)
            {
                Console.WriteLine($"Марка автомобиля: {owner.Brand}, Владелец: {owner.OwnerName}, Стаж: {owner.Experience} лет");
            }

            TransportWorkshops.WriteColorMessage("Соединение:", ConsoleColor.Cyan);

            var joinResult = transportQueue
                .SelectMany(transportList => transportList)
                .Join(carOwners,
                      transport => transport.Brand,
                      owner => owner.Brand,
                      (transport, owner) => new
                      {
                          Марка = transport.Brand,
                          Выпуск = transport.Year,
                          Стоимость = transport.Cost,
                          Владелец = owner.OwnerName,
                          Стаж = owner.Experience
                      });

            foreach (var item in joinResult)
            {
                Console.WriteLine($"Марка автомобиля: {item.Марка}, Год выпуска: {item.Выпуск}, " +
                    $"Стоимость - {item.Стоимость}, Владелец: {item.Владелец}, Стаж вождения: {item.Стаж} лет");
            }
        }

        public static double FindMaxCostTrucksByYearCycle(Queue<List<Transport>> transportQueue, int minYear)
        {
            double maxCost = double.MinValue;
            bool found = false;

            foreach (var transportList in transportQueue)
            {
                for (int i = 0; i < transportList.Count; i++)
                {
                    var transport = transportList[i];
                    if (transport is Truck && transport.Year > minYear)
                    {
                        if (!found || transport.Cost > maxCost)
                        {
                            maxCost = transport.Cost;
                            found = true;
                        }
                    }
                }
            }

            if (!found)
                throw new InvalidOperationException($"Не найдено ни одного грузовика с годом выпуска больше {minYear}.");

            return maxCost;
        }

        public static void CompareFindMaxCostTrucksByYear(Queue<List<Transport>> transportQueue, int minYear)
        {
            const int iterations = 100;
            Stopwatch stopwatch = new Stopwatch();


            stopwatch.Restart();
            double resultLINQ = 0;
            for (int i = 0; i < iterations; i++)
            {
                resultLINQ = FindMaxCostTrucksByYearLINQ(transportQueue, minYear);
            }
            stopwatch.Stop();
            long linqTime = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            double resultExtension = 0;
            for (int i = 0; i < iterations; i++)
            {
                resultExtension = FindMaxCostTrucksByYearExtension(transportQueue, minYear);
            }
            stopwatch.Stop();
            long extensionTime = stopwatch.ElapsedTicks;

            stopwatch.Restart();
            double resultCycle = 0;
            for (int i = 0; i < iterations; i++)
            {
                resultCycle = FindMaxCostTrucksByYearCycle(transportQueue, minYear);
            }
            stopwatch.Stop();
            long cycleTime = stopwatch.ElapsedTicks;

            Console.WriteLine($"LINQ запрос: {linqTime} мс, максимальная стоимость - {resultLINQ}");
            Console.WriteLine($"Метод расширения: {extensionTime} мс, максимальная стоимость - {resultExtension}");
            Console.WriteLine($"Цикл: {cycleTime} мс, максимальная стоимость - {resultCycle}");
        }
        #endregion

        #region 2 часть
        public static double AverageWhereLinq(Tree<int> tree, Func<int, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException();

            if (predicate == null) throw new ArgumentNullException();

            return (from element in tree
                    where predicate(element)
                    select element).Average();
        }

        public static double AverageWhereExtension(Tree<int> tree, Func<int, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException();

            if (predicate == null) throw new ArgumentNullException();

            return tree.Where(predicate).Average();
        }

        public static int CountWhereLinq<T>(Tree<T> tree, Func<T, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException();

            if (predicate == null) throw new ArgumentNullException();

            return (from element in tree
                    where predicate(element)
                    select element).Count();
        }

        public static int CountWhereExtension<T>(Tree<T> tree, Func<T, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException();

            if (predicate == null) throw new ArgumentNullException();

            return tree.Where(predicate).Count();
        }

        public static IEnumerable<IGrouping<TKey, T>> GroupByLinq<T, TKey>(Tree<T> tree, Func<T, TKey> key)
        {
            if (tree == null) throw new ArgumentNullException();

            if (key == null) throw new ArgumentNullException();

            return from element in tree
                   group element by key(element);
        }

        public static IEnumerable<IGrouping<TKey, T>> GroupByExtension<T, TKey>(Tree<T> tree, Func<T, TKey> key)
        {
            if (tree == null) throw new ArgumentNullException();

            if (key == null) throw new ArgumentNullException();

            return tree.GroupBy(key);
        }
        #endregion
    }
}
