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
            Console.Clear();
            Console.WriteLine("\nMain Menu:");
            Console.WriteLine("1. Ny Kund");
            Console.WriteLine("2. Visa kvitton");
            Console.WriteLine("3. Admin Verktyg");
            Console.WriteLine("4. Avsluta");
            Console.Write("Välj alternativ: ");

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
                        return; 
                    default:
                        Console.WriteLine("Felaktigt val, försök igen");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Välj en giltig siffra");
            }
        }
    }

    private static void HandleNewCustomer(CashRegister cashRegister)
    {
        cashRegister.NewCustomer();
        while (true)
        {
            
            
            Console.WriteLine("Skriv kommando: (<ProduktID> <Antal> or PAY):");
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
                    Console.WriteLine("Felaktig inmatining, försök igen");
                }
            }
        }
    }

    private static void ViewReceipts()
    {
        Console.WriteLine("Skriv in datumet för kvittot du vill titta på (format: yyyyMMdd), eller tryck på enter för dagens datum:");
        string input = Console.ReadLine();
        string date = string.IsNullOrEmpty(input) ? DateTime.Now.ToString("yyyyMMdd") : input;

        string fileName = $"RECEIPT_{date}.txt";
        if (File.Exists(fileName))
        {
            string content = File.ReadAllText(fileName);
            Console.WriteLine($"\nKvitton för {date}:");
            Console.WriteLine(content);
        }
        
        else
        {
            Console.WriteLine($"Felaktigt format eller inga kvitton hittade för {date}");
        }
        Console.ReadLine();
    }

    private static void HandleAdminTools(AdminTool adminTool)
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("\nAdmin Verktyg:");
            Console.WriteLine("1. Ändra Produkt");
            Console.WriteLine("2. Lägg till Produkt");
            Console.WriteLine("3. Ändra kampanjer");
            Console.WriteLine("4. Tillbaka till menyn");
            Console.Write("Välj alternativ: ");

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
                        return;
                    default:
                        Console.WriteLine("Felaktigt val, försök igen");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Välj en giltig siffra");
            }
        }
    }
}
