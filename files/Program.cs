using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace files
{

    internal class Program
    {
        
        static void Main(string[] args)
        {
            /*
             * Класс помещён в бесконечный цикл.
             * Eдинственый способ завершить работу это команда exit.
            */ 
            MyConsole m = new MyConsole();
            while (true)
            {
                Console.Write(Directory.GetCurrentDirectory() + ">");
                m.Input(Console.ReadLine());
            }
        }
    }
}