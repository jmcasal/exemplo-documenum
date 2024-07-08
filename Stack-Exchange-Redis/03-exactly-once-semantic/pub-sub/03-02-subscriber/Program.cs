using examples.common;

using Serilog;

using StackExchange.Redis;

using System.Collections.Generic;

namespace examples.ex_03_02.suscriber;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            var randomMessage = new RandomMessage();

            var next = 1000 + 100 * randomMessage.Next(0, 50);

            Log.Information($"Starting the Suscriber in {next} ms ".K(K.Cyan));
            Thread.Sleep(next);

            var settings = Settings.LoadFromSettings("appsettings.json");

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(settings.RedisConnectionString);
            var db = redis.GetDatabase();

            ISubscriber sub = redis.GetSubscriber();



            string channel = randomMessage.DiosEgipcio(0);
            string hashKey = randomMessage.DiosEgipcio(1);
            string queueKey = randomMessage.DiosEgipcio(2);

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;

            // Cerrar la conexión


            settings.AppName = $"P-{randomMessage.DiosGriego(settings.Index)}";



            sub.Subscribe(channel, (channel, message) =>
            {
                Console.WriteLine($"[{DateTime.Now:HHmmss.fff}] Processing {settings.AppName}: {message}".K(ConsoleColor.Yellow));
                ProcessQueue(db, hashKey, queueKey);
            });

            int iteration = 0;
            do
            {
                //Console.WriteLine("Pres Esc to exit".K(ConsoleColor.DarkYellow));
                Thread.Sleep(1000);
                ProcessQueue(db, hashKey, queueKey);

            } while (!MustExit());
            redis.Close();

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    private static void ProcessQueue(IDatabase db, string hashKey, string queueKey)
    {
        var id = db.ListRightPop(queueKey);
        if (!id.HasValue)
        {
            Console.WriteLine($"[{DateTime.Now:HHmmss.fff}] No messages to process".K(ConsoleColor.DarkGray));
            return;
        }

        var messageToProcess = db.HashGet(hashKey, id);
        Console.WriteLine($"---> [{DateTime.Now:HHmmss.fff}] {messageToProcess} <---");


    }

    private static bool MustExit()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Escape)
            {
                return true;
            }
        }
        return false;
    }


}
