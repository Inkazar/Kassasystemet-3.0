using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet_3._0
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string PriceType { get; set; } // "per kilo" or "per piece"

        public List<Campaign> Campaigns { get; private set; }

        public Product(int productId, string name, double price, string priceType)
        {
            ProductId = productId;
            Name = name;
            Price = price;
            PriceType = priceType;
            Campaigns = new List<Campaign>();
        }

        public double GetCurrentPrice()
        {
            foreach (Campaign campaign in Campaigns)
            {
                if (campaign.IsValid())
                {
                    return campaign.CampaignPrice;
                }
            }
            return Price;
        }

        public void AddCampaign(Campaign campaign)
        {
            Campaigns.Add(campaign);
        }
    }
}
