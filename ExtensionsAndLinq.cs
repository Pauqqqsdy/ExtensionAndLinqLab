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
        /// <summary>
        /// Находит максимальную стоимость грузовиков, выпущенных после указанного года, используя LINQ запрос
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        /// <param name="minYear">Минимальный год выпуска</param>
        /// <returns>Максимальная стоимость найденных грузовиков</returns>
        public static double FindMaxCostTrucksByYearLINQ(Queue<List<Transport>> transportQueue, int minYear)
        {
            return (from transportList in transportQueue
                    from transport in transportList
                    where transport is Truck && transport.Year > minYear
                    select transport.Cost).Max();
        }

        /// <summary>
        /// Находит максимальную стоимость грузовиков, выпущенных после указанного года, используя методы расширения
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        /// <param name="minYear">Минимальный год выпуска</param>
        /// <returns>Максимальная стоимость найденных грузовиков</returns>
        public static double FindMaxCostTrucksByYearExtension(Queue<List<Transport>> transportQueue, int minYear)
        {
            return transportQueue
                .SelectMany(transportList => transportList)
                .Where(transport => transport is Truck && ((Truck)transport).Year > minYear)
                .Max(transport => transport.Cost);
        }

        /// <summary>
        /// Объединяет списки грузовиков и внедорожников из всех цехов, используя LINQ запрос
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        /// <returns>Объединенная коллекция грузовиков и внедорожников</returns>
        public static IEnumerable<Transport> UnionTrucksAndOffroadCarsLINQ(Queue<List<Transport>> transportQueue)
        {
            // Список всех грузовиков
            var trucks = (from transportList in transportQueue
                          from transport in transportList
                          where transport is Truck
                          select transport).ToList();

            // Список всех внедорожников
            var offroadCars = (from transportList in transportQueue
                               from transport in transportList
                               where transport is OffroadCar
                               select transport).ToList();

            // Объединение списков
            return trucks.Union(offroadCars);
        }

        /// <summary>
        /// Объединяет списки грузовиков и внедорожников из всех цехов, используя методы расширения
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        /// <returns>Объединенная коллекция грузовиков и внедорожников</returns>
        public static IEnumerable<Transport> UnionTrucksAndOffroadCarsExtension(Queue<List<Transport>> transportQueue)
        {
            // Список всех грузовиков
            var trucks = transportQueue
                .SelectMany(transportList => transportList)
                .Where(transport => transport is Truck);

            // Список всех внедорожников
            var offroadCars = transportQueue
                .SelectMany(transportList => transportList)
                .Where(transport => transport is OffroadCar);

            // Объединение списков
            return trucks.Union(offroadCars);
        }

        /// <summary>
        /// Группирует легковые автомобили по количеству сидений, используя LINQ запрос
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        /// <returns>Сгруппированная коллекция легковых автомобилей</returns>
        public static IEnumerable<Transport> GroupingPassengerCarsBySeatsLINQ(Queue<List<Transport>> transportQueue)
        {
            TransportWorkshops.WriteColorMessage("LINQ запрос: ", ConsoleColor.Cyan);

            // Группа легковых автомобилей по количеству сидений
            var grouping = from transportList in transportQueue
                           from transport in transportList
                           where transport is PassengerCar
                           group transport by ((PassengerCar)transport).SeatsNumber;

            // Информация о каждой группе
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

        /// <summary>
        /// Группирует легковые автомобили по количеству сидений, используя методы расширения
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        /// <returns>Сгруппированная коллекция легковых автомобилей</returns>
        public static IEnumerable<Transport> GroupingPassengerCarsBySeatsExtension(Queue<List<Transport>> transportQueue)
        {
            TransportWorkshops.WriteColorMessage("Метод расширения: ", ConsoleColor.Cyan);

            // Группа легковых автомобилей по количеству сидений
            var grouping = transportQueue
                .SelectMany(transportList => transportList)
                .Where(transport => transport is PassengerCar)
                .GroupBy(transport => ((PassengerCar)transport).SeatsNumber);

            // Информация о каждой группе
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

        /// <summary>
        /// Создает новый тип данных для внедорожников, используя LINQ запрос
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        public static void GetNewTypeOffroadCarsLINQ(Queue<List<Transport>> transportQueue)
        {
            // Анонимный тип с маркой и годом выпуска внедорожников
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

        /// <summary>
        /// Создает новый тип данных для внедорожников, используя методы расширения
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        public static void GetNewTypeOffroadCarsExtension(Queue<List<Transport>> transportQueue)
        {
            // Анонимный тип с маркой и годом выпуска внедорожников
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

        /// <summary>
        /// Соединяет информацию о транспорте с информацией о владельцах, используя LINQ запрос
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        public static void JoinWithOwnersLINQ(Queue<List<Transport>> transportQueue)
        {
            // Массив владельцев автомобилей
            var carOwners = new[]
            {
                new { Brand = "Toyota", OwnerName = "Дилдорбек", Experience = 5 },
                new { Brand = "Nissan", OwnerName = "Аббасали", Experience = 8 },
                new { Brand = "Mercedes-Benz", OwnerName = "Радиохед", Experience = 12 },
                new { Brand = "BMW", OwnerName = "Джагджит", Experience = 3 },
                new { Brand = "Volvo", OwnerName = "Насрулло", Experience = 15 }
            };

            TransportWorkshops.WriteColorMessage("Доступные машины:", ConsoleColor.Green);

            // Информация о владельцах
            foreach (var owner in carOwners)
            {
                Console.WriteLine($"Марка автомобиля: {owner.Brand}, Владелец: {owner.OwnerName}, Стаж: {owner.Experience} лет");
            }

            TransportWorkshops.WriteColorMessage("Соединение:", ConsoleColor.Cyan);

            // Соединение информации о транспорте с информацией о владельцах
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

            // Результат соединения
            foreach (var item in joinResult)
            {
                Console.WriteLine($"Марка автомобиля: {item.Марка}, Год выпуска: {item.Выпуск}, " +
                    $"Стоимость - {item.Стоимость}, Владелец: {item.Владелец}, Стаж вождения: {item.Стаж} лет");
            }
        }

        /// <summary>
        /// Соединяет информацию о транспорте с информацией о владельцах, используя методы расширения
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        public static void JoinWithOwnersExtension(Queue<List<Transport>> transportQueue)
        {
            // Массив владельцев автомобилей
            var carOwners = new[]
            {
                new { Brand = "Toyota", OwnerName = "Дилдорбек", Experience = 5 },
                new { Brand = "Nissan", OwnerName = "Аббасали", Experience = 8 },
                new { Brand = "Mercedes-Benz", OwnerName = "Радиохед", Experience = 12 },
                new { Brand = "BMW", OwnerName = "Джагджит", Experience = 3 },
                new { Brand = "Volvo", OwnerName = "Насрулло", Experience = 15 }
            };

            TransportWorkshops.WriteColorMessage("Доступные машины:", ConsoleColor.Green);

            // Информация о владельцах
            foreach (var owner in carOwners)
            {
                Console.WriteLine($"Марка автомобиля: {owner.Brand}, Владелец: {owner.OwnerName}, Стаж: {owner.Experience} лет");
            }

            TransportWorkshops.WriteColorMessage("Соединение:", ConsoleColor.Cyan);

            // Соединение информации о транспорте с информацией о владельцах
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

            // Результат соединения
            foreach (var item in joinResult)
            {
                Console.WriteLine($"Марка автомобиля: {item.Марка}, Год выпуска: {item.Выпуск}, " +
                    $"Стоимость - {item.Стоимость}, Владелец: {item.Владелец}, Стаж вождения: {item.Стаж} лет");
            }
        }

        /// <summary>
        /// Находит максимальную стоимость грузовиков, выпущенных после указанного года, используя цикл
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        /// <param name="minYear">Минимальный год выпуска</param>
        /// <returns>Максимальная стоимость найденных грузовиков</returns>
        /// <exception cref="InvalidOperationException">Выбрасывается, если не найдено ни одного подходящего грузовика</exception>
        public static double FindMaxCostTrucksByYearCycle(Queue<List<Transport>> transportQueue, int minYear)
        {
            double maxCost = double.MinValue;
            bool found = false;

            // Все списки транспорта в очереди
            foreach (var transportList in transportQueue)
            {
                for (int i = 0; i < transportList.Count; i++)
                {
                    var transport = transportList[i];

                    // Проверка, является ли транспорт грузовиком и соответствует ли году выпуска
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

        /// <summary>
        /// Сравнивает производительность различных методов поиска максимальной стоимости грузовиков
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        /// <param name="minYear">Минимальный год выпуска</param>
        public static void CompareFindMaxCostTrucksByYear(Queue<List<Transport>> transportQueue, int minYear)
        {
            const int iterations = 100;
            Stopwatch stopwatch = new Stopwatch();

            // Время выполнения LINQ запроса
            stopwatch.Restart();
            double resultLINQ = 0;
            for (int i = 0; i < iterations; i++)
            {
                resultLINQ = FindMaxCostTrucksByYearLINQ(transportQueue, minYear);
            }
            stopwatch.Stop();
            long linqTime = stopwatch.ElapsedTicks;

            // Время выполнения метода расширения
            stopwatch.Restart();
            double resultExtension = 0;
            for (int i = 0; i < iterations; i++)
            {
                resultExtension = FindMaxCostTrucksByYearExtension(transportQueue, minYear);
            }
            stopwatch.Stop();
            long extensionTime = stopwatch.ElapsedTicks;

            // Время выполнения цикла
            stopwatch.Restart();
            double resultCycle = 0;
            for (int i = 0; i < iterations; i++)
            {
                resultCycle = FindMaxCostTrucksByYearCycle(transportQueue, minYear);
            }
            stopwatch.Stop();
            long cycleTime = stopwatch.ElapsedTicks;

            // Результаты сравнения
            Console.WriteLine($"LINQ запрос: {linqTime} мс, максимальная стоимость - {resultLINQ}");
            Console.WriteLine($"Метод расширения: {extensionTime} мс, максимальная стоимость - {resultExtension}");
            Console.WriteLine($"Цикл: {cycleTime} мс, максимальная стоимость - {resultCycle}");
        }
        #endregion

        #region 2 часть
        /// <summary>
        /// Вычисляет среднее значение элементов дерева, удовлетворяющих условию, используя LINQ запрос
        /// </summary>
        /// <param name="tree">Дерево целых чисел</param>
        /// <param name="predicate">Условие для фильтрации элементов</param>
        /// <returns>Среднее значение отфильтрованных элементов</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если дерево или предикат равны null</exception>
        public static double AverageWhereLinq(Tree<int> tree, Func<int, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException();
            if (predicate == null) throw new ArgumentNullException();

            return (from element in tree
                    where predicate(element)
                    select element).Average();
        }

        /// <summary>
        /// Вычисляет среднее значение элементов дерева, удовлетворяющих условию, используя методы расширения
        /// </summary>
        /// <param name="tree">Дерево целых чисел</param>
        /// <param name="predicate">Условие для фильтрации элементов</param>
        /// <returns>Среднее значение отфильтрованных элементов</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если дерево или предикат равны null</exception>
        public static double AverageWhereExtension(Tree<int> tree, Func<int, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException();
            if (predicate == null) throw new ArgumentNullException();

            return tree.Where(predicate).Average();
        }

        /// <summary>
        /// Подсчитывает количество элементов дерева, удовлетворяющих условию, используя LINQ запрос
        /// </summary>
        /// <typeparam name="T">Тип элементов дерева</typeparam>
        /// <param name="tree">Дерево элементов</param>
        /// <param name="predicate">Условие для фильтрации элементов</param>
        /// <returns>Количество элементов, удовлетворяющих условию</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если дерево или предикат равны null</exception>
        public static int CountWhereLinq<T>(Tree<T> tree, Func<T, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException();
            if (predicate == null) throw new ArgumentNullException();

            return (from element in tree
                    where predicate(element)
                    select element).Count();
        }

        /// <summary>
        /// Подсчитывает количество элементов дерева, удовлетворяющих условию, используя методы расширения
        /// </summary>
        /// <typeparam name="T">Тип элементов дерева</typeparam>
        /// <param name="tree">Дерево элементов</param>
        /// <param name="predicate">Условие для фильтрации элементов</param>
        /// <returns>Количество элементов, удовлетворяющих условию</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если дерево или предикат равны null</exception>
        public static int CountWhereExtension<T>(Tree<T> tree, Func<T, bool> predicate)
        {
            if (tree == null) throw new ArgumentNullException();
            if (predicate == null) throw new ArgumentNullException();

            return tree.Where(predicate).Count();
        }

        /// <summary>
        /// Группирует элементы дерева по ключу, используя LINQ запрос
        /// </summary>
        /// <typeparam name="T">Тип элементов дерева</typeparam>
        /// <typeparam name="TKey">Тип ключа группировки</typeparam>
        /// <param name="tree">Дерево элементов</param>
        /// <param name="key">Функция, возвращающая ключ группировки</param>
        /// <returns>Группированная коллекция элементов</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если дерево или функция ключа равны null</exception>
        public static IEnumerable<IGrouping<TKey, T>> GroupByLinq<T, TKey>(Tree<T> tree, Func<T, TKey> key)
        {
            if (tree == null) throw new ArgumentNullException();
            if (key == null) throw new ArgumentNullException();

            return from element in tree
                   group element by key(element);
        }

        /// <summary>
        /// Группирует элементы дерева по ключу, используя методы расширения
        /// </summary>
        /// <typeparam name="T">Тип элементов дерева</typeparam>
        /// <typeparam name="TKey">Тип ключа группировки</typeparam>
        /// <param name="tree">Дерево элементов</param>
        /// <param name="key">Функция, возвращающая ключ группировки</param>
        /// <returns>Группированная коллекция элементов</returns>
        /// <exception cref="ArgumentNullException">Выбрасывается, если дерево или функция ключа равны null</exception>
        public static IEnumerable<IGrouping<TKey, T>> GroupByExtension<T, TKey>(Tree<T> tree, Func<T, TKey> key)
        {
            if (tree == null) throw new ArgumentNullException();
            if (key == null) throw new ArgumentNullException();

            return tree.GroupBy(key);
        }
        #endregion
    }
}
