using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

//class Student
//{
//    public int Id { get; set; }
//    public int Points { get; set; }

//    public static List<Student> GenerateRandomStudents(int count)
//    {
//        var rand = new Random();
//        var students = new List<Student>();
//        for (int i = 1; i <= count; i++)
//        {
//            students.Add(new Student
//            {
//                Id = i,
//                Points = rand.Next(60, 101)
//            });
//        }
//        return students;
//    }
//}

//class Program
//{
//    static void Main()
//    {
//        var threads = new Thread[5];
//        for (int i = 0; i < threads.Length; i++)
//        {
//            threads[i] = new Thread(GroupThread);
//            threads[i].Start(i + 1);
//        }

//        foreach (var thread in threads)
//        {
//            thread.Join();
//        }
//    }

//    static void GroupThread(object groupIdObject)
//    {
//        var groupId = (int)groupIdObject;
//        var students = Student.GenerateRandomStudents(8);

//        var topStudents = students.Where(s => s.Points > 90).ToList();
//        Console.WriteLine($"Students with points higher than 90 in group {groupId}:");
//        foreach (var student in topStudents)
//        {

//            Console.WriteLine($"Student with highest score:");
//            Console.WriteLine($"Group {groupId}: Student {student.Id} scored {student.Points}");
//            Console.WriteLine($"Student {student.Id} scored {student.Points}");
//        }

//        var avgPoints = students.Average(s => s.Points);
//        Console.WriteLine($"Average points for group {groupId}: {avgPoints}");
//        lock (Program.bestGroupLock)
//        {
//            if (avgPoints > Program.bestGroupAvgPoints)
//            {
//                Program.bestGroupId = groupId;
//                Program.bestGroupAvgPoints = avgPoints;
//            }
//        }
//    }
//    static int bestGroupId;
//    static double bestGroupAvgPoints;
//    static object bestGroupLock = new object();
//}

using System.ComponentModel;

class Program
{
    static BackgroundWorker bw;

    static void Main()
    {
        bw = new BackgroundWorker();
        bw.WorkerReportsProgress = true;
        bw.WorkerSupportsCancellation = true;
        bw.DoWork += bw_DoWork;
        bw.ProgressChanged += bw_ProgressChanged;
        bw.RunWorkerCompleted += bw_RunWorkerCompleted;

        bw.RunWorkerAsync(null);

        Console.WriteLine(
          "Нажмите Enter в течении следующих пяти секунд, чтобы прервать работу");
        Console.ReadLine();

        if (bw.IsBusy)
        {
            bw.CancelAsync();
            Console.ReadLine();
        }
    }

    static void bw_DoWork(object sender, DoWorkEventArgs e)
    {
        for (int i = 0; i <= 100; i += 20)
        {
            if (bw.CancellationPending)
            {
                e.Cancel = true;
                return;
            }

            bw.ReportProgress(i);
            Thread.Sleep(1000);
        }

        e.Result = 123;    // будет передано в RunWorkerComрleted
    }

    static void bw_RunWorkerCompleted(object sender,
      RunWorkerCompletedEventArgs e)
    {
        if (e.Cancelled)
            Console.WriteLine(
              "Работа BackgroundWorker была прервана пользователем!");
        else if (e.Error != null)
            Console.WriteLine("Worker exception: " + e.Error);
        else
            Console.WriteLine("Работа закончена успешно. Результат - "
              + e.Result + ". ");

        Console.WriteLine("Нажмите Enter для выхода из программы...");
    }

    static void bw_ProgressChanged(object sender,
      ProgressChangedEventArgs e)
    {
        Console.WriteLine("Обработано " + e.ProgressPercentage + "%");
    }
}