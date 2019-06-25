using System;

// By Robert E Buitron wittem in C# for Therorem 
//
// This program will flatten an array of arbitrarily nested arrays of integers into a flat array of integers.
// 
// I decided to use a static typed language like C#.  I used Visual Studio 2017 and kept it straight forward. 
// You can enter as many nested int arrays as you wish but tested to 20.  Pressing Escape key will terminate 
// the program.  The input can only take digits, brackets and commas. Digits can be 0 to 2147483647.  Once the 
// program is running you are asked to enter your arbitrarily nested arrays.  The program  will check your input 
// for missing or extra incorrect placement of bracket and commas and let you know that the format is incorrect.  
// If the input is good, it will then flatten out the data by removing the nested int arrays leaving one int 
// array and display it horizontally.  

// NOTE - Pressing control characters may cause the program to hang.


namespace Theorem_flatten_an_array_test
{
    class Program
    {
        static void Main(string[] args)
        {
            string str;

            Console.WriteLine("Theorem Flatten Int Array Test  e.g. [[1,2,[3]],4] -> [1,2,3,4]\n");

            do
            {
                // Get key input of any digit, '[', ']' or [,]
                Console.WriteLine("Enter an array of arbitrarily nested arrays of integers. (Any digit, '[', ']' or [,]) or press escape to exit: ");
                str = "";

                ConsoleKeyInfo ch = Console.ReadKey(true);

                while ((ch.Key != ConsoleKey.Enter) || (str.Length == 0))
                {
                    if (ch.Key == ConsoleKey.Escape)
                        Environment.Exit(0);

                    if ((Char.IsDigit(ch.KeyChar)) || (ch.Key == ConsoleKey.Oem4) || (ch.Key == ConsoleKey.Oem6) || (ch.Key == ConsoleKey.OemComma))
                    {
                        //Console.WriteLine("here");
                        str += ch.KeyChar;
                        Console.Write(ch.KeyChar);
                        //Console.WriteLine("ch.Key = {0}", ch.Key);
                    }

                    ch = Console.ReadKey(true);
                }

                // Check the input by checking brackets must match each other and commas that must separate digits or a nested array. 

                int bracketCount = 0;
                int i = 0;
                Boolean isError = false;
                Boolean isDigit = false;
                Boolean isComma = false;

                while ((i < str.Length) && (isError == false))
                {

                    // Start with a left bracket must be there as the first character
                    if ((i == 0) && (str[i] == '['))
                    {
                        // Check that bracket count must be even
                        for (i = 0, bracketCount = 0; i < str.Length; i++)
                        {
                            if (str[i] == '[')
                            {
                                // Increment count
                                bracketCount++;

                                if (isDigit)
                                {
                                    // Comma missing
                                    isError = true;
                                    isDigit = false;
                                }
                            }
                            else if (str[i] == ']')
                            {
                                // Decrement count
                                bracketCount--;
                                isDigit = false;

                                if (isComma)
                                {
                                    // More than one comma error
                                    isError = true;
                                }
                            }
                            else if (char.IsDigit(str[i]))
                            {
                                // Digit 
                                isDigit = true;
                                isComma = false;
                            }
                            else if (str[i] == ',')
                            {
                                // Comma
                                if (isComma)
                                {
                                    // More than one comma error
                                    isError = true;
                                }
                                else
                                {
                                    // Comma after digit
                                    isComma = true;
                                    isDigit = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        // Error with input
                        isError = true;
                    }
                }

                if ((isError) || (bracketCount != 0) || (str.Length < 3))
                {
                    // Error with input
                    Console.WriteLine("  Incorrect input! Try again");
                }
                else
                {
                    // Remove all non digits not needed and leave digits separated by commas
                    var charsToRemove = new string[] {"[", "]"};

                    foreach (var c in charsToRemove)
                    {
                        str = str.Replace(c, "");
                    }

                    // Convert string to an int array
                    int[] intArray = Array.ConvertAll(str.Split(','), int.Parse);

                    // Display left outer brackets for int array
                    Console.Write(" -> [");

                    // Display horizonally
                    for (i = 0; i < intArray.Length - 1; i++)
                    {
                        Console.Write("{0},", intArray[i]);
                    }

                    // Display rightr brackets for int array
                    Console.WriteLine("{0}]\n", intArray[i]);
                }

            } while (true);
        }
    }
}
