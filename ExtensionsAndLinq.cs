using CarLibrary;
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
    }
}
