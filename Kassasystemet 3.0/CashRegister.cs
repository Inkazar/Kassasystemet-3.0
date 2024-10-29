using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet_3._0
{
   

    public class CashRegister
    {
        private List<Product> products;
        private Dictionary<Product, int> shoppingCart;

        public CashRegister(List<Product> products)
        {
            this.products = products;
            this.shoppingCart = new Dictionary<Product, int>();
        }

        public void NewCustomer()
        {
            shoppingCart.Clear();
            Console.WriteLine("Ny kund startad.");
        }

        public void AddProduct(int productId, int quantity)
        {
            Product product = products.Find(p => p.ProductId == productId);
            if (product != null)
            {
                if (shoppingCart.ContainsKey(product))
                {
                    shoppingCart[product] += quantity;
                }
                else
                {
                    shoppingCart.Add(product, quantity);
                }
                Console.WriteLine($"{quantity} x {product.Name} lagt i kundvagnen.");
            }
            else
            {
                Console.WriteLine($"Produkten med ID {productId} hittades inte.");
            }
        }

        public void Pay()
        {
            
            Receipt receipt = new Receipt(shoppingCart);
            receipt.SaveReceipt();
            receipt.DisplayReceipt();
            NewCustomer();
        }
    }

}
