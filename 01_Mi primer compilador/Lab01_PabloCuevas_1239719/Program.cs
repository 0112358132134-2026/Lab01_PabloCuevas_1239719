using System;
namespace Lab01_PabloCuevas_1239719
{
    class Program
    {
        static void Main(string[] args)
        {
            bool _continue = true;
            while (_continue)
            {
                Console.WriteLine("Enter some algebraic expression:");
                string regexp = Console.ReadLine();
                Parser parser = new Parser();

                try
                {
                    double result = parser.Parse(regexp);
                    Console.WriteLine("Result: " + result);
                }
                catch (Exception)
                {
                    Console.WriteLine("Invalid expression");
                }

                Console.WriteLine("Press any key to continue");
                Console.ReadKey();
                Console.Clear();

                bool ok = false;
                while (!ok)
                {
                    Console.WriteLine("Do you want to continue validating? (0: No - 1: Yes)");
                    try
                    {
                        int option = int.Parse(Console.ReadLine());
                        if (option == 1 || option == 0)
                        {
                            if (option == 0)
                            {
                                _continue = false;
                            }
                            ok = true;
                        }
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Enter 0 or 1");
                        Console.ReadLine();
                    }
                    Console.Clear();
                }
            }
        } // MAIN
    }
}