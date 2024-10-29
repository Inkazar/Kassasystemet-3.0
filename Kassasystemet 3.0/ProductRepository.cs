using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Globalization;

namespace Kassasystemet_3._0
{
    public class ProductRepository
    {
        private string fileName;

        public ProductRepository(string fileName)
        {
            this.fileName = fileName;
        }
        public List<Product> ReadProducts()

        {
            List<Product> products = new List<Product>();
            try
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        // Skip empty lines and header line
                        if (string.IsNullOrWhiteSpace(line) || line.StartsWith("ProductId"))
                            continue;

                        string[] parts = line.Split(',');

                        if (parts.Length == 4)
                        {
                            // Parse fields
                            if (int.TryParse(parts[0], out int productId) &&
                                double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double price))
                            {
                                string name = parts[1];
                                string priceType = parts[3];

                                // Create product object and add to list
                                Product product = new Product(productId, name, price, priceType);
                                products.Add(product);
                            }
                            else
                            {
                                Console.WriteLine($"Fel vid tolkning av raden: " +
                                $"{line}");

                            }

                        }
                        else
                        {
                            Console.WriteLine($"Fel format på raden: {line}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid läsning av filen: {ex.Message}");
            }
            return products;
        }

        public void SaveProducts(List<Product> products)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, append: false))
                {
                    writer.WriteLine("ProduktId,Namn,Pis,Pristyp");
                    foreach (var product in products)
                    {
                        writer.WriteLine($"{product.ProductId},{product.Name},{product.Price},{product.PriceType}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid skrivning till filen: {ex.Message}");
            }
        }
        public void AddProduct(Product product)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileName, append: true))
                {
                    writer.WriteLine($"{product.ProductId},{product.Name},{product.Price},{product.PriceType}");
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid skrivning till filen: {ex.Message}");
            }
        }
    }
}
