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
            Console.WriteLine("New customer session started.");
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
                Console.WriteLine($"{quantity} x {product.Name} added to the shopping cart.");
            }
            else
            {
                Console.WriteLine($"Product with ID {productId} not found.");
            }
        }

        public void Pay()
        {
            
            Receipt receipt = new Receipt(shoppingCart);
            receipt.SaveReceipt();
            NewCustomer();
        }
    }

}
