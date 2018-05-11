using OrderPrice.Services.BusinessExceptions;
using OrderPrice.Services.DomainLayer;
using OrderPrice.Services.ServiceLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace OrderPrice.UIConsole
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            var command = args.Length > 0 ? args[0] : string.Empty;

            Console.WriteLine(string.Empty);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to farfetch command-line store.");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("See list of commands available below:");
            Console.WriteLine(" - CalculateOrder [e.g. CalculateOrder Catalog.txt <ProductId Quantity> <ProductId Quantity> ..]");
            Console.WriteLine(" - Close");
            Console.WriteLine(string.Empty);

            Console.ForegroundColor = ConsoleColor.White;

            while (true)
            {
                try
                {
                    switch (command.ToLower())
                    {
                        case "calculateorder" :
                            ProcessOrder(args);
                            command = string.Empty;
                            break;
                        case "close":
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine(string.Empty);

                            args = GetUserInput();
                            command = args[0];
                            break;
                    }
                }
                catch (ProductOutOfStockException ex)
                {
                    ShowError(ex.Message);
                    Environment.Exit(1);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                    command = string.Empty;
                }
            }
        }

        private static string[] GetUserInput()
        {
            return Console.ReadLine()?.Split(' ');
        }

        private static void ProcessOrder(string[] args)
        {
            var catalog = args[1];

            IOrderService orderService = new OrderService(new ProductService(catalog));

            var orderItems = new Dictionary<string, int>();

            for (int i = 2; i < args.Length; i += 2)
            {
                orderItems.Add(args[i], Convert.ToInt32(args[i + 1]));
            }

            var domainOrder = orderService.PlaceOrder(orderItems);
            var orderTotalVat = orderService.CalculateOrder(domainOrder);

            Console.WriteLine($"Total: {orderTotalVat:C}");
        }
        
        private static void ShowError(string errorMessage)
        {
            // Keeps current fore-color in memory to restore it later
            var currentForeColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(errorMessage);
            Console.WriteLine(string.Empty);
            Console.ForegroundColor = currentForeColor;
        }
    }
}