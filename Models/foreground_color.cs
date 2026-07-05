namespace ConsoleApp.Calculation.Models;

public readonly struct ForegroundColor 
{
    public const string Red = "\x1b[1;38;2;251;52;117m";
    public const string Green = "\x1b[38;2;138;226;52m";
    public const string Yellow = "\x1b[38;2;255;223;0m";
    public const string LightBlue = "\x1b[38;2;137;196;255m";
    public const string Reset = "\x1b[0;39m";
}
