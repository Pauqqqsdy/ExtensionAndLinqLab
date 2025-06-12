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

            var transportQueue = TransportWorkshops.CreateTransportQueue(10);
            TransportWorkshops.WriteColorMessage("Цех", ConsoleColor.Red);
            TransportWorkshops.PrintTransportQueue(transportQueue);

        }
    }
}