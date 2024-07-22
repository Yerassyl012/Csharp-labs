using System;
using System.Threading;
using System.Collections.Generic;
class ResetEvent
{
    static AutoResetEvent are = new AutoResetEvent(false);
    static void Main()
    {
        int t = 30;
        new Thread(Method1).Start(t);
        new Thread(Method2).Start(t);
        //are.Reset();
        t = 10;
        Thread.Sleep(1000);
        Console.WriteLine(t);
        are.Set();
       // are.Reset();
        Console.WriteLine(t + 7);                                                                         

    }
    static void Method1(object t)
    {
        Console.WriteLine((int)t+9);
        are.WaitOne();
        int k = (int)t;
        k -= 21;
        Console.WriteLine(k);
    }
    static void Method2(object t)
    {
        Console.WriteLine((int)t);
        are.WaitOne();
        int k = (int)t;
        k /= 2;
        Console.WriteLine(k);
    }
}