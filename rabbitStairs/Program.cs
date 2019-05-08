using System;
using System.IO;
using System.Numerics;
using System.Text;

namespace rabbitStairs
{
    class Program
    {
        public static void Main()
        {
            ParseInput parseInput = new ParseInput();
            Calculation calculation = new Calculation();

            try
            {
                parseInput.FileRead();
            }
            catch (Exception e) when (e is OverflowException || e is FormatException)
            {
                Console.WriteLine("Enter a correct input information. Program will be close.");
                parseInput.Exit();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find file \"input.txt\" in folder ...\\rabbitStairs\\bin\\Debug\\netcoreapp2.1. Program will be close. ");
                parseInput.Exit();
            }

            var result = calculation.VariantCalculaiton(parseInput.Steps, parseInput.QtyStairs);

            var taskAnswer = result.ToString();

            using (FileStream outFstream = new FileStream("output.txt", FileMode.Create))
            {
                byte[] input = Encoding.Default.GetBytes(taskAnswer);
                outFstream.Write(input, 0, input.Length);
                Console.WriteLine("Answer was written in file.");
            }

            Console.ReadKey();
        }
    }

    class Calculation
    {
        public BigInteger VariantCalculaiton(int maxjump, int stairs)
        {
            // Creating array for all possible variants  
            BigInteger[] variants = new BigInteger[stairs + 1];
            // First level have only 1 variant 
            variants[0] = 1;
            variants[1] = 1;
            // Calculating in 10^9 system for improving speed of calculating(max 450000 operations for case k=300, n=300)
            // Calculating variants where rabbit can jump from begin of the stairs  
            for (var i = 2; i <= maxjump; i++)
            {
                variants[i] = variants[i - 1] << 1;
            }
            // Continue calculating for cases where rabbit jump from other levels   
            for (var i = maxjump + 1; i <= stairs; i++)
            {
                variants[i] = (variants[i - 1] << 1) - (variants[i - maxjump - 1]);
            }

            return variants[stairs];
        }
    }

    public class ParseInput
    {
        public int Steps { get; set; }
        public int QtyStairs { get; set; }

        public void FileRead()
        {
            using (FileStream fstream = new FileStream("input.txt", FileMode.Open))
            {
                byte[] output = new byte[10];
                fstream.Read(output, 0, output.Length);
                var textFromFile = Encoding.Default.GetString(output);

                string[] elements = textFromFile.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                  
                if (elements.Length < 2)
                {
                    Console.WriteLine("Needed at least 2 elements for correct work. Program will be close.");
                    Exit();
                }
                else
                {
                    Steps = int.Parse(elements[0]);
                    QtyStairs = int.Parse(elements[1]);
                }

                if (Steps < 1)
                {
                    Console.WriteLine("Quantity of jumps can't be less than 1. Program will be close.");
                    Exit();
                }
                if (QtyStairs < Steps)
                {
                    Console.WriteLine("Quantity of stairs can't be less than quantity of jumps. Program will be close.");
                    Exit();
                }
                if (QtyStairs > 3000)
                {
                    Console.WriteLine("You entered more than 3000 it can take a wile.");
                }
            }
        }

        public void Exit()
        {
            // Delay
            Console.ReadKey();
            Environment.Exit(-1);
        }
    }
}


  




