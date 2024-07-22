using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace thread_3lek
{
    internal class ThreadTest
    {
        bool done;

        static void Main()
        {
            ThreadTest tt = new ThreadTest(); // Создаем общий объект
            new Thread(tt.Go).Start();
            tt.Go();
        }

        void Go()
        {
            if (!done) { done = true; Console.WriteLine("Done"); }
        }


    }
}
