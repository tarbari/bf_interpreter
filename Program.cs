using System;
using System.IO;

namespace brainfuck_interpreter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Defining the variables.
            byte[] tape = new byte[30000];      // The array of bytes we will modify and print
            int tapePos = 0;                    // Defines which index in the Tape array is being modified
            string prg;                         // Array of commands that make up the program
            int prgPos = 0;                     // Defines which index in the prg array is being executed
            int bracketCounter = 0;             // We need this to count brackets because of the possible nested loops

            // Read a file called program.bf and import the data to prg string
            using (var extFile = new StreamReader("program.bf"))
            {
                prg = extFile.ReadToEnd();
            }

            // Defining commands
            while (prgPos < prg.Length)
            {
                // Move to the previous index on the array
                if (prg[prgPos] == '<')
                {
                    tapePos--;
                    prgPos++;
                }
                // Move to the next index on the array
                else if (prg[prgPos] == '>')
                {
                    tapePos++;
                    prgPos++; 
                }
                // Convert the value in the current index of the array into ASCII and print it.
                else if (prg[prgPos] == '.')
                {
                    Console.Write(Convert.ToChar(tape[tapePos]));
                    prgPos++;
                }
                // Ask user input and store the character in the current index of the array
                else if (prg[prgPos] == ',')
                {
                    var inp = Console.ReadKey(true);
                    tape[tapePos] = (byte)inp.KeyChar;
                    prgPos++;
                }
                // Increase the value of the current index in the array
                else if (prg[prgPos] == '+')
                {
                    tape[tapePos]++;
                    prgPos++;
                }
                // Decrease the value of the current index in the array
                else if (prg[prgPos] == '-')
                {
                    tape[tapePos]--;
                    prgPos++;
                }
                // This command starts a sort of while loop in bf
                // If the value in the current cell (tape[tapePos]) is not 0 we just continue to the next instruction
                // If the value in the current cell IS 0, we move to the next instruction but instead of running it we check if it's [ or ] or ] AND the bracket counter is 0
                // This lets us calculate if the next ] we encounter is the pair of the [ we started from.
                else if (prg[prgPos] == '[')
                {
                    if (tape[tapePos] == 0)
                    {
                        prgPos++;
                        while (prgPos < prg.Length)
                        {
                            if (prg[prgPos] == ']' && bracketCounter == 0)
                            {
                                break;
                            }
                            else if (prg[prgPos] == '[')
                            {
                                bracketCounter++;
                            }
                            else if (prg[prgPos] == ']')
                            {
                                bracketCounter--;
                            }
                            prgPos++;
                        }
                    }
                    else
                    {
                        prgPos++;
                    }
                }
                // This ends the loop in bf
                // This is exactly the same procedure as [ but in reverse
                // Again the point is to calculate whether the [ we encounter is the pair of the ] we started from
                else if (prg[prgPos] == ']')
                {
                   if (tape[tapePos] != 0) 
                   {
                       prgPos--;
                       while (prgPos >= 0)
                       {
                           if (prg[prgPos] == '[' && bracketCounter == 0)
                           {
                               break;
                           }
                           else if (prg[prgPos] == ']')
                           {
                               bracketCounter++;
                           }
                           else if (prg[prgPos] == '[')
                           {
                               bracketCounter--;
                           }
                           prgPos--;
                       }
                   }
                   else
                   {
                       prgPos++;
                   }
                }
                else
                {
                    prgPos++;
                }
            }
            Console.WriteLine();
        }
    }
}
