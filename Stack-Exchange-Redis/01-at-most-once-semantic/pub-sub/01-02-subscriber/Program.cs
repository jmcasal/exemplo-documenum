using examples.common;

using Serilog;

using StackExchange.Redis;

namespace examples.ex_01_02.suscriber;

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

            ISubscriber sub = redis.GetSubscriber();


            string channel = randomMessage.DiosEgipcio(0);

            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;

            // Cerrar la conexión


            settings.AppName = $"P-{randomMessage.DiosGriego(settings.Index)}";



            sub.Subscribe(channel, (channel, message) =>
            {
                Console.WriteLine($"{settings.AppName}: {message}".K(ConsoleColor.DarkRed));
            });


            int iteration = 0;
            do
            {

                Console.WriteLine($"Iteracion {iteration++}".K(ConsoleColor.DarkGreen));

                Thread.Sleep(10 * randomMessage.Next(0, 150));


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
