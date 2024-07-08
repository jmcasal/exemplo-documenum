using examples.common;

using Serilog;

using StackExchange.Redis;

namespace examples.ex_01_01.publisher;

internal class Program
{
    static void Main(string[] args)
    {
        try
        {
            var settings = Settings.LoadFromSettings("appsettings.json");

            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File($"/var/q/Example-Stack-Exchange-Redis/logs/publisher/{settings.AppName}/log.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            Log.Information("Starting the Publisher".K(K.Magenta));


            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(settings.RedisConnectionString);

            ISubscriber sub = redis.GetSubscriber();

            var randomMessage = new RandomMessage();


            string channel = randomMessage.DiosEgipcio(0);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            // Cerrar la conexión




            settings.AppName = $"P-{randomMessage.DiosRomano(settings.Index)}";

            int iteration = 0;
            do
            {
                var message = randomMessage.GetRandomMessage(++iteration);
                message = $"{settings.AppName} - {message} - {iteration}".K(randomMessage.HexColor(settings.Index));
                // Publicar el mensaje

                sub.Publish(channel, message);

                var next = 1000 + 100 * randomMessage.Next(0, 50);

                Console.WriteLine($"{message} - {next} ms");

                Thread.Sleep(next);


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
