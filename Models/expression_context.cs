namespace ConsoleApp.Calculation.Models;

public sealed class ExpressionContext
{
    public string raw_input = string.Empty;
    public string normalized_input = string.Empty;
    public List<Token> tokens = [];
    public int position = 0;
    public bool has_error = false;
    public string error_message = string.Empty;
}

public enum TokenType 
{
    None,
    Number, 
    Operator,
    UnaryMinus,
    LeftParen,
    RightParen,
    Function,
    Constant,
    Unknown
}

public sealed class Token 
{
    public TokenType token_type;
    public string token_value = string.Empty;
    public int position = 0;
    //public string format_token => $"[{token_type}: '{token_value}' @{position}]"; // for debugging
}
