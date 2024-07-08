using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace examples.common;

public static class ColorExtensions
{

    static string ToHex(this ConsoleColor color)
    {
        return color switch
        {
            ConsoleColor.Black => "#000000",
            ConsoleColor.DarkBlue => "#000080",
            ConsoleColor.DarkGreen => "#008000",
            ConsoleColor.DarkCyan => "#008080",
            ConsoleColor.DarkRed => "#800000",
            ConsoleColor.DarkMagenta => "#800080",
            ConsoleColor.DarkYellow => "#808000",
            ConsoleColor.Gray => "#C0C0C0",
            ConsoleColor.DarkGray => "#808080",
            ConsoleColor.Blue => "#0000FF",
            ConsoleColor.Green => "#00FF00",
            ConsoleColor.Cyan => "#00FFFF",
            ConsoleColor.Red => "#FF0000",
            ConsoleColor.Magenta => "#FF00FF",
            ConsoleColor.Yellow => "#FFFF00",
            ConsoleColor.White => "#FFFFFF",
            _ => throw new ArgumentException("Invalid color", nameof(color))
        };
    }

    static string ToHex(this string color)
    {
        return color switch
        {
            "Black" => "#000000",
            "DarkBlue" => "#000080",
            "DarkGreen" => "#008000",
            "DarkCyan" => "#008080",
            "DarkRed" => "#800000",
            "DarkMagenta" => "#800080",
            "DarkYellow" => "#808000",
            "Gray" => "#C0C0C0",
            "DarkGray" => "#808080",
            "Blue" => "#0000FF",
            "Green" => "#00FF00",
            "Cyan" => "#00FFFF",
            "Red" => "#FF0000",
            "Magenta" => "#FF00FF",
            "Yellow" => "#FFFF00",
            "White" => "#FFFFFF",
            _ => color, //throw new ArgumentException("Invalid color", nameof(color))
        };

    }


    public static string K(this string? source, string color) => Pastel.ConsoleExtensions.Pastel(Convert.ToString(source), color.ToHex());
    public static string K(this string? source, ConsoleColor color) => Pastel.ConsoleExtensions.Pastel(source, color);

    public static string KBg(this string source, string color) => Pastel.ConsoleExtensions.PastelBg(source, color);
    public static string KBg(this string source, ConsoleColor color) => Pastel.ConsoleExtensions.PastelBg(source, color);

}
public static class K
{
    public static string Black => "#000000";
    public static string DarkBlue => "#000080";
    public static string DarkGreen => "#008000";
    public static string DarkCyan => "#008080";
    public static string DarkRed => "#800000";
    public static string DarkMagenta => "#800080";
    public static string DarkYellow => "#808000";
    public static string Gray => "#C0C0C0";
    public static string DarkGray => "#808080";
    public static string Blue => "#0000FF";
    public static string Green => "#00FF00";
    public static string Cyan => "#00FFFF";
    public static string Red => "#FF0000";
    public static string Magenta => "#FF00FF";
    public static string Yellow => "#FFFF00";
    public static string White => "#FFFFFF";
}
