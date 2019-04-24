using System;
using System.IO;
using System.Text;

namespace rabbitStairs
{
    class Program
    {
        public static void Main()
        {


            int result;

            InputLine lineObj = new InputLine();

            using (FileStream fstream = new FileStream("input.txt", FileMode.Open))
            {
                byte[] output = new byte[10];
                fstream.Read(output, 0, output.Length);
                var textFromFile = Encoding.Default.GetString(output);

                string[] elements = textFromFile.Split(new[] { ' ', ',', ':', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    lineObj.Steps = int.Parse(elements[0]);
                    lineObj.QtyStairs = int.Parse(elements[1]);
                }
                catch (Exception e) when (e is OverflowException || e is FormatException)
                {
                    Console.WriteLine("Enter a correct input information. Program will be close.");
                    // Delay
                    Console.ReadLine();
                    Environment.Exit(-1);
                }
                
            }

            lineObj.CheckInput(lineObj.Steps, lineObj.QtyStairs);
          
            result = lineObj.RabbitVariantCalculaiton(lineObj.Steps, lineObj.QtyStairs);

            using (FileStream outFstream = new FileStream("output.txt", FileMode.Create))
            {
                var taskAnswer = result.ToString();
                
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

        public int RabbitVariantCalculaiton(int k, int n)
        {
            // We get negative number, this means that our stairs is less than jump length. This method doesn't work
            if (n < 0) return 0;
            // We are at the top of the stairs, previous jump guide us to the target
            if (n == 0) return 1;
            // Calculating the quantity of possibilities for current stairs
            int s = 0;
            // Trying all possibilities of jumping
            for (var i = 1; i <= k; i++) 
            {
                // Making the jump for i step will guide us to the same task with a stair that have (n - i) steps
                s += RabbitVariantCalculaiton(k, n - i);
            }
            return s;
        }

        public void CheckInput(int k, int n)
        {
            if (k < 1)
            {
                Console.WriteLine("Quantity of jumps can't be less than 1. Program will be close.");
                // Delay
                Console.ReadLine();
                Environment.Exit(-1);
            }
            if (n < k)
            {
                Console.WriteLine("Quantity of stairs can't be less than quantity of jumps. Program will be close.");
                // Delay
                Console.ReadLine();
                Environment.Exit(-1);
            }
            if (n > 300)
            {
                Console.WriteLine("The stair is too high for rabbit. It can't be grater than 300 steps. Program will be close.");
                // Delay
                Console.ReadLine();
                Environment.Exit(-1);
            }

        }
    }


  

}


