using KassaSystem___Kim.Products;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KassaSystem___Kim.Admin.Discount
{
    internal class DiscountService
    {
        public static void WriteToFile(Discount newDiscount)
        {
            string filePath = 
                $"../../../Admin/Discount/DiscountFiles/DiscountFile.csv";

            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
                File.WriteAllText(filePath,
                    "DiscountedId;DiscountedPrice;TwoForThree;StartDate;EndDate"
                    + Environment.NewLine);
            }
            if (File.Exists(filePath))
            {
                string textToAppend = $"" +
                    $"{newDiscount.DiscountedProductId};" +
                    $"{newDiscount.DiscountPrice};" +
                    $"{newDiscount.TwoForThree};" +
                    $"{newDiscount.StartDate};" +
                    $"{newDiscount.EndDate}";
                File.AppendAllText(filePath, textToAppend + Environment.NewLine);
            }

        }

        public static List<Discount> SortIntoList()
        {
            string path = "../../../Admin/Discount/DiscountFiles/DiscountFile.csv";
            string filePath = path;

            List<Discount> discountList = new List<Discount>();
            using (StreamReader streamReader = new StreamReader(
                    File.OpenRead(filePath)))
            {
                while (!streamReader.EndOfStream) // EndOfStream will indicate when stream is the end of file.
                {
                    var line = streamReader.ReadLine();
                    var properties = line.Split(';');

                    if (properties[0] != "DiscountedId")
                    {
                        var discount = new Discount();
                        discount.DiscountedProductId = Int32.Parse(properties[0]);
                        discount.DiscountPrice = Decimal.Parse(properties[1]);
                        discount.TwoForThree = bool.Parse(properties[2]); // ÄNDRA TILL BOOL
                        discount.StartDate = DateTime.Parse(properties[3]); // ÄNDRA TILL DATETIME
                        discount.EndDate = DateTime.Parse(properties[4]); //ÄNDRA TILL DATETIME
                        discountList.Add(discount);                        
                    }
                }
            }

            return discountList;
        }

        public static void RewriteDiscountText(List<Discount> discounts)
        {
            string filePath = "../../../Admin/Discount/DiscountFiles/DiscountFile.csv";

            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath,
                    "DiscountedId;DiscountedPrice;TwoForThree;StartDate;EndDate"
                    + Environment.NewLine);

                foreach (var item in discounts)
                {
                    string textToAppend = $"" +
                    $"{item.DiscountedProductId};" +
                    $"{item.DiscountPrice};" +
                    $"{item.TwoForThree};" +
                    $"{item.StartDate};" +
                    $"{item.EndDate}";

                    File.AppendAllText(filePath, textToAppend + Environment.NewLine);
                }
            }

        }
    }
}
