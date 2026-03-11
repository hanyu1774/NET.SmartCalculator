using ConsoleApp.Calculation.Models;
namespace ConsoleApp.Calculation.Flows;

internal sealed class TokenizeExpression 
{
    private static class Expression
    {
        public static readonly string[] functions = 
        [
            "log10", "log2", "sinh", "cosh", "tanh",
            "asin", "acos", "atan", "sqrt", "cbrt",
            "ceil", "floor", "round", "sign",
            "sin", "cos", "tan", "log", "ln",
            "abs", "exp"
        ];

        public static readonly string[] constants =
        [
            "tau", "phi", "pi", "e"
        ];
    }

    public List<Token> run(string input)
    {
        if (string.IsNullOrEmpty(input)) 
        {
            return [];
        }

        List<Token> tokens = new List<Token>(input.Length / 2 + 2);
        int position    = 0;

        while (position < input.Length)
        {
            if (char.IsDigit(input[position]) || input[position] == '.')
            {
                tokens.Add(read_number(input, ref position));
                continue;
            }

            if (char.IsLetter(input[position]))
            {
                Token? word = try_read_word(input, ref position);
                if (word is not null) { tokens.Add(word); continue; }
            }

            TokenType type = input[position] switch
            {
                '+' or '-' or '*' or '/' or '^' or '%' => TokenType.Operator,
                '(' => TokenType.LeftParen,
                ')' => TokenType.RightParen,
                _   => TokenType.None
            };

            if (type == TokenType.None) { position++; continue; }

            tokens.Add(new Token
            {
                token_type  = type,
                token_value = input[position].ToString(),
                position    = position
            });
            position++;
        }
        return tokens;
    }

    private static Token read_number(string input, ref int position)
    {
        int start = position;
        while (position < input.Length && (char.IsDigit(input[position]) || input[position] == '.'))
            position++;

        if (position < input.Length && (input[position] == 'e' || input[position] == 'E'))
        {
            int look = position + 1;
            if (look < input.Length &&
                (char.IsDigit(input[look]) || input[look] == '+' || input[look] == '-'))
            {
                position++;
                if (input[position] == '+' || input[position] == '-') position++;
                while (position < input.Length && char.IsDigit(input[position])) position++;
            }
        }

        return new Token
        {
            token_type  = TokenType.Number,
            token_value = input[start..position],
            position    = start
        };
    }

    private static Token? try_read_word(string input, ref int position)
    {
        foreach (string function in Expression.functions)
        {
            if (input.AsSpan(position).StartsWith(function, StringComparison.OrdinalIgnoreCase))
            {
                int end = position + function.Length;
                if (end >= input.Length || !char.IsLetterOrDigit(input[end]))
                {
                    int start = position; position = end;
                    return new Token { token_type = TokenType.Function,
                                       token_value = function, position = start };
                }
            }
        }

        foreach (string constant in Expression.constants)
        {
            if (input.AsSpan(position).StartsWith(constant, StringComparison.OrdinalIgnoreCase))
            {
                int end = position + constant.Length;
                if (end >= input.Length || !char.IsLetterOrDigit(input[end]))
                {
                    int start = position; position = end;
                    return new Token 
                    { 
                        token_type = TokenType.Constant,
                        token_value = constant, position = start 
                    };
                }
            }
        }

        position++;
        return null;
    }
}
