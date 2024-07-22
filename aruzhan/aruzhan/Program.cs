using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;

namespace ConsoleSubstring
{
    class Substring
    {
        private string fileName { get; set; }
        private string[] lines { get; set; }
        public static List<Task<int>> task = new List<Task<int>>();
        public Substring(string fileName)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException("File name should not be empty");
            }
            this.fileName = fileName;
            string text = "";
            using (FileStream fs = File.Open(fileName, FileMode.Open))
            {
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding(true);
                while (fs.Read(b, 0, b.Length) > 0)
                {
                    text += temp.GetString(b);
                }
            }
            this.lines = text.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
        }
        public int Find()
        {
            string sub = "footer";
            int count = 0;
            for (int i = 0; i < this.lines.Length; i++)
            {
                int cp = i;

                if (this.lines[cp].Contains(sub))
                {
                    for (int j = 0; j < this.lines[cp].Length - sub.Length + 1; j++)
                    {
                        if (this.lines[cp].Substring(j, sub.Length).Equals(sub))
                        {
                            count++;
                            Console.WriteLine("Zhol " + (cp + 1) + " orni " + (j + 1));
                        }

                    }
                }
            }
            return count;
        }
        public void Taskkk()
        {
            task.Add(Task<int>.Factory.StartNew(Find));
            Task s = Task.WhenAll(task).ContinueWith(a =>
            {
                int max = int.MaxValue;
                for (int i = 0; i < a.Result.Length; i++)
                    if (max > a.Result[i])
                    {
                        max = a.Result[i];

                    }
                Console.WriteLine("Maksimaldy man: {0}", max);
            });
            s.Wait();
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Substring substring = new Substring(@"C:\Users\HOme\Desktop\File.txt");
            substring.Taskkk();
        }
    }
}