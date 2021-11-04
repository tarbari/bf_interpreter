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

            // Read a file called program.txt and import the data to prg string
            using (var extFile = new StreamReader("program.txt"))
            {
                prg = extFile.ReadToEnd();
            }

            // Defining commands
            while (prgPos < prg.Length)
            {
                if (prg[prgPos] == '<')
                {
                    tapePos--;
                    prgPos++;
                }
                else if (prg[prgPos] == '>')
                {
                    tapePos++;
                    prgPos++; 
                }
                else if (prg[prgPos] == '.')
                {
                    Console.Write(Convert.ToChar(tape[tapePos]));
                    prgPos++;
                }
                else if (prg[prgPos] == ',')
                {
                    var inp = Console.ReadKey(true);
                    tape[tapePos] = (byte)inp.KeyChar;
                    prgPos++;
                }
                else if (prg[prgPos] == '+')
                {
                    tape[tapePos]++;
                    prgPos++;
                }
                else if (prg[prgPos] == '-')
                {
                    tape[tapePos]--;
                    prgPos++;
                }
                else if (prg[prgPos] == '[')
                {

                }
                else if (prg[prgPos] == ']')
                {

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
