using System.Drawing;

namespace HillClimber
{
    internal class Program
    {
        static Random random = new Random();

        static string GenerateString(string input)
        {
            string output = "";
            for (int i = 0; i < input.Length; i++) output += (char)random.Next(32, 127);
            return output;
        }

        static string Mutate(string input)
        {
            char[] output = input.ToCharArray();

            int index = random.Next(0, input.Length);
            int charAlteration = random.Next(0, 2) == 0 ? -1 : 1;

            if (input[index] - 1 < 32 || input[index] + 1 > 126) charAlteration *= -1;
            if (input[index] - 1 < 32 || input[index] + 1 > 126) return input;

            char alteredChar = input[index];
            alteredChar = (char)(alteredChar + charAlteration);
            output[index] = alteredChar;

            return new string(output);
        }

        static double Error(string desiredOutput, string actualOutput)
        {
            double sum = 0;
            for (int i = 0; i < desiredOutput.Length; i++) sum += Math.Abs(desiredOutput[i] - actualOutput[i]);
            return sum / desiredOutput.Length;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Write anything for hill climber:");
            string? targetString = Console.ReadLine();
            string currentString = GenerateString(targetString);

            double error = Error(targetString, currentString);
            while (error != 0)
            {
                string newString = Mutate(currentString);
                double newError = Error(targetString, newString);

                if (newError < error)
                {
                    currentString = newString;
                    error = newError;
                }
                Console.Clear();
                Console.WriteLine(currentString);
                Thread.Sleep(1);
            }
            Console.ReadKey();
        }
    }
}
