using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace files
{
    internal class MyConsole
    {
        public string buf { get; set; }
        public MyConsole()
        {
            buf = Directory.GetCurrentDirectory();
        }
        public void Input(string str)
        {
            string tmp = str;
            string tmp2 = str;
            try
            {
                tmp = str.Remove(str.IndexOf(' '));
                tmp2 = str.Remove(0, str.IndexOf(' ') + 1);
            }
            catch
            {
            }
            finally
            {
                if (tmp == "cd")
                    CD(tmp2);
                else if (tmp == "dir")
                    DIR(tmp2);
                else if (tmp == "cls")
                    Console.Clear();
                else if (tmp == "del")
                    DEL(tmp2);
                else if (tmp == "copy")
                    COPY(tmp2);
                else if (tmp == "copycon")
                    COPYCON(tmp2);
                else if (tmp == "mkdir")
                    MKDIR(tmp2);
                else if (tmp == "help")
                    HELP();
                else if (tmp == "type")
                    TYPE(tmp2);
            }
        }
        private void CD(string tmp)
        {
            if (tmp == "cd")
                Console.WriteLine(buf);
            else if (Directory.Exists(tmp))
                buf = tmp;
            else
                Console.WriteLine("Incorrect path");
        }
        private void DIR(string tmp)
        {
            if (tmp == "dir")
            {
                Console.WriteLine($"Folder content {buf}\n");
                DirectoryInfo dir = new DirectoryInfo(buf);
                long size = 0;
                foreach (var d in dir.GetDirectories())
                {
                    Console.WriteLine($"{d.CreationTime} {d.Name, 30}");
                }
                foreach (var file in dir.GetFiles())
                {
                    size += file.Length;
                    Console.WriteLine($"{file.CreationTime} {file.Name, 30}");
                }
                Console.WriteLine($"{dir.GetDirectories().Length} directories");
                Console.WriteLine($"{dir.GetFiles().Length} Files {size} bytes");
            }
            else if (Directory.Exists(tmp))
            {
                Console.WriteLine($"Folder content {tmp}\n");
                DirectoryInfo dir = new DirectoryInfo(tmp);
                long size = 0;
                foreach (var d in dir.GetDirectories())
                {
                    Console.WriteLine($"{d.CreationTime} {d.Name,30}");
                }
                foreach (var file in dir.GetFiles())
                {
                    size += file.Length;
                    Console.WriteLine($"{file.CreationTime} {file.Name,30}");
                }
                Console.WriteLine($"{dir.GetDirectories().Length} directories");
                Console.WriteLine($"{dir.GetFiles().Length} Files {size} bytes");
            }
            else
                Console.WriteLine("Incorrect path");
        }
        private void DEL(string tmp)
        {
            if (File.Exists(tmp))
                File.Delete(tmp);
            else
                Console.WriteLine("Incorrect path");
        }
        private void COPY(string tmp)
        {
            if (tmp != "copy")
            {
                File.Copy(buf + "\\" + tmp.Substring(0, tmp.Length - tmp.IndexOf(' ') - 2),
                    buf + "\\" + tmp.Substring(tmp.IndexOf(' ') + 1));
                Console.WriteLine("File successfully copied");
            }
        }
        private void COPYCON(string tmp)
        {
            File.Create($@"{buf}\{tmp}");
        }
        private void TYPE(string tmp)
        {
            if (File.Exists(tmp))
                Console.WriteLine(File.ReadAllText($@"{buf}\{tmp}"));
            else
                Console.WriteLine("Incorrect path");
        }
        private void MKDIR(string tmp)
        {
             Directory.CreateDirectory(buf + "\\" + tmp);
        }
        private void HELP()
        {
            Console.WriteLine("help - Вывод справочных сведений о командах.\n" +
                "cls - Очищает содержимое экрана.\n" +
                "dir - Вывод списка файлов и подкаталогов в указанном каталоге.\n" +
                "cd - Выводит имя или изменяет текущий каталог.\n" +
                "copy - Копирование одного или нескольких файлов в другое место.\n" +
                "del - Удаление одного или нескольких файлов.\n" +
                "mkdir - Создание каталога.");
        }
    }
}
