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
        public static IEnumerable<OffroadCar> FindExpensiveOffroadCarsLINQ(IEnumerable<Transport> transports, double minCost)
        {
            return from transport in transports
                   where transport is OffroadCar && transport.Cost > minCost
                   select (OffroadCar)transport;
        }
    }
}
