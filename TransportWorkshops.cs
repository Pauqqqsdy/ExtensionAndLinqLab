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
        /// <summary>
        /// Создает список легковых автомобилей
        /// </summary>
        /// <param name="count">Количество автомобилей для создания</param>
        /// <returns>Список легковых автомобилей</returns>
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

        /// <summary>
        /// Создает список внедорожников
        /// </summary>
        /// <param name="count">Количество внедорожников для создания</param>
        /// <returns>Список внедорожников</returns>
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

        /// <summary>
        /// Создает список грузовиков
        /// </summary>
        /// <param name="count">Количество грузовиков для создания</param>
        /// <returns>Список грузовиков</returns>
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

        /// <summary>
        /// Создает список различных типов транспорта
        /// </summary>
        /// <param name="count">Количество транспортных средств для создания</param>
        /// <returns>Список различных типов транспорта</returns>
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

        /// <summary>
        /// Создает очередь списков транспорта с указанным количеством каждого типа
        /// </summary>
        /// <param name="countPerType">Количество транспортных средств каждого типа</param>
        /// <returns>Очередь списков транспорта</returns>
        public static Queue<List<Transport>> CreateTransportQueue(int countPerType)
        {
            var transportQueue = new Queue<List<Transport>>();

            // Добавление в очередь списков разных типов транспорта
            transportQueue.Enqueue(CreatePassengerCarsList(countPerType));
            transportQueue.Enqueue(CreateTrucksList(countPerType));
            transportQueue.Enqueue(CreateOffroadCarsList(countPerType));
            transportQueue.Enqueue(CreateTransportList(countPerType));

            return transportQueue;
        }

        /// <summary>
        /// Создает очередь списков транспорта из существующих списков
        /// </summary>
        /// <param name="transportLists">Массив списков транспорта</param>
        /// <returns>Очередь списков транспорта</returns>
        public static Queue<List<Transport>> CreateTransportQueueFromLists(params List<Transport>[] transportLists)
        {
            var transportQueue = new Queue<List<Transport>>();

            // Добавление в очередь списков, которые не пусты
            foreach (var list in transportLists)
            {
                if (list != null && list.Count > 0)
                {
                    transportQueue.Enqueue(list);
                }
            }

            return transportQueue;
        }

        /// <summary>
        /// Выводит список транспорта с указанным заголовком
        /// </summary>
        /// <param name="transportList">Список транспорта для вывода</param>
        /// <param name="transportType">Заголовок для вывода</param>
        public static void PrintTransportList(List<Transport> transportList, string transportType)
        {
            Console.WriteLine(transportType);

            foreach (var transport in transportList)
            {
                Console.WriteLine(transport);
            }
        }

        /// <summary>
        /// Выводит информацию о всех цехах в очереди
        /// </summary>
        /// <param name="transportQueue">Очередь списков транспорта</param>
        public static void PrintTransportQueue(Queue<List<Transport>> transportQueue)
        {
            if (transportQueue == null || transportQueue.Count == 0)
            {
                WriteColorMessage("Завод пуст.", ConsoleColor.Red);
                return;
            }

            WriteColorMessage($"Общее количество цехов в заводе: {transportQueue.Count}", ConsoleColor.Green);
            Console.WriteLine();

            int workshopNumber = 1;

            // Вывод информации о каждом цехе
            foreach (var workshop in transportQueue)
            {
                string shopType = GetWorkshopName(workshop);

                WriteColorMessage($"Цех {workshopNumber} ({shopType}): {workshop.Count} единиц транспорта", ConsoleColor.Green);

                if (workshop.Count > 0)
                {
                    // Вывод информации о каждом транспортном средстве в цехе
                    for (int i = 0; i < workshop.Count; i++)
                    {
                        Console.WriteLine($"{i + 1}. {workshop[i]}");
                    }
                }
                else
                {
                    WriteColorMessage("Цех пуст", ConsoleColor.Red);
                }

                Console.WriteLine();
                workshopNumber++;
            }
        }

        /// <summary>
        /// Определяет название цеха на основе типа транспорта в нем
        /// </summary>
        /// <param name="workshop">Список транспорта в цехе</param>
        /// <returns>Название цеха</returns>
        public static string GetWorkshopName(List<Transport> workshop)
        {
            if (workshop == null || workshop.Count == 0)
                return "Пусто";

            string typeName = workshop[0].GetType().Name;

            // Функция вернёт название цеха в зависимости от типа транспорта
            return typeName switch
            {
                "PassengerCar" => "Легковые автомобили",
                "Truck" => "Грузовики",
                "OffroadCar" => "Внедорожники",
                "Transport" => "Все виды транспорта",
                _ => "Смешанный"
            };
        }

        /// <summary>
        /// Выводит сообщение в консоль с указанным цветом
        /// </summary>
        /// <param name="message">Сообщение для вывода</param>
        /// <param name="color">Цвет текста</param>
        public static void WriteColorMessage(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
