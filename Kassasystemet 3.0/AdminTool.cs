using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet_3._0
{
    
    public class AdminTool
    {
        private List<Product> products;
        private ProductRepository productRepository;

        public AdminTool(List<Product> products, ProductRepository productRepository)
        {
            this.products = products;
            this.productRepository = productRepository;
        }

        public void EditProduct()
        {
            Console.WriteLine("Enter the Product ID to edit:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                Product product = products.Find(p => p.ProductId == productId);
                if (product != null)
                {
                    Console.WriteLine($"Current name: {product.Name}, price: {product.Price}");
                    Console.WriteLine("Enter new name (or press Enter to keep current):");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        product.Name = newName;
                    }
                    Console.WriteLine("Enter new price (or press Enter to keep current):");
                    string newPriceInput = Console.ReadLine();
                    if (double.TryParse(newPriceInput, out double newPrice))
                    {
                        product.Price = newPrice;
                    }
                    Console.WriteLine("Product has been updated.");

                    // Save all products to file
                    productRepository.SaveProducts(products);
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Product ID.");
            }
        }

        public void AddProduct()
        {
            Console.WriteLine("Enter Product ID:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                if (products.Exists(p => p.ProductId == productId))
                {
                    Console.WriteLine("A product with this ID already exists.");
                    return;
                }
                Console.WriteLine("Enter name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter price:");
                if (double.TryParse(Console.ReadLine(), out double price))
                {
                    Console.WriteLine("Enter price type (per kilo/per piece):");
                    string priceType = Console.ReadLine();
                    Product newProduct = new Product(productId, name, price, priceType);
                    products.Add(newProduct);
                    Console.WriteLine("Product has been added.");

                    // Save the new product to file
                    productRepository.AddProduct(newProduct);
                }
                else
                {
                    Console.WriteLine("Invalid price.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Product ID.");
            }
        }

        public void ManageCampaigns()
        {
            Console.WriteLine("Enter the Product ID to manage campaigns for:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                Product product = products.Find(p => p.ProductId == productId);
                if (product != null)
                {
                    while (true)
                    {
                        Console.WriteLine($"\nCampaign management for {product.Name}:");
                        Console.WriteLine("1. Add Campaign");
                        Console.WriteLine("2. Remove Campaign");
                        Console.WriteLine("3. View Campaigns");
                        Console.WriteLine("4. Back to Admin Menu");
                        Console.Write("Choose an option: ");

                        int choice;
                        bool isNumber = int.TryParse(Console.ReadLine(), out choice);

                        if (isNumber)
                        {
                            switch (choice)
                            {
                                case 1:
                                    AddCampaign(product);
                                    break;
                                case 2:
                                    RemoveCampaign(product);
                                    break;
                                case 3:
                                    ViewCampaigns(product);
                                    break;
                                case 4:
                                    return; // Back to admin menu
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
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid Product ID.");
            }
        }

        private void AddCampaign(Product product)
        {
            Console.WriteLine("Enter start date (format: yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Enter end date (format: yyyy-MM-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    Console.WriteLine("Enter campaign price:");
                    if (double.TryParse(Console.ReadLine(), out double campaignPrice))
                    {
                        Campaign campaign = new Campaign(startDate, endDate, campaignPrice);
                        product.AddCampaign(campaign);
                        Console.WriteLine("Campaign has been added.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid campaign price.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid end date.");
                }
            }
            else
            {
                Console.WriteLine("Invalid start date.");
            }
        }

        private void RemoveCampaign(Product product)
        {
            if (product.Campaigns.Count == 0)
            {
                Console.WriteLine("No campaigns to remove.");
                return;
            }

            ViewCampaigns(product);
            Console.WriteLine("Enter the number of the campaign to remove:");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index >= 1 && index <= product.Campaigns.Count)
                {
                    product.Campaigns.RemoveAt(index - 1);
                    Console.WriteLine("Campaign has been removed.");
                }
                else
                {
                    Console.WriteLine("Invalid number.");
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid number.");
            }
        }

        private void ViewCampaigns(Product product)
        {
            Console.WriteLine($"\nCampaigns for {product.Name}:");
            if (product.Campaigns.Count == 0)
            {
                Console.WriteLine("No campaigns.");
            }
            else
            {
                int i = 1;
                foreach (var campaign in product.Campaigns)
                {
                    Console.WriteLine($"{i}. {campaign.StartDate:yyyy-MM-dd} to {campaign.EndDate:yyyy-MM-dd} - {campaign.CampaignPrice} kr");
                    i++;
                }
            }
        }
    }

}
