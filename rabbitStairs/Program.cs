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

 
            using (FileStream fstream = new FileStream("input.txt", FileMode.OpenOrCreate))
            {
                byte[] output = new byte[10];
                fstream.Read(output, 0, output.Length);
                string textFromFile = Encoding.Default.GetString(output);

                string[] words = textFromFile.Split(new[] { ' ', ',', ':', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

                try
                {
                    lineObj.k = Int16.Parse(words[0]);
                    lineObj.n = Int16.Parse(words[1]);
                }
                catch (Exception e) when (e is OverflowException || e is FormatException)
                {
                    Console.WriteLine("Enter a correct input information. Program will be close.");
                    // Delay
                    Console.ReadLine();
                    Environment.Exit(-1);
                }
                
            }

            // Checking user input and fix handling errors
            if (lineObj.k < 1)
            {
                Console.WriteLine("Quantity of jumps can't be less than 1. Program will be close.");
                // Delay
                Console.ReadLine();
                Environment.Exit(-1);
            }

            if (lineObj.n < lineObj.k)
            {
                Console.WriteLine("Quantity of stairs can't be less than quantity of jumps. Program will be close.");
                // Delay
                Console.ReadLine();
                Environment.Exit(-1);
            }

            if (lineObj.n > 300)
            {
                Console.WriteLine("The stair is too high for rabbit. It can't be grater than 300 steps. Program will be close.");
                // Delay
                Console.ReadLine();
                Environment.Exit(-1);
            }


            result = lineObj.Rabbit(lineObj.k, lineObj.n);

            using (FileStream outFstream = new FileStream("output.txt", FileMode.Create))
            {
                string taskAnswer = result.ToString();
                
                byte[] input = Encoding.Default.GetBytes(taskAnswer);
                outFstream.Write(input, 0, input.Length);
                Console.WriteLine("Answer was written in file.");
            }

            Console.ReadLine();
        }
    }

    class InputLine
    {
        public string line { get; set; }

        public int k { get; set; }
        public int n { get; set; }

        public int Rabbit(int k, int n)
        {
            // We get negative number, this means that our stairs is less than jump length. This method doesn't work
            if (n < 0) return 0;
            // We are at the top of the stairs, previous jump guide us to the target
            if (n == 0) return 1;
            // Calculating the quantity of possibilities for current stairs
            int s = 0;
            // Trying all possibilities of jumping
            for (int i = 1; i <= k; i++) 
            {
                // Making the jump for i step will guide us to the same task with a stair that have (n - i) steps
                s += Rabbit(k, n - i);
            }
            return s;
        }
    }

  

}


