using KassaSystem___Kim.Admin;
using KassaSystem___Kim.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KassaSystem___Kim.Products
{
    internal static class UpdateProduct
    {
        public static Product Update(Product product)
        {
            var notUpdatedProduct = product;
            bool updating = true;

            do
            {
                Console.Clear();

                string prompt = "Vad vill du ändra?";
                string[] options = [
                    product.ProductId.ToString(),
                    product.ProductName.ToString(),
                    product.ProductPrice.ToString(),
                    product.PriceType.ToString(),
                    "SPARA",
                    "AVBRYT UTAN ATT SPARA"];

                Menu prodProperty = new Menu(prompt, options);
                int selectedIndex = prodProperty.Run();

                switch (selectedIndex)
                {
                    case 0:
                        Console.Clear();
                        Console.WriteLine("Skriv det nya Produkt ID:t");
                        Console.WriteLine("Nuvarande: " + product.ProductId);
                        Console.Write("Nya: ");

                        if (Int32.TryParse(Console.ReadLine(), out var newId))
                        {
                            var tempId = product.ProductId;
                            product.ProductId = newId;

                            Console.WriteLine($"\nDu har uppdaterat Produkt ID.\n" +
                                $"{tempId} -> {product.ProductId}");

                            Console.WriteLine();
                            Utility.WaitForKeyPress();
                        }
                        break;

                    case 1:
                        Console.Clear();
                        Console.WriteLine("Skriv det nya Produkt Namnet");
                        Console.WriteLine("Nuvarande: " + product.ProductName);
                        Console.Write("Nya: ");
                        var newName = Console.ReadLine();

                        var tempName = product.ProductName;
                        product.ProductName = newName;

                        Console.WriteLine($"\nDu har uppdaterat Produkt Namnet.\n" +
                            $"{tempName} -> {product.ProductName}");

                        Console.WriteLine();
                        Utility.WaitForKeyPress();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("Skriv det nya Produkt Priset");
                        Console.WriteLine("Nuvarande: " + product.ProductPrice);
                        Console.Write("Nya: ");

                        if (Decimal.TryParse(Console.ReadLine(), out var newPrice))
                        {
                            var tempPrice = product.ProductPrice;
                            product.ProductPrice = newPrice;

                            Console.WriteLine($"\nDu har uppdaterat Produkt Priset.\n" +
                                $"{tempPrice} -> {product.ProductPrice}");

                            Console.WriteLine();
                            Utility.WaitForKeyPress();
                        }
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("Skriv det nya Produkt Pristyp");
                        Console.WriteLine("Nuvarande: " + product.PriceType);
                        Console.Write("Nya: ");
                        var newType = Console.ReadLine();

                        var tempType = product.PriceType;
                        product.PriceType = newType;

                        Console.WriteLine($"\nDu har uppdaterat Produkt Pristyp.\n" +
                            $"{tempType} -> {product.PriceType}");

                        Console.WriteLine();
                        Utility.WaitForKeyPress();
                        break;
                    case 4:
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Du har sparat dina ändringar");
                        Console.ResetColor();

                        return product;

                    case 5:
                        Console.WriteLine("\nAvslutar...");
                        Utility.WaitForKeyPress();
                        //updating = false;
                        AdminOption.Menu();
                        break;
                }


            }
            while (updating);

            return notUpdatedProduct;

        }

        public static void ReplaceUpdated(int oldProduct, Product newProduct,
            List<Product> sortedList)
        {
            // Byt ut gamla producted i .csv med den nya. INDEX, (updateIndex i App.cs)

            foreach (var item in sortedList)
            {
                if (item.ProductId == oldProduct)
                {
                    item.ProductId = newProduct.ProductId;
                    item.ProductName = newProduct.ProductName;
                    item.ProductPrice = newProduct.ProductPrice;
                    item.PriceType = newProduct.PriceType;
                }
            }
            RewriteProductsText(sortedList);
        }

        public static void RewriteProductsText(List<Product> sortedList)
        {
            string filePath = "../../../Products/ProductText/Products.csv";

            if (File.Exists(filePath))
            {
                File.WriteAllText(filePath,
                    "ProductId;ProductName;ProductPrice,PriceType"
                    + Environment.NewLine);

                foreach (var item in sortedList)
                {
                    var textToAppend = $"{item.ProductId};{item.ProductName};" +
                        $"{item.ProductPrice};{item.PriceType}";

                    File.AppendAllText(filePath, textToAppend + Environment.NewLine);
                }
            }
        }

        public static bool CheckDuplicate(List<Product> sortedList, Product newProduct)
        {
            // Måste kolla ID
            foreach (var item in sortedList)
            {
                if (item.ProductId == newProduct.ProductId)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Du har använt samma ID från annan produkt.");
                    Console.ResetColor();

                    return true;
                }
            }
            return false;
        }
    }

}
