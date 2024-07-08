using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

#nullable disable

namespace examples.common;

public class RandomMessage : Random
{

    public string DiosRomano(int? index = null)
    {
        return Dioses.Romanos[(index ?? this.Next(0, Dioses.Romanos.Length)) % Dioses.Romanos.Length];
    }

    public string DiosGriego(int? index = null)
    {
        return Dioses.Griegos[(index ?? this.Next(0, Dioses.Griegos.Length)) % Dioses.Griegos.Length];
    }

    public string DiosEgipcio(int? index = null)
    {
        return Dioses.Egipcios[(index ?? this.Next(0, Dioses.Egipcios.Length)) % Dioses.Egipcios.Length];
    }

    public string HexColor(int? index = null)
    {
        return Colors[(index ?? this.Next(0, Colors.Length)) % Colors.Length];
    }

    public string GetRandomMessage(int? index)
    {
        return Messages[(index ?? this.Next(0, Messages.Count)) % Messages.Count];
    }

    List<string> _messages = null;

    List<string> Messages => _messages ??= GetRandomMessages();

    private List<string> GetRandomMessages()
    {
        var rs = new List<string>();
        for (var i = 0; i < 100; i++)
        {

            rs.Add($"Random message");
        }
        return rs;
    }

    public string[] Colors = new string[] {
            "Cyan",
            "Magenta",
            "Yellow",
            "Green",
            "Red",
            "Blue",
            "Gray",
            "White", 
    };
}
