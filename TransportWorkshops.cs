using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CarLibrary;

namespace ExtensionAndLinqLab
{
    public class TransportWorkshops
    {
        public static List<Transport> CreatePassengerCarsList(int count)
        {
            var passengerCarsList = new List<Transport>();
            for (int i = 0; i < count; i++)
            {
                var car = new PassengerCar();
                car.RandomCreate();
                passengerCarsList.Add(car);
            }
            return passengerCarsList;
        }

        public static List<Transport> CreateOffroadCarsList(int count)
        {
            var offroadCarsList = new List<Transport>();
            for (int i = 0; i < count; i++)
            {
                var car = new OffroadCar();
                car.RandomCreate();
                offroadCarsList.Add(car);
            }
            return offroadCarsList;
        }

        public static List<Transport> CreateTrucksList(int count)
        {
            var trucksList = new List<Transport>();
            for (int i = 0; i < count; i++)
            {
                var car = new Truck();
                car.RandomCreate();
                trucksList.Add(car);
            }
            return trucksList;
        }

        public static List<Transport> CreateTransportList(int count)
        {
            var transportList = new List<Transport>();
            for (int i = 0; i < count; i++)
            {
                Transport transport;
                switch (i % 4)
                {
                    case 0:
                        transport = new Transport();
                        break;
                    case 1:
                        transport = new PassengerCar();
                        break;
                    case 2:
                        transport = new Truck();
                        break;
                    default:
                        transport = new OffroadCar();
                        break;
                }
                transport.RandomCreate();
                transportList.Add(transport);
            }
            return transportList;
        }

        public static Queue<List<Transport>> CreateTransportQueue(int countPerType)
        {
            var transportQueue = new Queue<List<Transport>>();

            transportQueue.Enqueue(CreatePassengerCarsList(countPerType));
            transportQueue.Enqueue(CreateTrucksList(countPerType));
            transportQueue.Enqueue(CreateOffroadCarsList(countPerType));
            transportQueue.Enqueue(CreateTransportList(countPerType));

            return transportQueue;
        }

        public static Queue<List<Transport>> CreateTransportQueueFromLists(params List<Transport>[] transportLists)
        {
            var transportQueue = new Queue<List<Transport>>();

            foreach (var list in transportLists)
            {
                if (list != null && list.Count > 0)
                {
                    transportQueue.Enqueue(list);
                }
            }

            return transportQueue;
        }

        public static void PrintTransportList(List<Transport> transportList, string transportType)
        {
            Console.WriteLine(transportType);
            foreach (var transport in transportList)
            {
                Console.WriteLine(transport);
            }
        }

        public static void PrintTransportQueue(Queue<List<Transport>> transportQueue)
        {
            if (transportQueue == null || transportQueue.Count == 0)
            {
                Console.WriteLine("Завод пуст.");
                return;
            }

            Console.WriteLine($"Общее количество цехов в заводе: {transportQueue.Count}");

            int workshopNumber = 1;
            foreach (var workshop in transportQueue)
            {
                string shopType = GetWorkshopName(workshop);

                Console.WriteLine($"Цех {workshopNumber} ({shopType}): {workshop.Count} единиц транспорта");

                if (workshop.Count > 0)
                {
                    for (int i = 0; i < workshop.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {workshop[i]}");
                    }
                }
                else
                {
                    Console.WriteLine("Цех пуст");
                }

                Console.WriteLine();
                workshopNumber++;
            }
        }

        public static string GetWorkshopName(List<Transport> workshop)
        {
            if (workshop == null || workshop.Count == 0)
                return "Пусто";

            string typeName = workshop[0].GetType().Name;

            return typeName switch
            {
                "PassengerCar" => "Легковые автомобили",
                "Truck" => "Грузовики",
                "OffroadCar" => "Внедорожники",
                "Transport" => "Все виды транспорта",
                _ => "Смешанный"
            };
        }

        public static void WriteColorMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
