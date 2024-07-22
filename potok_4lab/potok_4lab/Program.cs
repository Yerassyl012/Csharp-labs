using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace potok_4lab
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("** Fun with Directory(Info) **\n");
            ShowWindowsDirectoryInfo();
            DisplayImageFiles();
            ModifyAppDirectory();
            FunWithDirectoryType();

            //709
            Console.WriteLine("** Fun with DriveInfo **\n");
            // Получить информацию обо всех устройствах.
            DriveInfo[] myDrives = DriveInfo.GetDrives();
            // Вывести сведения об устройствах.
            foreach (DriveInfo d in myDrives)
            {
                Console.WriteLine("Name: {0}", d.Name); //	имя
                Console.WriteLine("Type: {0}", d.DriveType);    //	тип
                                                                // Проверить, смонтировано ли устройство.
                if (d.IsReady)
                {
                    Console.WriteLine("Free space: {0}", d.TotalFreeSpace);
                    // Свободное пространство
                    Console.WriteLine("Format: {0}", d.DriveFormat);    //	формат
                    Console.WriteLine("Label: {0}", d.VolumeLabel); //	метка
                }
                Console.WriteLine();
            }
            //711


            //// Создать новый файл на диске С: .
            //FileInfo f = new FileInfo(@"C:\Test.dat");
            //FileStream fs = f.Create();
            //// Использовать объект FileStream...
            //// Закрыть файловый
            //fs.Close();

            //FileInfo f3 = new FileInfo(@"C:\Test.dat");
            //using (FileStream fs1 = f.Create())
            //{
            //    // Использовать объект FileStream..
            //}
            //// Создать новый файл через FileInfo.Open().
            //FileInfo f2 = new FileInfo(@"C:\Test2.dat");
            //using (FileStream fs2 = f2.Open(FileMode.OpenOrCreate,
            // FileAccess.ReadWrite, FileShare.None))
            //{
            //    // Использовать объект FileStream...
            //}

            ////713
            //// Получить объект FileStream с правами только для чтения.
            //FileInfo f5 = new FileInfo(@"C:\Test3.dat");
            //using (FileStream readOnlyStream = f5.OpenRead())
            //{
            //    // Использовать объект FileStream...
            //}
            //// Теперь получить объект FileStream с правами только для записи.
            //FileInfo f4 = new FileInfo(@"C:\Test4.dat");
            //using (FileStream writeOnlyStream = f4.OpenWrite())
            //{
            //    // Использовать объект FileStream...
            //}

            //// Получить объект StreamReader.
            //FileInfo f6 = new FileInfo(@"C:\boot.ini");
            //using (StreamReader sreader = f5.OpenText())
            //{
            //    // Использовать объект StreamReader...
            //}

            //FileInfo f7 = new FileInfo(@"C:\Test6.txt");
            //using (StreamWriter swriter = f6.CreateText())
            //{
            //    // Использовать объект StreamWriter...
            //}
            //FileInfo f8 = new FileInfo(@"C:\FinalTest.txt");
            //using (StreamWriter swriterAppend = f7.AppendText())
            //{
            //    // Использовать объект StreamWriter...
            //}
            //// Получить объект FileStream череэ File.Create().
            //using (FileStream fs3 = File.Create(@"C:\Test.dat"))
            //{ }
            //// Получить объект FileStream через File.Open().
            //using (FileStream fs2 = File.Open(@"C:\Test2.dat",
            //FileMode.OpenOrCreate,
            //FileAccess.ReadWrite, FileShare.None))
            //{ }
            //// Получить объект FileStream с правами только для чтения.
            //using (FileStream readOnlyStream = File.OpenRead(@"Test3.dat11"))
            //{ }
            //// Получить объект FileStream с правами только для записи.
            //using (FileStream writeOnlyStream = File.OpenWrite(@"Test4.dat"))
            //{ }
            //// Получить объект StreamReader.
            //using (StreamReader sreader = File.OpenText(@"C:\boot.ini11"))
            //{ }
            //// Получить несколько объектов StreamWriter.
            //using (StreamWriter swriter = File.CreateText(@"C:\Test6.txt"))
            //{ }
            //using (StreamWriter swriterAppend = File.AppendText(@"C:\FinalTest.txt"))
            //{ }

            Console.WriteLine("***** Fun with FileStreams *****\n");

            try
            {
                // Obtain a FileStream object.
                using (FileStream fStream = File.Open(@"myMessage.dat",
                  FileMode.Create))
                {
                    // Encode a string as an array of bytes.
                    string msg = "Hello!";
                    byte[] msgAsByteArray = Encoding.Default.GetBytes(msg);

                    // Write byte[] to file.
                    fStream.Write(msgAsByteArray, 0, msgAsByteArray.Length);

                    // Reset internal position of stream.
                    fStream.Position = 0;

                    // Read the types from file and display to console.
                    Console.Write("Your message as an array of bytes: ");
                    byte[] bytesFromFile = new byte[msgAsByteArray.Length];
                    for (int i = 0; i < msgAsByteArray.Length; i++)
                    {
                        bytesFromFile[i] = (byte)fStream.ReadByte();
                        Console.Write(bytesFromFile[i]);
                    }

                    // Display decoded messages.
                    Console.Write("\nDecoded Message: ");
                    Console.WriteLine(Encoding.Default.GetString(bytesFromFile));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();

            Console.ReadLine();

        }
        static void ShowWindowsDirectoryInfo()
        {
            // Вывести информацию о каталоге.
            DirectoryInfo dir = new DirectoryInfo(@"C:\Windows");
            Console.WriteLine("** Directory Info **");
            Console.WriteLine("FullName: {0}", dir.FullName);
            Console.WriteLine("Name: {0}", dir.Name);
            Console.WriteLine("Parent: {0}", dir.Parent);
            Console.WriteLine("Creation: {0}", dir.CreationTime);
            Console.WriteLine("Attributes: {0}", dir.Attributes);
            Console.WriteLine(" Root: {0}", dir.Root);
            Console.WriteLine("* * * * * * * * * * * * * * * * * * * * * * * * * *\n");

        }
        static void DisplayImageFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(@"C:\Windows\Web\Wallpaper");
            // Получить все файлы с расширением *.jpg.
            FileInfo[] imageFiles = dir.GetFiles("*.jpg", SearchOption.AllDirectories);
            // Сколько файлов найдено?
            Console.WriteLine("Found {0} *.jpg files\n", imageFiles.Length);
            // Вывести информацию о каждом файле.
            foreach (FileInfo f in imageFiles)
            {
                Console.WriteLine("***********");
                Console.WriteLine("File name: {0}", f.Name);    //	имя файла
                Console.WriteLine("File size: {0}", f.Length);  //	размер
                Console.WriteLine("Creation: {0}", f.CreationTime); // время создания
                Console.WriteLine("Attributes: {0}", f.Attributes); // атрибуты
                Console.WriteLine("*********\n");
            }
        }
        static void ModifyAppDirectory()
        {
            DirectoryInfo dir = new DirectoryInfo(".");
            // Создать \MyFolder в начальном каталоге.
            dir.CreateSubdirectory("MyFolder");
            // Получить возвращенный объект DirectoryInfo.
            DirectoryInfo myDataFolder = dir.CreateSubdirectory(@"MyFolder2\Data");
            // Выводит путь к ..\MyFolder2\Data.
            Console.WriteLine("New Folder is: {0}", myDataFolder);
        }
        static void FunWithDirectoryType()
        {
            // Вывести список всех дисковых устройств текущего компьютера.
            string[] drives = Directory.GetLogicalDrives();
            Console.WriteLine("Here are your drives:");
            foreach (string s in drives)
                Console.WriteLine("—> {0} ", s);
            // Удалить то, что было ранее создано.
            Console.WriteLine("Press Enter to delete directories");
            Console.ReadLine();
            try
            {
                Directory.Delete(@"C:\Users\Yerassyl Elbrus\source\repos\potok_4lab\potok_4lab\bin\Debug\MyFolder");
                // Второй параметр указывает, нужно ли удалять подкаталоги.
                Directory.Delete(@"C:\Users\Yerassyl Elbrus\source\repos\potok_4lab\potok_4lab\bin\Debug\MyFolder2", true);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}