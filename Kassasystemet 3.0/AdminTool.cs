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
            Console.WriteLine("Skriv in ProduktID du vill ändra:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                Product product = products.Find(p => p.ProductId == productId);
                if (product != null)
                {
                    Console.WriteLine($"Namn: {product.Name}, Pris: {product.Price}");
                    Console.WriteLine("Skriv in det nya namnet (eller tryck på Enter för att behålla det gamla1):");
                    string newName = Console.ReadLine();
                    if (!string.IsNullOrEmpty(newName))
                    {
                        product.Name = newName;
                    }
                    Console.WriteLine("Mata in det nya priset (eller tryck på enter för att behålla det gamla):");
                    string newPriceInput = Console.ReadLine();
                    if (double.TryParse(newPriceInput, out double newPrice))
                    {
                        product.Price = newPrice;
                    }
                    Console.WriteLine("Produkten är uppdaterad.");

                   
                    productRepository.SaveProducts(products);
                }
                else
                {
                    Console.WriteLine("Produkten hittades inte.");
                }
            }
            else
            {
                Console.WriteLine("Felaktigt Produkt ID.");
            }
        }

        public void AddProduct()
        {
            Console.WriteLine("Mata in Produkt ID:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                if (products.Exists(p => p.ProductId == productId))
                {
                    Console.WriteLine("En produkt med det här ID't finns redan.");
                    return;
                }
                Console.WriteLine("Skriv in namnet:");
                string name = Console.ReadLine();
                Console.WriteLine("Skriv in priset:");
                if (double.TryParse(Console.ReadLine(), out double price))
                {
                    Console.WriteLine("Skriv in pristypen (per kilo/per styck):");
                    string priceType = Console.ReadLine();
                    Product newProduct = new Product(productId, name, price, priceType);
                    products.Add(newProduct);
                    Console.WriteLine("Produkten är tillagd.");

                    // Save the new product to file
                    productRepository.AddProduct(newProduct);
                }
                else
                {
                    Console.WriteLine("Felaktigt pris.");
                }
            }
            else
            {
                Console.WriteLine("Felaktigt Produkt ID.");
            }
        }

        public void ManageCampaigns()
        {
            Console.WriteLine("Skriv in Produkt ID't som du vill ändra administrera kampanjen för:");
            if (int.TryParse(Console.ReadLine(), out int productId))
            {
                Product product = products.Find(p => p.ProductId == productId);
                if (product != null)
                {
                    while (true)
                    {
                        
                        Console.WriteLine($"\nKampanjen för {product.Name}:");
                        Console.WriteLine("1. Lägg till kampanj");
                        Console.WriteLine("2. Ta bort kampanj");
                        Console.WriteLine("3. Visa kampanjer");
                        Console.WriteLine("4. Tillbaka till menyn");
                        Console.Write("Välj alternativ: ");

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
                else
                {
                    Console.WriteLine("Produkten hittades inte");
                }
            }
            else
            {
                Console.WriteLine("Felaktigt Produkt ID.");
            }
        }

        private void AddCampaign(Product product)
        {
            Console.WriteLine("Skriv in startdatum (format: yyyy-MM-dd):");
            if (DateTime.TryParse(Console.ReadLine(), out DateTime startDate))
            {
                Console.WriteLine("Skriv in slutdatum (format: yyyy-MM-dd):");
                if (DateTime.TryParse(Console.ReadLine(), out DateTime endDate))
                {
                    Console.WriteLine("Mata in kampanj priset:");
                    if (double.TryParse(Console.ReadLine(), out double campaignPrice))
                    {
                        Campaign campaign = new Campaign(startDate, endDate, campaignPrice);
                        product.AddCampaign(campaign);
                        Console.WriteLine("Kampanjen har lagts till.");
                    }
                    else
                    {
                        Console.WriteLine("Felaktigt kampanj pris.");
                    }
                }
                else
                {
                    Console.WriteLine("Felaktigt slutdatum.");
                }
            }
            else
            {
                Console.WriteLine("Felaktigt startdatum.");
            }
        }

        private void RemoveCampaign(Product product)
        {
            if (product.Campaigns.Count == 0)
            {
                Console.WriteLine("Det finns ingen kampanj att ta bort.");
                return;
            }

            ViewCampaigns(product);
            Console.WriteLine("Skriv in numret på kampanjen du vill ta bort:");
            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index >= 1 && index <= product.Campaigns.Count)
                {
                    product.Campaigns.RemoveAt(index - 1);
                    Console.WriteLine("Kampnajen har tagits bort.");
                }
                else
                {
                    Console.WriteLine("Felaktigt nummer.");
                }
            }
            else
            {
                Console.WriteLine("Det var ingen siffra.");
            }
        }

        private void ViewCampaigns(Product product)
        {
            Console.WriteLine($"\nKampanjer för {product.Name}:");
            if (product.Campaigns.Count == 0)
            {
                Console.WriteLine("Inga kampanjer.");
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
