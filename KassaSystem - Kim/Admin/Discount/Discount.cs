using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.Discount
{
    internal class Discount
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int DiscountedProductId { get; set; }
        public decimal DiscountPrice { get; set; } = 0m;
        public bool TwoForThree { get; set; } = false;

        public Discount()
        {
            
        }
        public Discount(DateTime startDate, DateTime endDate,
            int discountedId, decimal discountPrice, bool twoForThree)
        {
            StartDate = startDate;
            EndDate = endDate;
            DiscountedProductId = discountedId;
            DiscountPrice = discountPrice;
            TwoForThree = twoForThree;
        }
    }

}
