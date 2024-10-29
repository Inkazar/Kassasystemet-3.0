using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kassasystemet_3._0
{
    public class Campaign
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public double CampaignPrice { get; set; }

        public Campaign (DateTime startDate, DateTime endDate, double campaignPrice)
        {
            StartDate = startDate;
            EndDate = endDate;
            CampaignPrice = campaignPrice;
        }

        public bool IsValid()
        {
            DateTime today = DateTime.Now.Date;
            return today >= StartDate && today <= EndDate;
        }
    }
}
