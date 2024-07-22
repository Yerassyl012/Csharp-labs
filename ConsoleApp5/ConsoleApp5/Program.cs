using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;

public interface IScheduler
{
    void Schedule(Action action);
}

public class Broker
{
    public string Name { get; set; }
    private IObservable<string> notificationStream;

    public Broker(string name)
    {
        Name = name;
        notificationStream = CreateNotificationStream().ObserveOn(ThreadPoolScheduler.Instance);
    }

    public void StartNotifications()
    {
        notificationStream.Subscribe(notification =>
        {
            Console.WriteLine($"[{Name}] {notification}");
            // Другая логика обработки уведомлений
        });
    }

    private IObservable<string> CreateNotificationStream()
    {
        // Вместо имитации потока данных, в реальном сценарии вы можете использовать реальный источник данных
        return Observable.Interval(TimeSpan.FromSeconds(1))
            .Select(_ => $"New notification from {Name}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        Broker broker1 = new Broker("Broker1");
        Broker broker2 = new Broker("Broker2");

        broker1.StartNotifications();
        broker2.StartNotifications();

        Console.ReadLine(); // Чтобы программа не завершалась сразу
    }
}
