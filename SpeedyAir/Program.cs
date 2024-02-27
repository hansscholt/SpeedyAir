//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Emit;
using System.Text.Json;

namespace SpeedyAir
{
    class Flight
    {
        public int iDay { get; set; }
        public int iFlightNumber { get; set; }
        public string? sDeparture { get; set; }
        public string? sDestination { get; set; }
        public int iBoxCount { get; set; }
        public Flight() { iBoxCount = 0; }
    }

    class Order 
    {
        public string? destination { get; set; }
        public bool bScheduled { get; set; }
        public Order() { bScheduled = false; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Flight> flightList = new List<Flight>()
            {
                new Flight {iDay=0, iFlightNumber=0, sDeparture = "YUL", sDestination = "YYZ" },
                new Flight {iDay=0, iFlightNumber=1, sDeparture = "YUL", sDestination = "YYC" },
                new Flight {iDay=0, iFlightNumber=2, sDeparture = "YUL", sDestination = "YVR" },

                new Flight {iDay=1, iFlightNumber=3, sDeparture = "YUL", sDestination = "YYZ" },
                new Flight {iDay=1, iFlightNumber=4, sDeparture = "YUL", sDestination = "YYC" },
                new Flight {iDay=1, iFlightNumber=5, sDeparture = "YUL", sDestination = "YVR" },
            };

            foreach (var flightItem in flightList)
            {
                Console.WriteLine(string.Format("Flight: {0}, departure: {1}, arrival: {2}, day: {3}",
                    (flightItem.iFlightNumber + 1), flightItem.sDeparture, flightItem.sDestination, (flightItem.iDay + 1)));
            }
            //with generic flight numbers
            //for (int i = 0; i < flightList.Count; i++)
            //{
            //    Flight flightItem = flightList[i];
            //    Console.WriteLine(string.Format("Flight: {0}, departure: {1}, arrival: {2}, day: {3}",
            //        (i + 1), flightItem.sOrigin, flightItem.sDestiny, (flightItem.iDay + 1)));
            //}

            Console.WriteLine("=====");
            string sPath = "coding-assigment-orders.json";//paste the file next to the executable
            string text = File.ReadAllText(sPath);
            Dictionary<string, Order>? jsonObject = JsonSerializer.Deserialize<Dictionary<string, Order>>(text);

            //order: order-001, flightNumber: 1, departure: <departure_city>, arrival: <arrival_city>, day: x
            //order: order-099, flightNumber: 1, departure: <departure_city>, arrival: <arrival_city>, day: x 
            //order: order-X, flightNumber: not scheduled
            foreach (KeyValuePair<string,Order> order in jsonObject)
            {
                string sOrderName = order.Key;
                string sOrderDest = order.Value.destination;
                
                for (int i = 0; i < flightList.Count; i++)
                {
                    if (flightList[i].iBoxCount >= 20)//each plane can carry 20 boxes
                        continue;                    
                    if (sOrderDest == flightList[i].sDestination)
                    {
                        flightList[i].iBoxCount++;
                        order.Value.bScheduled = true;
                        Console.WriteLine(string.Format("order: {0}, flightNumber: {1}, departure: {2}, arrival: {3}, day: {4}",
                            sOrderName, (flightList[i].iFlightNumber + 1), flightList[i].sDeparture, flightList[i].sDestination, (flightList[i].iDay + 1)));
                        break;
                    }
                }
                if (!order.Value.bScheduled)
                    Console.WriteLine(string.Format("order: {0}, flightNumber: not scheduled", sOrderName));
            }
           

            Console.ReadLine();
        }
    }
}