using CarLibrary;
using System;
using System.Diagnostics;

namespace ExtensionAndLinqLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //var passengerCarsList = TransportWorkshops.CreatePassengerCarsList(5);
            //TransportWorkshops.PrintTransportList(passengerCarsList, "Легковые автомобили:");

            //var offroadCarsList = TransportWorkshops.CreateOffroadCarsList(5);
            //TransportWorkshops.PrintTransportList(offroadCarsList, "Внедорожники:");

            //var trucksList = TransportWorkshops.CreateTrucksList(5);
            //TransportWorkshops.PrintTransportList(trucksList, "Грузовые автомобили");

            //var transportList = TransportWorkshops.CreateTransportList(10);
            //TransportWorkshops.PrintTransportList(transportList, "Список разных видов транспорта");

            //var transportQueue = TransportWorkshops.CreateTransportQueueFromLists(
            //    passengerCarsList,
            //    trucksList,
            //    offroadCarsList,
            //    transportList
            //);

            var transportQueue = TransportWorkshops.CreateTransportQueue(10);

            TransportWorkshops.PrintTransportQueue(transportQueue);
        }
    }
}