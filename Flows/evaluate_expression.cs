using ConsoleApp.Calculation.Models;
namespace ConsoleApp.Calculation.Flows;

internal sealed class EvaluateExpression
{
    public double run(ExpressionContext context)
    {
        if (context.tokens.Count == 0) return 0;

        double value = parse_additive(context);

        if (context.position < context.tokens.Count && !context.has_error)
        {
            Token leftover = peek(context);
            context.has_error = true;
            context.error_message = $"Unexpected token '{leftover.token_value}' in position {leftover.position+1}";
        }

        return value;
    }

    private static double parse_additive(ExpressionContext expression_context)
    {
        double left = parse_multiplicative(expression_context);
        while (is_operator(expression_context, '+') || is_operator(expression_context, '-'))
        {
            char operator_symbol = peek(expression_context).token_value[0]; consume(expression_context);
            double right = parse_multiplicative(expression_context);
            left = operator_symbol == '+' ? left + right : left - right;
        }
        return left;
    }

    private static double parse_multiplicative(ExpressionContext expression_context)
    {
        double left = parse_unary(expression_context);
        while (is_operator(expression_context, '*') || is_operator(expression_context, '/') || is_operator(expression_context, '%'))
        {
            char op = peek(expression_context).token_value[0]; consume(expression_context);
            double right = parse_unary(expression_context);
            left = op switch
            {
                '*' => left * right,
                '/' => divide(expression_context, left, right),
                '%' => left % right,
                _   => left
            };
        }
        return left;
    }

    private static double divide(ExpressionContext expression_context, double left, double right)
    {
        if (right != 0) return left / right;

        if (!expression_context.has_error)
        {
            expression_context.has_error = true;
            expression_context.error_message = "Division by zero";
        }
        return double.NaN;
    }

    private static double parse_power(ExpressionContext expression_context)
    {
        double base_value = parse_primary(expression_context);
        if (!is_operator(expression_context, '^')) return base_value;
        consume(expression_context);
        return Math.Pow(base_value, parse_unary(expression_context)); // right-associative, permits negative exponents (2^-2)
    }

    private static double parse_unary(ExpressionContext expression_context)
    {
        if (is_operator(expression_context, '-')) { consume(expression_context); return -parse_unary(expression_context); }
        if (is_operator(expression_context, '+')) { consume(expression_context); return  parse_unary(expression_context); }
        return parse_power(expression_context);
    }

    private static double parse_primary(ExpressionContext expression_context)
    {
        Token token = peek(expression_context);

        if (token.token_type == TokenType.Number)
        {
            consume(expression_context);
            if (double.TryParse(token.token_value,
                System.Globalization.CultureInfo.InvariantCulture, out double number))
            {
                return number;
            }

        if (!expression_context.has_error)
        {
            expression_context.has_error = true;
            expression_context.error_message =
                $"Invalid number '{token.token_value}' in position {token.position+1}";
        }
        return double.NaN;
    }

    if (token.token_type == TokenType.Constant)
    {
        consume(expression_context);
        return token.token_value switch
        {
            "pi"  => Math.PI,
            "e"   => Math.E,
            "tau" => Math.Tau,
            "phi" => 1.6180339887498948482,
            _     => double.NaN
        };
    }

    if (token.token_type == TokenType.Function)
    {
        consume(expression_context);
        bool paren = peek(expression_context).token_type == TokenType.LeftParen;
        if (paren) consume(expression_context);
        double arg = parse_additive(expression_context);
        if (paren) expect(expression_context, TokenType.RightParen);
        return apply(token.token_value, arg);
    }

    if (token.token_type == TokenType.LeftParen)
    {
        consume(expression_context);
        double result = parse_additive(expression_context);
        expect(expression_context, TokenType.RightParen);
        return result;
    }

    if (!expression_context.has_error)
    {
        expression_context.has_error = true;
        expression_context.error_message = token.token_type == TokenType.None
            ? "Expression ends unexpectedly; a missing operand?"
            : $"Unexpected token '{token.token_value}' in position {token.position+1}";
    }
    return double.NaN;
}

    private static Token peek(ExpressionContext expression_context) =>
        expression_context.position < expression_context.tokens.Count
            ? expression_context.tokens[expression_context.position]
            : new Token { token_type = TokenType.None };

    private static void consume(ExpressionContext expression_context) => expression_context.position++;

    private static void expect(ExpressionContext expression_context, TokenType type)
    {
        if (peek(expression_context).token_type == type)
        {
            consume(expression_context);
            return;
        }

        if (!expression_context.has_error)
        {
            expression_context.has_error = true;
            expression_context.error_message =
                $"Expected: {type} in position {peek(expression_context).position+1}";
        }
    }

    private static bool is_operator(ExpressionContext expression_context, char op)
    {
        Token token = peek(expression_context);
        return token.token_type == TokenType.Operator &&
               token.token_value.Length == 1 && token.token_value[0] == op;
    }

    private static double apply(string name, double x) => name switch
    {
        "sqrt"  => Math.Sqrt(x),  "cbrt"  => Math.Cbrt(x),
        "abs"   => Math.Abs(x),   "ceil"  => Math.Ceiling(x),
        "floor" => Math.Floor(x), "round" => Math.Round(x, MidpointRounding.AwayFromZero),
        "sign"  => Math.Sign(x),  "exp"   => Math.Exp(x),
        "ln"    => Math.Log(x),   "log"   => Math.Log10(x),
        "log10" => Math.Log10(x), "log2"  => Math.Log2(x),
        "sin"   => Math.Sin(x),   "cos"   => Math.Cos(x),
        "tan"   => Math.Tan(x),   "asin"  => Math.Asin(x),
        "acos"  => Math.Acos(x),  "atan"  => Math.Atan(x),
        "sinh"  => Math.Sinh(x),  "cosh"  => Math.Cosh(x),
        "tanh"  => Math.Tanh(x),
        _       => double.NaN
    };
}
