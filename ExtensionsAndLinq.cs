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
        public static double FindMaxPriceByYearLINQ(List<Transport> transportList, int minYear)
        {
            return (from transport in transportList
                    where transport.Year > minYear
                    select transport.Cost).Max();
        }

        public static double FindMaxPriceByYearExtension(List<Transport> transportList, int minYear)
        {
            return transportList
                .Where(transport => transport.Year > minYear)
                .Max(transport => transport.Cost);
        }
    }
}
