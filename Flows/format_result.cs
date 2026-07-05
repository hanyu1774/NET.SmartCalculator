using System.Text.RegularExpressions;

namespace ConsoleApp.Calculation.Flows;

internal sealed partial class FormatResult
{

    [GeneratedRegex(@"[eE]\+?0*(\d+)$")]
    private static partial Regex SciNotificationRegex();

    [GeneratedRegex(@"(\.\d*?)0+$|\.0*$")]
    private static partial Regex TrailingZeroesRegex();

    public string run(double value)
    {
        if (double.IsNaN(value)) 
        {
            return "Not a number";
        }
        if (double.IsPositiveInfinity(value)) 
        { 
            return "∞";
        }
        if (double.IsNegativeInfinity(value)) 
        { 
            return "-∞";
        }

        double abs = Math.Abs(value);

        if (abs != 0 && (abs >= 1e15 || abs < 1e-9))
        {
            string sci = value.ToString("G15",
                System.Globalization.CultureInfo.InvariantCulture);
            return SciNotificationRegex().Replace(sci, m => $"E{m.Groups[1].Value}");
        }

        string result = value.ToString("G14",
            System.Globalization.CultureInfo.InvariantCulture);
        return TrailingZeroesRegex().Replace(result, "$1");
    }
}
