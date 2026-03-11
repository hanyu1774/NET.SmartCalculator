using ConsoleApp.Calculation.Flows;
using ConsoleApp.Calculation.Models;

namespace ConsoleApp.Calculation.Workflows;

public sealed class Workflow
{
    public void run()
    {
        GetInput            get_input            = new();
        NormalizeExpression normalize_expression = new();
        TokenizeExpression  tokenize_expression  = new();
        EvaluateExpression  evaluate_expression  = new();
        FormatResult        format_result        = new();
        
        Console.WriteLine("Taschenrechner  |  'exit' zum Beenden");
        Console.WriteLine("--------------------------------------");

        while (true)
        {
            string raw = get_input.run();

            if (raw.Equals("exit", StringComparison.OrdinalIgnoreCase) ||
                raw.Equals("quit", StringComparison.OrdinalIgnoreCase))
                break;

            if (string.IsNullOrWhiteSpace(raw)) continue;

            string normalized = normalize_expression.run(raw);

            ExpressionContext ctx = new()
            {
                raw_input        = raw,
                normalized_input = normalized,
                tokens           = tokenize_expression.run(normalized)
            };

            double  value     = evaluate_expression.run(ctx);
            string  formatted = format_result.run(value);

            Console.WriteLine($"= {formatted}");
            Console.WriteLine();
        }
    }
}
