using CarLibrary;
using DataStructuresLab.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
