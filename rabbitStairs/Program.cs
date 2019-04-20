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

                lineObj.k = Int16.Parse(words[0]);
                lineObj.n = Int16.Parse(words[1]);
            }




            result = lineObj.Rabbit(lineObj.k, lineObj.n);

            using (FileStream outFstream = new FileStream("output.txt", FileMode.OpenOrCreate))
            {
                string taskAnswer = result.ToString();

                byte[] input = Encoding.Default.GetBytes(taskAnswer);
                outFstream.Write(input, 0, input.Length);
                Console.WriteLine("Текст записан в файл");
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
            if (n < 0) return 0; // мы получили отрицательное число, значит в лестнице было меньше ступеней, чем была длина прыжка, этот способ добраться до вершины лестницы не работает
            if (n == 0) return 1; // мы не вершине лестницы, предыдущий прыжок привел нас к цели
            int s = 0; // считаем количество способов для текущей лестницы
            for (int i = 1; i <= k; i++) // пробуем все варианты прыжков
            {
                s += Rabbit(k, n - i); // совершив прыжок на i ступеней, мы получим ту же задачу, но с лестницей, в которой n - i ступеней
            }
            return s;
        }
    }

  

}


