using CarLibrary;
using System;
using System.Diagnostics;

namespace ExtensionAndLinqLab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var transportQueue = new Queue<List<Transport>>();

            var passengerCarsList = new List<Transport>();
            for (int i = 0; i < 5; i++)
            {
                var car = new PassengerCar();
                car.RandomCreate();
                passengerCarsList.Add(car);
                Console.WriteLine(car);
            }

            var trucksList = new List<Transport>();
            for (int i = 0; i < 5; i++)
            {
                var truck = new Truck();
                truck.RandomCreate();
                trucksList.Add(truck);
                Console.WriteLine(truck);
            }

            var offroadCarsList = new List<Transport>();
            for (int i = 0; i < 5; i++)
            {
                var offroad = new OffroadCar();
                offroad.RandomCreate();
                offroadCarsList.Add(offroad);
                Console.WriteLine(offroad);
            }

            var transportList = new List<Transport>();
            for (int i = 0; i < 4; i++)
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
                Console.WriteLine(transport);
            }

            transportQueue.Enqueue(passengerCarsList);
            transportQueue.Enqueue(trucksList);
            transportQueue.Enqueue(offroadCarsList);
            transportQueue.Enqueue(transportList);

            Console.WriteLine($"Завод по изготовлению автомобилей имеет {transportQueue.Count} цеха.");

        }
    }
}