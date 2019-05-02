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

            InputLine lineObj = new InputLine();

            try
            {
                using (FileStream fstream = new FileStream("input.txt", FileMode.Open))
                {
                    byte[] output = new byte[10];
                    fstream.Read(output, 0, output.Length);
                    var textFromFile = Encoding.Default.GetString(output);

                    string[] elements = textFromFile.Split(new[] { ','}, StringSplitOptions.RemoveEmptyEntries);

                    try
                    {
                        if (elements.Length < 2)
                        {
                            Console.WriteLine("Needed at least 2 elements for correct work. Program will be close.");
                            lineObj.Exit();
                        }
                        else
                        {
                            lineObj.Steps = int.Parse(elements[0]);
                            lineObj.QtyStairs = int.Parse(elements[1]);
                        }
                        
                    }
                    catch (Exception e) when (e is OverflowException || e is FormatException)
                    {
                        Console.WriteLine("Enter a correct input information. Program will be close.");
                        lineObj.Exit();
                    }

                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Could not find file \"input.txt\" in folder ...\\rabbitStairs\\bin\\Debug\\netcoreapp2.1. Program will be close. ");
                lineObj.Exit();
            }
            
            lineObj.CheckInput(lineObj.Steps, lineObj.QtyStairs);
          
            var result = lineObj.RabbitVariantCalculaiton(lineObj.Steps, lineObj.QtyStairs);

            var taskAnswer = result.ToString();

            using (FileStream outFstream = new FileStream("output.txt", FileMode.Create))
            {
                byte[] input = Encoding.Default.GetBytes(taskAnswer);
                outFstream.Write(input, 0, input.Length);
                Console.WriteLine("Answer was written in file.");
            }

            Console.ReadLine();

            
        }
    }

    class InputLine
    {
        public int Steps { get; set; }
        public int QtyStairs { get; set; }

        public BigInteger RabbitVariantCalculaiton(int k, int n)
        {
            // Creating array for all possible variants  
            BigInteger[] variants = new BigInteger[n + 1];
            // First level have only 1 variant 
            variants[0] = 1;
            variants[1] = 1;
            // Calculating in 10^9 system for improving speed of calculating(max 450000 operations for case k=300, n=300)
            // Calculating variants where rabbit can jump from begin of the stairs  
            for (var i = 2; i <= k; i++)
            {
                variants[i] = variants[i - 1] << 1;
            }
            // Continue calculating for cases where rabbit jump from other levels   
            for (var i = k + 1; i <= n; i++)
            {
                variants[i] = (variants[i - 1] << 1) - (variants[i - k - 1]);
            }

            return variants[n];
        }

        public void CheckInput(int k, int n)
        {
            if (k < 1)
            {
                Console.WriteLine("Quantity of jumps can't be less than 1. Program will be close.");
                Exit();
            }
            if (n < k)
            {
                Console.WriteLine("Quantity of stairs can't be less than quantity of jumps. Program will be close.");
                Exit();
            }
            if (n > 300)
            {
                Console.WriteLine("The stair is too high for rabbit. It can't be grater than 300 steps. Program will be close.");
                Exit();
            }

        }

        public void Exit()
        {
            // Delay
            Console.ReadLine();
            Environment.Exit(-1);
        }
    }


  

}


