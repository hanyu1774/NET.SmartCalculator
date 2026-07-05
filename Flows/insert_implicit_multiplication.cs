using ConsoleApp.Calculation.Models;
namespace ConsoleApp.Calculation.Flows;

internal sealed class InsertImplicitMultiplication
{
    public List<Token> run(List<Token> tokens)
    {
        if (tokens.Count < 2) return tokens;

        List<Token> result = new List<Token>(tokens.Count + 4);

        for (int i = 0; i < tokens.Count; i++)
        {
            result.Add(tokens[i]);

            if (i + 1 < tokens.Count && needs_implicit_multiply(tokens[i], tokens[i + 1]))
            {
                result.Add(new Token
                {
                    token_type  = TokenType.Operator,
                    token_value = "*",
                    position    = tokens[i + 1].position
                });
            }
        }
        return result;
    }

    private static bool needs_implicit_multiply(Token left, Token right) =>
        (left.token_type  is TokenType.Number or TokenType.RightParen or TokenType.Constant) &&
        (right.token_type is TokenType.Number or TokenType.LeftParen
                           or TokenType.Function or TokenType.Constant);
}
