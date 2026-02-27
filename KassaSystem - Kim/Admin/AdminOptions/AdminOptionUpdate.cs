using KassaSystem___Kim.Products;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Admin.AdminOptions
{
    internal static class AdminOptionUpdate
    {
        public static void Run()
        {
            Console.Clear();

            var readProduct = new ReadProduct();
            var oldSortedProducts = readProduct.SortIntoList();
            var sortedProducts = readProduct.SortIntoList();
            var oldProductId = 0;

            List<string> tempListToArray = new List<string>();
            foreach (var product in sortedProducts)
            {
                string tempString = $"[ {product.ProductId} " +
                    $"{product.ProductName} " +
                    $"{product.ProductPrice} " +
                    $"{product.PriceType} ]";

                tempListToArray.Add(tempString);
            }
            string[] updateOptions = tempListToArray.ToArray();

            string updatePrompt = "Ändra produkter";
            Menu updateMenu = new Menu(updatePrompt, updateOptions);
            int updateIndex = updateMenu.Run();

            var updateProduct = new Product();

            foreach (var item in sortedProducts)
            {
                for (int i = 0; i < sortedProducts.Count; i++)
                {
                    if (updateIndex == sortedProducts.IndexOf(item, i, 1))
                    {
                        oldProductId = item.ProductId;
                        // Vill inte den nya produkten kollar sig själv.
                        oldSortedProducts.RemoveAt(i);
                        updateProduct = UpdateProduct.Update(item);
                    }
                }
            }

            bool isDuplicate = UpdateProduct.CheckDuplicate(
                oldSortedProducts, updateProduct);

            if (isDuplicate != true)
            {
                UpdateProduct.ReplaceUpdated(
                    oldProductId, updateProduct, sortedProducts);
            }
            else
            {
                Console.WriteLine("Återvänder till menyn...");
                Utility.WaitForKeyPress();

                // Kanske ha en mer specifikt för denna case.
                AdminOption.Menu();
                //AdminOptionUpdate.Run();
            }

            Console.WriteLine();
            Console.WriteLine("Listan av produkter uppdaterad!");
            Utility.WaitForKeyPress();
        }
    }
}
