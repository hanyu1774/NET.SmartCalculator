# NET.SmartCalculator
This is a console application, where you can do any advanced calculation. It mirrors the functionalities of the smart calculator, when you type math expressions into the Google search bar. Said smart calculator understands most common inputs and expressions (e.g. `33+(6(28^2-0.55)^2)`) and calculates them. Guess what, NET.SmartCalculator can do that, too.

## What this application doesn't do (yet?)
You may know the TI-84 (CE) programmable calculator and its capabilities. NET.SmartCalculator doesn't have nor can it calculate vectors, interferences, etc.. NET.SmartCalculator doesn't show any graphs, either.
It would be very interesting, if I were to implement functionalities I mentioned in the future. Who knows? Maybe I will do that?

## Small hint...
This application makes use of RGB colors (via ASCII escape notations) for certain messages. See `models/foreground_color.cs`, where colors are set:

```c#
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
    public const string Reset = "\x1b[0;39m"; 
}
```
