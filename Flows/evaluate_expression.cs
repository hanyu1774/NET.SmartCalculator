using ConsoleApp.Calculation.Models;
namespace ConsoleApp.Calculation.Flows;

internal sealed class EvaluateExpression
{
    public double run(ExpressionContext context)
    {
        if (context.tokens.Count == 0) return 0;
        return parse_additive(context);
    }

    private static double parse_additive(ExpressionContext ctx)
    {
        double left = parse_multiplicative(ctx);
        while (is_op(ctx, '+') || is_op(ctx, '-'))
        {
            char op = peek(ctx).token_value[0]; consume(ctx);
            double right = parse_multiplicative(ctx);
            left = op == '+' ? left + right : left - right;
        }
        return left;
    }

    private static double parse_multiplicative(ExpressionContext ctx)
    {
        double left = parse_power(ctx);
        while (is_op(ctx, '*') || is_op(ctx, '/') || is_op(ctx, '%'))
        {
            char op = peek(ctx).token_value[0]; consume(ctx);
            double right = parse_power(ctx);
            left = op switch
            {
                '*' => left * right,
                '/' => right == 0 ? double.NaN : left / right,
                '%' => left % right,
                _   => left
            };
        }
        return left;
    }

    private static double parse_power(ExpressionContext ctx)
    {
        double b = parse_unary(ctx);
        if (!is_op(ctx, '^')) return b;
        consume(ctx);
        return Math.Pow(b, parse_power(ctx)); // right-associative
    }

    private static double parse_unary(ExpressionContext ctx)
    {
        if (is_op(ctx, '-')) { consume(ctx); return -parse_unary(ctx); }
        if (is_op(ctx, '+')) { consume(ctx); return  parse_unary(ctx); }
        return parse_primary(ctx);
    }

    private static double parse_primary(ExpressionContext ctx)
    {
        Token t = peek(ctx);

        if (t.token_type == TokenType.Number)
        {
            consume(ctx);
            return double.Parse(t.token_value,
                System.Globalization.CultureInfo.InvariantCulture);
        }

        if (t.token_type == TokenType.Constant)
        {
            consume(ctx);
            return t.token_value switch
            {
                "pi"  => Math.PI,
                "e"   => Math.E,
                "tau" => Math.Tau,
                "phi" => 1.6180339887498948482,
                _     => double.NaN
            };
        }

        if (t.token_type == TokenType.Function)
        {
            consume(ctx);
            bool paren = peek(ctx).token_type == TokenType.LeftParen;
            if (paren) consume(ctx);
            double arg = parse_additive(ctx);
            if (paren) expect(ctx, TokenType.RightParen);
            return apply(t.token_value, arg);
        }

        if (t.token_type == TokenType.LeftParen)
        {
            consume(ctx);
            double result = parse_additive(ctx);
            expect(ctx, TokenType.RightParen);
            return result;
        }

        return double.NaN;
    }

    private static Token peek(ExpressionContext ctx) =>
        ctx.position < ctx.tokens.Count
            ? ctx.tokens[ctx.position]
            : new Token { token_type = TokenType.None };

    private static void consume(ExpressionContext ctx) => ctx.position++;

    private static void expect(ExpressionContext ctx, TokenType type)
    {
        if (peek(ctx).token_type == type) consume(ctx);
    }

    private static bool is_op(ExpressionContext ctx, char op)
    {
        Token t = peek(ctx);
        return t.token_type == TokenType.Operator &&
               t.token_value.Length == 1 && t.token_value[0] == op;
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
