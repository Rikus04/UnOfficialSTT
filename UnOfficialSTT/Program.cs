using System;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace UnOfficialSTT
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                using (var reader = new StreamReader(@"C:\Users\Rikus\Desktop\practicalTest.csv"))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    csv.Read(); // Skip the header row
                    csv.ReadHeader(); // Read the header row to get column names

                    int rowCount = 0; // Track the number of rows
                    string[,] data = new string[100, 2]; // 2D array to store ID and new selling price

                    // Read the data rows
                    while (csv.Read())
                    {
                        string productId = csv.GetField<string>("Product ID");
                        string sellingPrice = csv.GetField<string>("Selling Price");

                        sellingPrice = sellingPrice.Replace("ZAR", "").Trim();

                        double originalSellingPrice = Convert.ToDouble(sellingPrice, CultureInfo.InvariantCulture);
                        double newSellingPrice = originalSellingPrice * 1.15;

                        // Store ID and new selling price in the array
                        data[rowCount, 0] = productId;
                        data[rowCount, 1] = newSellingPrice.ToString("F2", CultureInfo.InvariantCulture);

                        rowCount++; // Increment the row count
                    }

                    // Display the data from the array
                    for (int row = 0; row < rowCount; row++)
                    {
                        string productId = data[row, 0];
                        string newSellingPrice = data[row, 1];

                        Console.WriteLine("Product ID: " + productId);
                        Console.WriteLine("New Selling Price: ZAR " + newSellingPrice);
                        Console.WriteLine();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
    }
}
