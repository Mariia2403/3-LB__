using System;
using System.IO;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CheckFiles();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Помилка: " + ex.Message);
            }
            Console.ReadLine();
        }

        static void CheckFiles()
        {
            int sumOfProducts = 0;
            int productCount = 0;
            string folderPath = @"D:\Visual Studio (2)\2 курс\ООП\3 ЛБ\1 task\Новая папка";
            string noFilePath = Path.Combine(folderPath, "no_file.txt");
            string badDataPath = Path.Combine(folderPath, "bad_data.txt");
            string overFlowPath = Path.Combine(folderPath, "overflow.txt");

            try
            {
                File.WriteAllText(noFilePath, string.Empty);
                File.WriteAllText(badDataPath, string.Empty);
                File.WriteAllText(overFlowPath, string.Empty);
            }
            catch
            {
                Console.WriteLine("Помилка при створенні або очищенні файлів для запису.");
                return;
            }

            for (int i = 10; i <= 29; i++)
            {
                string fileName = $"{i}.txt";
                string filePath = Path.Combine(folderPath, fileName);
                StreamReader reader = null;

                try
                {
                    reader = new StreamReader(filePath);

                    int number1 = int.Parse(reader.ReadLine());
                    int number2 = int.Parse(reader.ReadLine());

                    checked
                    {
                        int product = number1 * number2;
                        sumOfProducts += product;
                        productCount++;
                    }
                }
                catch (FileNotFoundException)
                {
                    File.AppendAllText(noFilePath, fileName + Environment.NewLine);
                }
                catch (FormatException)
                {
                    File.AppendAllText(badDataPath, fileName + Environment.NewLine);
                }
                catch (OverflowException)
                {
                    File.AppendAllText(overFlowPath, fileName + Environment.NewLine);
                }
                catch (Exception)
                {
                    File.AppendAllText(badDataPath, fileName + Environment.NewLine);
                }
                
            }

            try
            {
                double average = (double)sumOfProducts / productCount;
                Console.WriteLine($"Сума: {sumOfProducts}");
                Console.WriteLine($"Середнє арифметичне добутків: {average}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Не вдалося обчислити жодного добутку.");
            }

           
        }
    }
}