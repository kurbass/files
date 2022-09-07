using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace files
{
    internal class MyConsole
    {
        List<string> History = new List<string>();

        public void Input(string str)//Этот метод принимает введённую пользователем команду и вызывает нужную функцию
        {
            string tmp = str;
            string tmp2 = str;
            if (str.IndexOf(' ') == -1)
            {
                tmp = str;
                tmp2 = str;
            }
            else
            {
                tmp = str.Remove(str.IndexOf(' '));
                tmp2 = str.Remove(0, str.IndexOf(' ') + 1);
            }

            if (tmp != "exit")
            {
                if (tmp == "cd")
                    CD(tmp2);
                else if (tmp == "cd..")
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
                    HELP(tmp2);
                else if (tmp == "type")
                    TYPE(tmp2);
                else if (tmp == "rd")
                    RD(tmp2);
                else if (tmp == "history")
                    HISTORY();
                else if (tmp == "attrib")
                    ATTRIB(tmp2);
                else if (tmp == "copyall")
                    COPYALL(tmp2);
                else if (tmp == "moveall")
                    MOVEALL(tmp2);
                else if (tmp == "movedir")
                    MOVEDIR(tmp2);
            }
            else
                Process.GetCurrentProcess().Kill();
        }

        private void CD(string tmp)//Выводит имя или изменяет текущий каталог.
        {
            if (tmp == "cd..")
                Directory.SetCurrentDirectory(Directory.GetCurrentDirectory()
                    .Substring(0, Directory.GetCurrentDirectory().LastIndexOf('\\')));
            else if (tmp == "cd")
                Console.WriteLine(Directory.GetCurrentDirectory());
            else if (Directory.Exists(tmp))
                Directory.SetCurrentDirectory(tmp);
            else
                Console.WriteLine("Incorrect path");
            History.Add("cd");
        }

        private void DIR(string tmp)//Вывод списка файлов и подкаталогов в указанном каталоге.
        {
            if (tmp == "dir")
            {
                Console.WriteLine($"Folder content {Directory.GetCurrentDirectory()}\n");
                DirectoryInfo dir = new DirectoryInfo(Directory.GetCurrentDirectory());
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
            History.Add("dir");
        }

        private void DEL(string tmp)//Удаление файла.
        {
            if (File.Exists(tmp))
                File.Delete(tmp);
            else
                Console.WriteLine("Incorrect path");
            History.Add("del");
        }

        private void COPY(string tmp)//Копирование одного файла в другое место.
        {
            if (File.Exists(tmp.Substring(0, tmp.IndexOf(' '))) && File.Exists(tmp.Substring(tmp.IndexOf(' ') + 1)) != true)
            {
                try
                {
                    File.Copy(Directory.GetCurrentDirectory() + "\\" + tmp.Substring(0, tmp.IndexOf(' ')),
                    tmp.Substring(tmp.IndexOf(' ') + 1));
                    Console.WriteLine("File successfully copied");
                }
                catch
                {
                    Console.WriteLine("Incorrect input");
                }
            }
            else
                Console.WriteLine("Incorrect input");
            History.Add("copy");
        }

        private void COPYCON(string tmp)//Создание файла.
        {
            File.Create(tmp);
            History.Add("copycon");
        }

        private void TYPE(string tmp)//Вывод содержимого файла.
        {
            if (File.Exists(tmp))
                Console.WriteLine(File.ReadAllText(tmp));
            else
                Console.WriteLine("Incorrect path");
            History.Add("type");
        }

        private void MKDIR(string tmp)//Создание каталога.
        {
             Directory.CreateDirectory(tmp);
             History.Add("mkdir");
        }

        private void RD(string tmp)//Удаление каталога.
        {
            if (Directory.Exists(tmp))
            {
                DirectoryInfo dir = new DirectoryInfo(tmp);
                if (dir.GetDirectories().Any() || dir.GetFiles().Any())
                {
                    foreach (var file in dir.GetFiles())
                    {
                        file.Delete();
                    }
                    foreach (var d in dir.GetDirectories())
                    {
                        RD(tmp + "\\" + d.Name);
                    }
                }
                dir.Delete();
            }
            else
                Console.WriteLine("Incorrect path");
            History.Add("rd");
        }

        private void HISTORY()//Вывод истории введенных команд.
        {
            foreach (var item in History)
            {
                Console.WriteLine(item);
            }
            History.Add("history");
        }

        private void ATTRIB(string tmp)//Вывод атрибутов файла.
        {
            if (File.Exists(tmp))
            {
                FileInfo f = new FileInfo(tmp);
                Console.WriteLine($"Name: {f.Name}\nSize: {f.Length} Bytes\nExtension: {f.Extension}\n" +
                    $"Creation Time: {f.CreationTime}\nLast Write Time: {f.LastWriteTime}");
            }
            else
                Console.WriteLine("Incorrect path");
            History.Add("attrib");
        }

        private void COPYALL(string tmp)//Копирует все файлы из текущего каталога.
        {
            if (Directory.Exists(tmp))
            {
                int i = 1;
                foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory()))
                {
                    File.Copy(item, tmp + '\\' + item.Substring(item.LastIndexOf('\\')));
                    i++;
                }
            }
            else
                Console.WriteLine("Incorrect path");
            History.Add("copyall");
        }

        private void MOVEALL(string tmp)//Перемещает все файлы из текущего каталога.
        {
            if (Directory.Exists(tmp))
            {
                int i = 1;
                foreach (var item in Directory.GetFiles(Directory.GetCurrentDirectory()))
                {
                    File.Move(item, tmp + '\\' + item.Substring(item.LastIndexOf('\\')));
                    i++;
                }
            }
            else
                Console.WriteLine("Incorrect path");
            History.Add("moveall");
        }

        private void MOVEDIR(string tmp)//Перемещает заданный каталог.
        {
            if (Directory.Exists(tmp.Substring(0, tmp.LastIndexOf(' '))) &&
                Directory.Exists(tmp.Substring(tmp.LastIndexOf(' ') + 1, tmp.LastIndexOf('\\') - tmp.LastIndexOf(' ') - 1)))
                Directory.Move(tmp.Substring(0, tmp.LastIndexOf(' ')),
                    tmp.Substring(tmp.LastIndexOf(' ') + 1));
            else
                Console.WriteLine("Incorrect path");
            History.Add("movedir");
        }

        private void HELP(string tmp)//Вывод справочных сведений о командах.
        {
            if (tmp == "help")
            {
                Console.WriteLine("help - Вывод справочных сведений о командах.\n" +
                "cls - Очищает содержимое экрана.\n" +
                "dir - Вывод списка файлов и подкаталогов в указанном каталоге.\n" +
                "cd - Выводит имя или изменяет текущий каталог.\n" +
                "copy - Копирование одного файла в другое место.\n" +
                "del - Удаление файла.\n" +
                "mkdir - Создание каталога.\n" +
                "copycon - Создание файла.\n" +
                "type - Вывод содержимого файла.\n" +
                "rd - Удаление каталога.\n" +
                "history - Вывод истории введенных команд.\n" +
                "copyall - Копирует все файлы из текущего каталога.\n" +
                "moveall - Перемещает все файлы из текущего каталога.\n" +
                "movedir - Перемещает заданный каталог.\n" +
                "exit - Завершение работы.\n");
            }
            else if (tmp == "cls")
                Console.WriteLine("cls - Очищает содержимое экрана.");
            else if (tmp == "dir")
                Console.WriteLine("dir - Вывод списка файлов и подкаталогов в указанном каталоге.\n" +
                    "dir - Вывод информацию в текущем каталоге.\n" +
                    "dir [путь] - Вывод информацию в введённом пути.\n");
            else if (tmp == "cd")
                Console.WriteLine("cd - Выводит имя или изменяет текущий каталог.\n" +
                    "cd - Вывод имени текущего каталога.\n" +
                    "cd.. - Сделать шаг назад в каталоге.\n" +
                    "cd [путь] - Сделать текущий каталог равным [путь].\n");
            else if (tmp == "copy")
                Console.WriteLine("copy - Копирование одного файла в другое место.\n" +
                    "copy [имя файла из текущего каталога] [полный путь к файлу] - Копирует файл из текущего каталога.\n");
            else if (tmp == "del")
                Console.WriteLine("del - Удаление файла.\n" +
                    "del [имя файла] - Удаляет файл из текущей директории.\n" +
                    "del [полный путь к файлу] - Удаляет файл.\n");
            else if (tmp == "mkdir")
                Console.WriteLine("mkdir - Создание каталога.\n" +
                    "mkdir [имя каталога] - Создаёт каталог в текущем каталоге.\n" +
                    "mkdir [полный путь к каталогу] - Создаёт каталог.\n");
            else if (tmp == "copycon")
                Console.WriteLine("copycon - Создание файла.\n" +
                    "copycon [имя файла] - Создание файла в текущем каталоге\n" +
                    "copycon [полный путь к файлу] - Создает файл.\n");
            else if (tmp == "type")
                Console.WriteLine("type - Вывод содержимого файла.\n" +
                    "type [имя файла] - Вывод содержимого файла в текущем каталоге.\n" +
                    "type [полный путь к файлу] - Вывод содержимого файла.\n");
            else if (tmp == "rd")
                Console.WriteLine("rd - Удаление каталога.\n" +
                    "rd [имя каталога] - Удаляет каталог в текущем каталоге.\n" +
                    "rd [полный путь к каталогу] - Удаляет каталог.\n");
            else if (tmp == "history")
                Console.WriteLine("history - Вывод истории введенных команд.\n");
            else if (tmp == "copyall")
                Console.WriteLine("copyall - Копирует все файлы из текущего каталога.\n" +
                    "copyall [полный путь к каталогу] - Копирует все файлы из текущего каталога в [полный путь к каталогу].\n");
            else if (tmp == "moveall")
                Console.WriteLine("moveall - Перемещает все файлы из текущего каталога.\n" +
                    "moveall [полный путь к каталогу] - Перемещает все файлы из текущего каталога в [полный путь к каталогу].\n");
            else if (tmp == "movedir")
                Console.WriteLine("movedir - Перемещает заданный каталог.\n" +
                    "movedir [полный путь к каталогу№1] [полный путь к каталогу№2] - Перемещает из [полный путь к каталогу№1] в [полный путь к каталогу№2].\n");
        }
    }
}
