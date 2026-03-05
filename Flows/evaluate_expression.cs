using ConsoleApp.Calculation.Models;
namespace ConsoleApp.Calculation.Flows;

internal sealed class EvaluateExpression 
{
    public double run() 
    {

    }

    private double parse_addivitive() 
    {

    }

    private double parse_multiplicative() 
    {

    }

    private double parse_power() 
    {

    }

    private double parse_unary() 
    {

    }

    private double parse_primary() 
    {
        
    }

    private Token peek(ExpressionContext expression_context) 
    {
        if(expression_context.position < expression_context.tokens.Count) 
        {
            return expression_context.tokens[expression_context.position];
        }
        return new Token();
    }

    private void consume(ExpressionContext expression_context) => expression_context.position++;

    private bool expect(ExpressionContext expression_context, TokenType token_type) 
    {
        Token token = peek(expression_context);
        if(token.token_type != token_type) 
        {
            return false;
        }

        consume(expression_context);
        return true;
    }
}
