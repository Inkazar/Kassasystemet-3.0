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
        private Dictionary<Product, int> items;
        private double totalPrice;

        public Receipt(Dictionary<Product, int> items)
        {
            this.items = new Dictionary<Product, int>(items);
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
            foreach (var entry in items)
            {
                double price = entry.Key.GetCurrentPrice();
                totalPrice += price * entry.Value;
            }
        }

        public void SaveReceipt()
        {
            string fileName = $"RECEIPT_{DateTime.Now:yyyyMMdd}.txt";
            using (StreamWriter writer = new StreamWriter(fileName, append: true))
            {
                writer.WriteLine($"----- Receipt #{receiptNumber} -----");
                foreach (var entry in items)
                {
                    double price = entry.Key.GetCurrentPrice();
                    writer.WriteLine($"{entry.Value} x {entry.Key.Name} - {price} kr");
                }
                writer.WriteLine($"Total: {totalPrice} kr");
                writer.WriteLine("---------------------------");
            }
        }
    }

}
