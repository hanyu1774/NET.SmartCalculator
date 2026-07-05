using ConsoleApp.Calculation.Flows;
using ConsoleApp.Calculation.Models;

namespace ConsoleApp.Calculation.Workflows;

public sealed class Workflow
{
    public void run()
    {
        GetInput get_input = new();
        NormalizeExpression normalize_expression = new();
        TokenizeExpression tokenize_expression = new();
        InsertImplicitMultiplication insert_implicit_multiplication = new();
        EvaluateExpression evaluate_expression = new();
        FormatResult format_result = new();
        
        Console.WriteLine($"{ForegroundColor.LightBlue} Smart Calculator  |  Type 'exit' to terminate this application");
        Console.WriteLine($"-----------------------------------------------------------------{ForegroundColor.Reset}");

        while (true)
        {
            string raw = get_input.run();

            if (raw.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                raw.Equals("quit", StringComparison.OrdinalIgnoreCase)) 
            {
                break;
            }

            if (string.IsNullOrWhiteSpace(raw)) 
            { 
                continue;
            }

            string normalized = normalize_expression.run(raw);

            ExpressionContext context = new()
            {
                raw_input = raw,
                normalized_input = normalized,
                tokens = insert_implicit_multiplication.run(tokenize_expression.run(normalized))
            };

            double value = evaluate_expression.run(context);

            if (context.has_error)
            {
                Console.WriteLine($"{ForegroundColor.Red}Error: {context.error_message}{ForegroundColor.Reset}");
                Console.WriteLine();
                continue;
            }

            string formatted = format_result.run(value);

            Console.WriteLine($"={ForegroundColor.Yellow} {formatted} {ForegroundColor.Reset}");
            Console.WriteLine();
        }
    }
}
