namespace ConsoleApp.Calculation.Models;

public readonly struct ForegroundColor 
{
    // Explaination:
    // ASCII escape notations are used here.
    // See also: https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
    //
    // Terminals will see them as commands for certain actions, e.g. `\n` => new line
    // \x1b[ => 'ESC'
    // 0 or 1 => either 'normal' or 'bold'. If neither are used, then either
    // the font weight default (used by the terminal itself), a previous
    // setting (if the setting within the runtime wasn't reverted) is used or 'normal'.
    // 38 => foreground color
    // 2 => make use of actual RGB values
    // 'm' => end of instruction

    public const string Red = "\x1b[1;38;2;251;52;117m"; // RGB(251, 52, 117)
    public const string Green = "\x1b[38;2;138;226;52m"; // RGB(138, 226, 52)
    public const string Yellow = "\x1b[38;2;255;223;0m"; // RGB(255, 223, 0)
    public const string LightBlue = "\x1b[38;2;137;196;255m"; // RGB(137, 196, 255)
    public const string Reset = "\x1b[0;39m"; // Resets the font weight and foreground color
}
