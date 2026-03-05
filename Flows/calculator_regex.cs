using System.Text.RegularExpressions;

namespace ConsoleApp.Calculation.Flows;

internal static partial class CalculatorRegex 
{
   private static class Pattern 
   {
        public const string TOKEN = @"(?<function>sqrt|cbrt|sin|cos|tan|asin|acos|atan|sinh|cosh|tanh|log10|log2|log|ln|abs|ceil|floor|round|sign|exp)" +
            @"|(?<constant>tau|phi|pi|e)" +
            @"|(?<number>[+-]?(?:\d+\.?\d*|\.\d+)(?:[eE][+-]?\d+)?)" +
            @"|(?<operator>[+\-*\/\^%])" +
            @"|(?<lparen>\()" +
            @"|(?<rparen>\))";
        public const string WHITESPACE = @"\s+";
        public const string FUNCTION_BEFORE_TOKEN = @"(\d)(\(|sqrt|cbrt|sin|cos|tan|asin|acos|atan|sinh|cosh|tanh|log10|log2|log|ln|abs|ceil|floor|round|sign|exp|tau|phi|pi|e(?![eE\d]))";
        public const string FUNCTION_INSIDE_PARENTHESIS = @"(\))(\(|\d|sqrt|cbrt|sin|cos|tan|asin|acos|atan|sinh|cosh|tanh|log10|log2|log|ln|abs|ceil|floor|round|sign|exp|tau|phi|pi|e(?![eE\d]))";
        public const string DOUBLE_MINUS = @"--";
        public const string PLUS_MINUS = @"\+-|-\+";
        public const string MINUS_UNICODE = @"\u2212";
        public const string MULTIPLY_UNICODE = @"\u00D7";
        public const string DIVIDE_UNICODE = @"\u00F7";
        public const string POWER_TWO = @"²";
        public const string POWER_THREE = @"³";
        public const string ALLOWED_CHARACTERS = @"[^0-9a-zA-Z\s\+\-\*\/\^\%\(\)\.\,]";
        public const string CONSECUTIVE_OPERATORS = @"(?<![eE])[\+\*\/\^%]{2,}|[\+\*\/\^%][\+\*\/\^%]";
        public const string TRAILING_OPERATOR = @"[\+\-\*\/\^\%]\s*$";
        public const string MALFORMED_DECIMAL = @"(?<!\d)\.(?!\d)|\.{2,}";
        public const string EMPTY_PARENTHESES = @"\(\s*\)";
        public const string TRAILING_DECIMAL_ZEROES = @"(\.\d*?)0+$|\.0*$";
        public const string SCIENTIFIC_NOTATION_EXPONENT = @"[eE]\+?0*(\d+)$";
        public const string NUMBER_LITERAL = @"^\d+(\.\d+)?([eE][+-]?\d+)?$";
   } 

    [GeneratedRegex(Pattern.TOKEN, RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    public static partial Regex tokenizer();

    [GeneratedRegex(Pattern.WHITESPACE, RegexOptions.Compiled)]
    public static partial Regex whitespace();
    
    [GeneratedRegex(Pattern.FUNCTION_BEFORE_TOKEN, RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    public static partial Regex implicit_multiply_digit_before_token();

    [GeneratedRegex(Pattern.FUNCTION_INSIDE_PARENTHESIS, RegexOptions.IgnoreCase | RegexOptions.Compiled)]
    public static partial Regex implicit_multiply_closing_paren();

    [GeneratedRegex(Pattern.DOUBLE_MINUS, RegexOptions.Compiled)]
    public static partial Regex double_minus_to_plus();

    [GeneratedRegex(Pattern.PLUS_MINUS, RegexOptions.Compiled)]
    public static partial Regex plus_minus_to_minus();

    [GeneratedRegex(Pattern.MINUS_UNICODE, RegexOptions.Compiled)]
    public static partial Regex unicode_minus();

    [GeneratedRegex(Pattern.MULTIPLY_UNICODE, RegexOptions.Compiled)]
    public static partial Regex unicode_times();

    [GeneratedRegex(Pattern.DIVIDE_UNICODE, RegexOptions.Compiled)]
    public static partial Regex unicode_divide();

    [GeneratedRegex(Pattern.POWER_TWO, RegexOptions.Compiled)]
    public static partial Regex superscript_two();

    [GeneratedRegex(Pattern.POWER_THREE, RegexOptions.Compiled)]
    public static partial Regex superscript_three();

    [GeneratedRegex(Pattern.ALLOWED_CHARACTERS, RegexOptions.Compiled)]
    public static partial Regex invalid_characters();

    [GeneratedRegex(Pattern.CONSECUTIVE_OPERATORS, RegexOptions.Compiled)]
    public static partial Regex consecutive_operators();

    [GeneratedRegex(Pattern.TRAILING_OPERATOR, RegexOptions.Compiled)]
    public static partial Regex trailing_operator();

    [GeneratedRegex(Pattern.MALFORMED_DECIMAL, RegexOptions.Compiled)]
    public static partial Regex bad_decimal_point();

    [GeneratedRegex(Pattern.EMPTY_PARENTHESES, RegexOptions.Compiled)]
    public static partial Regex empty_parentheses();

    [GeneratedRegex(Pattern.TRAILING_DECIMAL_ZEROES, RegexOptions.Compiled)]
    public static partial Regex trailing_decimal_zeroes();

    [GeneratedRegex(Pattern.SCIENTIFIC_NOTATION_EXPONENT, RegexOptions.Compiled)]
    public static partial Regex scientific_notation_exponent();

    [GeneratedRegex(Pattern.NUMBER_LITERAL, RegexOptions.Compiled)]
    public static partial Regex number_literal();
}
