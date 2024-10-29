using Kassasystemet_3._0;
using System;
using System.Collections.Generic;


public class Program
{
    static void Main(string[] args)
    {
        string productFileName = "../../../products.txt";
        ProductRepository productRepository = new ProductRepository(productFileName);

        List<Product> products = productRepository.ReadProducts();

        CashRegister cashRegister = new CashRegister(products);
        AdminTool adminTool = new AdminTool(products, productRepository);

        while (true)
        {
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. New Customer");
            Console.WriteLine("2. View Receipts");
            Console.WriteLine("3. Admin Tools");
            Console.WriteLine("4. Exit");
            Console.Write("Choose an option: ");

            int choice;
            bool isNumber = int.TryParse(Console.ReadLine(), out choice);

            if (isNumber)
            {
                switch (choice)
                {
                    case 1:
                        HandleNewCustomer(cashRegister);
                        break;
                    case 2:
                        ViewReceipts();
                        break;
                    case 3:
                        HandleAdminTools(adminTool);
                        break;
                    case 4:
                        return; // Exit the program
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }

    private static void HandleNewCustomer(CashRegister cashRegister)
    {
        cashRegister.NewCustomer();
        while (true)
        {
            Console.WriteLine("Enter command (<productId> <quantity> or PAY):");
            string command = Console.ReadLine().Trim();
            if (command.Equals("PAY", StringComparison.OrdinalIgnoreCase))
            {
                cashRegister.Pay();
                break;
            }
            else
            {
                string[] parts = command.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && int.TryParse(parts[0], out int productId) && int.TryParse(parts[1], out int quantity))
                {
                    cashRegister.AddProduct(productId, quantity);
                }
                else
                {
                    Console.WriteLine("Invalid command. Try again.");
                }
            }
        }
    }

    private static void ViewReceipts()
    {
        Console.WriteLine("Enter date for receipts to view (format: yyyyMMdd), or press Enter for today's date:");
        string input = Console.ReadLine();
        string date = string.IsNullOrEmpty(input) ? DateTime.Now.ToString("yyyyMMdd") : input;

        string fileName = $"RECEIPT_{date}.txt";
        if (File.Exists(fileName))
        {
            string content = File.ReadAllText(fileName);
            Console.WriteLine($"\nReceipts for {date}:");
            Console.WriteLine(content);
        }
        else
        {
            Console.WriteLine($"No receipts found for date {date}.");
        }
    }

    private static void HandleAdminTools(AdminTool adminTool)
    {
        while (true)
        {
            Console.WriteLine("\nAdmin Tools:");
            Console.WriteLine("1. Edit Product");
            Console.WriteLine("2. Add Product");
            Console.WriteLine("3. Manage Campaigns");
            Console.WriteLine("4. Back to Main Menu");
            Console.Write("Choose an option: ");

            int choice;
            bool isNumber = int.TryParse(Console.ReadLine(), out choice);

            if (isNumber)
            {
                switch (choice)
                {
                    case 1:
                        adminTool.EditProduct();
                        break;
                    case 2:
                        adminTool.AddProduct();
                        break;
                    case 3:
                        adminTool.ManageCampaigns();
                        break;
                    case 4:
                        return; // Back to main menu
                    default:
                        Console.WriteLine("Invalid choice. Try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }
    }
}
