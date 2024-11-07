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
        private List<CartItem> shoppingCart;

        public CashRegister(List<Product> products)
        {
            this.products = products;
            this.shoppingCart = new List<CartItem>();
        }

        public void NewCustomer()
        {
            Console.Clear();
            shoppingCart.Clear();
            Console.WriteLine("Ny kund startad.");
        }

        public void AddProduct(int productId, int quantity)
        {
            Product product = products.Find(p => p.ProductId == productId);
            if (product != null)
            {
                CartItem existingItem = shoppingCart.Find(item => item.Product.ProductId == productId);
                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    shoppingCart.Add(new CartItem(product, quantity));
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
