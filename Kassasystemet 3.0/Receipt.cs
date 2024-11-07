using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet_3._0
{
    
    public class Receipt
    {
        private static int latestReceiptNumber = ReadLatestReceiptNumber();
        private int receiptNumber;
        private List<CartItem> items;
        private double totalPrice;

        public Receipt(List<CartItem> items)
        {
            this.items = new List<CartItem>(items);
            this.receiptNumber = ++latestReceiptNumber;
            SaveLatestReceiptNumber();
            CalculateTotalPrice();
        }

        private static int ReadLatestReceiptNumber()
        {
            string fileName = "latestReceiptNumber.txt";
            if (File.Exists(fileName))
            {
                string text = File.ReadAllText(fileName);
                return int.Parse(text);
            }
            return 0;
        }

        private void SaveLatestReceiptNumber()
        {
            string fileName = "latestReceiptNumber.txt";
            File.WriteAllText(fileName, receiptNumber.ToString());
        }

        private void CalculateTotalPrice()
        {
            totalPrice = 0;
            foreach (var item in items)
            {
                double price = item.Product.GetCurrentPrice();
                totalPrice += price * item.Quantity;
            }
        }

        public void SaveReceipt()
        {
            string fileName = $"RECEIPT_{DateTime.Now:yyyyMMdd}.txt";
            using (StreamWriter writer = new StreamWriter(fileName, append: true))
            {
                writer.WriteLine($"----- Kvitto #{receiptNumber} -----");
                foreach (var item in items)
                {
                    double price = item.Product.GetCurrentPrice();
                    writer.WriteLine($"{item.Quantity} x {item.Product.Name} - {price} kr");
                }
                writer.WriteLine($"Totalt: {Math.Round(totalPrice, 2):F2} kr");
                writer.WriteLine("---------------------------");
            }
        }
        public void DisplayReceipt()
        {
            Console.WriteLine($"\n----- Kvitto #{receiptNumber} -----");
            foreach ( var item in items)
            {
                double price = (double)item.Product.GetCurrentPrice();
                Console.WriteLine($"{item.Quantity} x {item.Product.Name} - {price} kr");
            }
            
            Console.WriteLine($"Totalt: {Math.Round(totalPrice, 2):F2} kr");
            Console.WriteLine("---------------------------");
            Console.ReadKey();
        }
    }

}
