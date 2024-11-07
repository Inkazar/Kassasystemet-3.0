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

        
    }

}
