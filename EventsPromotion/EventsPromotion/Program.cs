using System;
using System.Collections.Generic;
using System.Linq;

namespace EventsPromotion
{
    internal class Customer
    {
        public string Name { get; set; }
        public string City { get; set; }
    }
    internal class Event
    {
        public string Name { get; set; }
        public string City { get; set; }
        public int Price { get; set; }
    }

    internal class Program
    {
        private const int MaximumEventsCount = 5;

        static void Main(string[] args)
        {
            Console.WriteLine("=====================================================");
            Console.WriteLine("==================== Question 01 ====================");
            Console.WriteLine("=====================================================");
            try
            {
                Question01();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("=====================================================");
            Console.WriteLine("==================== Question 02 ====================");
            Console.WriteLine("=====================================================");
            try
            {
                Question02();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("=====================================================");
            Console.WriteLine("==================== Question 03 ====================");
            Console.WriteLine("=====================================================");
            try
            {
                Question03();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("=====================================================");
            Console.WriteLine("==================== Question 03 ====================");
            Console.WriteLine("=====================================================");
            try
            {
                Question05();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void Question01()
        {            
            var customerName = "John Smith";
            Customer customer = GetCustomerByName(customerName);
            AddCityEventsEmailsToCustomer(customer);
        }

        private static void Question02()
        {            
            var customerName = "Nathan";
            Customer customer = GetCustomerByName(customerName);
            AddCloserEventsEmailsToCustomer(customer);
        }

        private static void Question03()
        {            
            var customerName = "Nathan";
            Customer customer = GetCustomerByName(customerName);
            AddCloserEventsEmailsToCustomerWithRetries(customer);
        }

        private static void Question05()
        {            
            var customerName = "Nathan";
            Customer customer = GetCustomerByName(customerName);
            AddCloserEventsEmailsToCustomerFilteredByPrice(customer, 100);
        }

        private static void AddCityEventsEmailsToCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(typeof(Customer).Name);

            var customerEvents = GetCustomerEvents(customer);
            foreach (var customerEvent in customerEvents)
            {
                AddToEmail(customer, customerEvent);
            }
        }

        private static void AddCloserEventsEmailsToCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(typeof(Customer).Name);
            
            var customerEvents = GetCustomerCloserEvents(customer);

            foreach (var customerEvent in customerEvents)
            {
                AddToEmail(customer, customerEvent);
            }
        }

        private static void AddCloserEventsEmailsToCustomerWithRetries(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(typeof(Customer).Name);

            var customerEvents = GetCustomerCloserEventsWithRetry(customer);

            foreach (var customerEvent in customerEvents)
            {
                AddToEmail(customer, customerEvent);
            }
        }
        private static void AddCloserEventsEmailsToCustomerFilteredByPrice(Customer customer, int basePrice)
        {
            if (customer == null)
                throw new ArgumentNullException(typeof(Customer).Name);

            var customerEvents = GetCustomerCloserEventsWithFilter(customer, basePrice);

            foreach (var customerEvent in customerEvents)
            {
                AddToEmail(customer, customerEvent);
            }
        }

        private static Customer GetCustomerByName(string customerName)
        {
            return Customers.FirstOrDefault(d => d.Name == customerName);
        }

        private static IEnumerable<Event> GetCustomerCloserEvents(Customer customer)
        {
            var query = from d in Events
                        select new { Event = d, Distance = GetDistance(customer.City, d.City) };

            return query.OrderBy(d => d.Distance).Take(MaximumEventsCount).Select(d => d.Event);
        }
        private static IEnumerable<Event> GetCustomerCloserEventsWithFilter(Customer customer, int basePrice)
        {
            var query = from d in Events
                        where d.Price >= basePrice
                        select new { Event = d, Distance = GetDistance(customer.City, d.City) };

            return query.OrderBy(d => d.Distance).Take(MaximumEventsCount).Select(d => d.Event);
        }

        private static IEnumerable<Event> GetCustomerCloserEventsWithRetry(Customer customer)
        {
            var query = from d in Events
                        select new { Event = d, Distance = GetDistanceWithRetry(customer.City, d.City) };

            return query.OrderBy(d => d.Distance).Take(MaximumEventsCount).Select(d => d.Event);
        }

        private static IEnumerable<Event> GetCustomerEvents(Customer customer)
        {
            var query = from d in Events
                        where d.City == customer.City
                        select d;
            return query;
        }

        static int GetDistance(string fromCity, string toCity)
        {
            return AlphebiticalDistance(fromCity, toCity);
        }

        static int GetDistanceWithRetry(string fromCity, string toCity)
        {
            var tries = 5;
            var distance = 0;
            while (tries-- > 0)
                try
                {
                    distance = AlphebiticalDistance(fromCity, toCity);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error getting the cities distance: {ex.Message}.");
                }
            return distance;
        }

        private static int AlphebiticalDistance(string fromCity, string toCity)
        {
            var result = 0;
            int i;            
            for (i = 0; i < Math.Min(fromCity.Length, toCity.Length); i++)
            {
                result += Math.Abs(fromCity[i] - toCity[i]);
            }
            for (; i < Math.Max(fromCity.Length, toCity.Length); i++)
            {
                result += fromCity.Length > toCity.Length ? fromCity[i] : toCity[i];
            }
            return result;
        }

        static void AddToEmail(Customer c, Event e)
        {
            var distance = GetDistance(c.City, e.City);
            Console.Out.WriteLine($"{c.Name}: {e.Name} in {e.City}"
            + (distance > 0 ? $" ({distance} miles away)" : "")
            + $" for ${e.Price}");
        }

        static IEnumerable<Event> Events =>         
            new List<Event>
            {
                new Event{ Name = "Phantom of the Opera", City = "New York", Price = 100},
                new Event{ Name = "Metallica", City = "Los Angeles", Price = 120},
                new Event{ Name = "Metallica", City = "New York", Price = 120},
                new Event{ Name = "Metallica", City = "Boston", Price = 120},
                new Event{ Name = "LadyGaGa", City = "New York", Price = -1},
                new Event{ Name = "LadyGaGa", City = "Boston", Price = -1},
                new Event{ Name = "LadyGaGa", City = "Chicago", Price = -1},
                new Event{ Name = "LadyGaGa", City = "San Francisco", Price = -1},
                new Event{ Name = "LadyGaGa", City = "Washington", Price = -1}
            };
        static IEnumerable<Customer> Customers =>
            new List<Customer>
            {
                new Customer{ Name = "Nathan", City = "New York"},
                new Customer{ Name = "Bob", City = "Boston"},
                new Customer{ Name = "Cindy", City = "Chicago"},
                new Customer{ Name = "Lisa", City = "Los Angeles"}
            };
    }
}
