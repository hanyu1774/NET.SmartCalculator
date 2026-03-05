namespace ConsoleApp.Calculation.Models;

public sealed class CalculatorResult 
{
    public decimal input_value = 0;
    public string formatted_value = string.Empty;
    public bool is_error = false;
    public string error_message = string.Empty;
    public string normalized_expression = string.Empty;
}
