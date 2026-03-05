namespace ConsoleApp.Calculation.Flows;

internal sealed class FormatResult 
{
    private sealed class OutputMessage 
    {
        public readonly string not_a_number = "Not a number";
        public readonly string positive_infinity = "∞";
        public readonly string negative_infinity = "-∞";
    }

    public string run(double input_value) 
    {
        OutputMessage output_message = new();
        if(double.IsNaN(input_value)) return output_message.not_a_number;
        if(double.IsPositiveInfinity(input_value)) return output_message.positive_infinity;
        if(double.IsNegativeInfinity(input_value)) return output_message.negative_infinity;
        
        double absolute_value = Math.Abs(input_value);
        if(absolute_value != 0 && (absolute_value >= 1e15 || absolute_value < 1e-9)) 
        {
            string scientific_notification_value = input_value.ToString("G15", System.Globalization.CultureInfo.InvariantCulture);
            scientific_notification_value = CalculatorRegex.scientific_notation_exponent()
                .Replace(scientific_notification_value, match => $"E{match.Groups[1].Value}");
            return scientific_notification_value;
        }
        string formatted_decimal_value = input_value.ToString("G14", System.Globalization.CultureInfo.InvariantCulture);
        formatted_decimal_value = CalculatorRegex.trailing_decimal_zeroes().Replace(formatted_decimal_value, "$1");
        return formatted_decimal_value;
    }
}
