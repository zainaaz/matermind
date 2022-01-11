using System;

namespace mastermind
{
    class Game
    {
        private static String userSecretCode,
        message = "\nYou lost.";
        private static int counter = 4;
        private static String title = "MasterMind";





        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("    ------------------------------");
            Console.WriteLine("       Let's play Master-Mind ");
            Console.WriteLine("    ------------------------------");
            Console.WriteLine("\n\npress any key to continue");
            Console.ReadKey();




            SecretCode sc = new SecretCode();
            for (int i = 0; i < counter; i++)
            {
                Console.Title = title + (counter - i) + " tries left.";
                try
                {


                    Console.WriteLine("\nSecretCode must be 4 characters!");

                    Console.WriteLine("\nselect a number");
                    sc.verifySecretCode((userSecretCode = Console.ReadLine()));
                    Console.WriteLine("");
                    if (userSecretCode != sc.secretCode)
                        sc.handleUserSecretCode(userSecretCode);
                    else
                    {
                        message = "You won.";
                        break;
                    }
                }
                catch (SecretCodeFormatException e)
                {
                    Console.WriteLine(e.Message);
                    i--;
                    continue;
                }
            }
            Console.WriteLine(message + " The code was " + sc.secretCode);


        }
        public class SecretCode
        {

            public String secretCode { get; }
            public SecretCode()
            {
                secretCode = createSecretCode();

            }
            public String createSecretCode()
            {
                Random rand = new Random();
                return rand.Next(1, 7).ToString() + rand.Next(1, 7) + rand.Next(1, 7) + rand.Next(1, 7).ToString();
            }
            public void handleUserSecretCode(String pUserSecretCode)
            {
                int count = 0,
                    count1 = 0; ;
                for (int i = 0; i < 4; i++)
                {
                    if (pUserSecretCode[i] == secretCode[i])
                    {
                        count1++;
                    }
                    else
                    {
                        if (pUserSecretCode.Substring(i).Contains(secretCode[i]))
                            count++;

                    }
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(count + " Right, but in the wrong place\n");

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(count1 + " Right and in the right place\n");



                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("---------------------------------------------------------------------------");
                Console.ForegroundColor = ConsoleColor.Cyan;


            }


            public void verifySecretCode(String pUserSecretCode)
            {

                if (pUserSecretCode.Length == 4)
                {
                    foreach (char c in pUserSecretCode)
                    {
                        try
                        {
                            int n = Convert.ToInt32(c.ToString());
                            if (n < 1)
                                throw new SecretCodeFormatException("Values are lower than 1!");
                            else if (n > 6)
                                throw new SecretCodeFormatException("Values are higher than 6!");

                        }
                        catch (FormatException) { throw new SecretCodeFormatException("Numbers only!"); }
                    }
                }
                else
                    throw new SecretCodeFormatException("SecretCode must be 4 characters!");
            }


        }
        public class SecretCodeFormatException : Exception
        {
            public SecretCodeFormatException(string message) : base("\n" + message + "\n")
            {

            }
        }

    }

}
