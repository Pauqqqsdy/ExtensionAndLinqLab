using CarLibrary;
using System;
using System.Diagnostics;

namespace ExtensionAndLinqLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //TransportWorkshops.WriteColorMessage("Цех 1", ConsoleColor.Red);
            //var passengerCarsList = TransportWorkshops.CreatePassengerCarsList(5);
            //TransportWorkshops.PrintTransportList(passengerCarsList, "Легковые автомобили:");
            //Console.WriteLine();

            //var offroadCarsList = TransportWorkshops.CreateOffroadCarsList(5);
            //TransportWorkshops.PrintTransportList(offroadCarsList, "Внедорожники:");
            //Console.WriteLine();

            //var trucksList = TransportWorkshops.CreateTrucksList(5);
            //TransportWorkshops.PrintTransportList(trucksList, "Грузовые автомобили:");
            //Console.WriteLine();

            //var transportList = TransportWorkshops.CreateTransportList(10);
            //TransportWorkshops.PrintTransportList(transportList, "Список разных видов транспорта:");
            //Console.WriteLine();

            //var transportQueueFromLists = TransportWorkshops.CreateTransportQueueFromLists(
            //    passengerCarsList,
            //    trucksList,
            //    offroadCarsList,
            //    transportList
            //);

            TransportWorkshops.WriteColorMessage("Завод, Бинго!", ConsoleColor.Magenta);
            var transportQueue = TransportWorkshops.CreateTransportQueue(10);
            TransportWorkshops.PrintTransportQueue(transportQueue);

            #region Выборка + агрегирование
            //TransportWorkshops.WriteColorMessage("Поиск в цехе самого дорогого грузовика, который выпустили позже 2010 года:", ConsoleColor.Yellow);
            //double maxCostLINQ = ExtensionsAndLinq.FindMaxCostTrucksByYearLINQ(transportQueue, 2010);
            //TransportWorkshops.WriteColorMessage($"LINQ запросом: {maxCostLINQ}", ConsoleColor.Cyan);

            //double maxCostExtension = ExtensionsAndLinq.FindMaxCostTrucksByYearExtension(transportQueue, 2010);
            //TransportWorkshops.WriteColorMessage($"Методом расширения: {maxCostExtension}", ConsoleColor.Cyan);
            #endregion

            #region Объединение множеств
            //TransportWorkshops.WriteColorMessage("\nОбъединение грузовиков и внедорожников из всех цехов", ConsoleColor.Yellow);
            //var unionTrucksAndOffroadLINQ = ExtensionsAndLinq.UnionTrucksAndOffroadCarsLINQ(transportQueue);
            //TransportWorkshops.WriteColorMessage($"LINQ запросом: {unionTrucksAndOffroadLINQ.Count()} единиц транспорта", ConsoleColor.Cyan);

            //var unionTrucksAndOffroadExtension = ExtensionsAndLinq.UnionTrucksAndOffroadCarsExtension(transportQueue);
            //TransportWorkshops.WriteColorMessage($"Методом расширения: {unionTrucksAndOffroadExtension.Count()} единиц транспорта", ConsoleColor.Cyan);
            #endregion

            #region Группировка
            //Console.WriteLine();
            //TransportWorkshops.WriteColorMessage("Группировка легковых автомобилей по количеству сидений", ConsoleColor.Yellow);
            //var passengerCarsGroupBySeastLINQ = ExtensionsAndLinq.GroupingPassengerCarsBySeatsLINQ(transportQueue);
            //Console.WriteLine();
            //var passengerCarsGroupBySeastExtension = ExtensionsAndLinq.GroupingPassengerCarsBySeatsExtension(transportQueue);
            #endregion

            #region Получение нового типа
            //ExtensionsAndLinq.GetNewTypeOffroadCarsLINQ(transportQueue);
            //Console.WriteLine();
            //ExtensionsAndLinq.GetNewTypeOffroadCarsExtension(transportQueue);
            #endregion

            #region Соединение
            //TransportWorkshops.WriteColorMessage("LINQ запрос:", ConsoleColor.Yellow);
            //ExtensionsAndLinq.JoinWithOwnersLINQ(transportQueue);

            //Console.WriteLine();

            //TransportWorkshops.WriteColorMessage("Метод расширения:", ConsoleColor.Yellow);
            //ExtensionsAndLinq.JoinWithOwnersExtension(transportQueue);
            #endregion

            #region Сравнение тиков для цикла for, LINQ запроса и метода расширения(поиск самых дорогих грузовиков)
            ExtensionsAndLinq.CompareFindMaxCostTrucksByYear(transportQueue, 2010);
            #endregion
        }
    }
}