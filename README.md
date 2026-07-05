# NET.SmartCalculator
This is a console application, where you can do any advanced calculation. It mirrors most functionalities of the smart calculator, when you type math expressions into the Google search bar. The smart calculator from Google understands most common inputs and expressions (e.g. `33+(6(28^2-0.55)^2)`) and calculates them. Guess what, NET.SmartCalculator can do that, too.

# Tasks
This application does almost everything as expected. There are still a few things need to done.
The following tasks weren't done yet.

* Provide a functionality to explain things via the command `help`. Examples: how to do root calculations via `sqrt()` etc..
* Easy re-use of error messages because the error messages are currently hardcoded. Make them also consistent, e.g. `Not a number` vs `Error: [...]`
* Remove some of the dead code.
* Improve the looks of TUI even more.
* A functionality to check for previous results.

## What this application doesn't do (yet?)
You may know the TI-84 (CE) programmable calculator and its capabilities. NET.SmartCalculator doesn't have special functions like the TI-84 (CE) in order to calculate or render things like vectors, interferences, etc.. NET.SmartCalculator doesn't show any graphs, either.

It would be very interesting, if I were to implement mentioned functionalities in the future. Who knows? Maybe I will do that?

## Small hint...
This application makes use of RGB colors (via ASCII escape notations) for certain messages. RGB colors are used instead of the standard 8 or 16 color codes because terminals have color profiles: either a default theme or one customized by the user. Those profiles remap the "standard" SGR (Select Graphic Rendition) color codes to whatever colors that profile defines. So, if I say "Red color for this text" using the command `\x1b[31m` in the string, the terminal will use the defined color value for red from the profile.

If your terminal makes use of a custom theme, then perhaps certain colors make certain message invisible or unreadable.
Whether or not there might be problems, you can change the colors to your liking or use the standard color codes.
For more info, check out this page: [ANSI Escape Sequences](https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797)

See `models/foreground_color.cs`, where colors are set:

```c#
public readonly struct ForegroundColor 
{
    // Explaination:
    // ASCII escape notations are used here.
    // See also: https://gist.github.com/fnky/458719343aabd01cfb17a3a4f7296797
    //
    // Terminals will see them as commands for certain actions, e.g. `\n` => new line
    // \x1b[ translates into 'ESC[' ; You can either use '\033[' or 'ESC[' etc. instead of '\x1b['
    // Any terminal will understand it as 'ESC[' either way.
    //
    // 0 or 1 => either 'normal' or 'bold'. If neither are used, then either
    // the font weight default (used by the terminal itself), a previous
    // setting (if the setting within the runtime wasn't reverted) is used or 'normal'.
    //
    // 38 => foreground color
    // 2 => make use of actual RGB values
    // 'm' => end of instruction

    public const string Red = "\x1b[1;38;2;251;52;117m"; // RGB(251, 52, 117)
    public const string Green = "\x1b[38;2;138;226;52m"; // RGB(138, 226, 52)
    public const string Yellow = "\x1b[38;2;255;223;0m"; // RGB(255, 223, 0)
    public const string LightBlue = "\x1b[38;2;137;196;255m"; // RGB(137, 196, 255)
    public const string Reset = "\x1b[0;39m"; // Resets the font weight and foreground color
}
```

If you do change the names or add new attributes, then you might have to update the following files, too:

* `flows/get_input.cs`
* `workflows/workflow.cs`

In those files `ForegroundColor` is used for the string messages. The file `flows/evaluate_expression.cs` doesn't need any color settings because error messages are saved here and used by `workflows/workflow.cs`, where the foreground color for error messages is already set to red.
