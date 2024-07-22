using System;
using System.Threading;
using System.Threading.Tasks;

class ContinuationDemo
{
    // Тапсырма ретінде орындалатын әдіс
    static void MyTask()
    {
        Console.WriteLine("MyTask() иске косылды");
        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("MyTask() адисиндеги санауыш мани: " + count);
        }
        Console.WriteLine("MyTask аякталды");
    }
    // Тапсырманың жалғасы ретінде орындалатын әдіс.
    static void ContTask(Task t)
    {
        Console.WriteLine("Жалгасы иске косылды");
        for (int count = 0; count < 5; count++)
        {
            Thread.Sleep(500);
            Console.WriteLine("Жалгасында санауыш мани:  " + count);
        }
        Console.WriteLine("Жалгасы аякталды");
    }

    static void Main()
    {
        Console.WriteLine("Негизги агын иске косылды.");
        // Алғашқы тапсырманың объектісін құру
        Task tsk = new Task(MyTask);
        // Тапсырманың жалғасын құру
        Task taskCont = tsk.ContinueWith(ContTask);
        // Тапсырмалар тізбегін іске қосу.
        tsk.Start();
        // Жалғасының аяқталуын күту.
        taskCont.Wait();
        tsk.Dispose();
        taskCont.Dispose();
        Console.WriteLine("Негизги агын аякталды.");
    }
}


