using examples.common;

using Serilog;

using StackExchange.Redis;

using System.Runtime.InteropServices;

namespace examples.ex_03_01.publisher;

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
            var db = redis.GetDatabase();

            ISubscriber sub = redis.GetSubscriber();

            var randomMessage = new RandomMessage();


            string channel = randomMessage.DiosEgipcio(0);
            string hashKey = randomMessage.DiosEgipcio(1);
            string queueKey = randomMessage.DiosEgipcio(2);

            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.ForegroundColor = ConsoleColor.White;

            // Cerrar la conexión
            settings.AppName = $"P-{randomMessage.DiosRomano(settings.Index)}";

            int iteration = 0;
            do
            {

                var messageId = Guid.NewGuid().ToString();

                var message = randomMessage.GetRandomMessage(++iteration);
                message = $"{settings.AppName} - {message} - {iteration} - {messageId}".K(randomMessage.HexColor(settings.Index));
                db.HashSet(hashKey, messageId, message);
                db.ListLeftPush(queueKey, messageId, when: When.Always, CommandFlags.PreferMaster);

                // Publicar el mensaje
                sub.Publish(channel, $"Message To Process: {messageId}");

                Console.Write($"[{DateTime.Now:HHmmss.fff}] Message To Process: {messageId}.");

                Console.WriteLine($"Press Any Key to continue. [ESC] to exit.");


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
        var key = Console.ReadKey(true);
        if (key.Key == ConsoleKey.Escape)
        {
            return true;
        }

        return false;
    }


}
