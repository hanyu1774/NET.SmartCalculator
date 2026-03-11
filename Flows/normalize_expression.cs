namespace ConsoleApp.Calculation.Flows;

internal sealed class NormalizeExpression
{
    public string run(string input) 
    {
        if(string.IsNullOrWhiteSpace(input)) 
        {
            return string.Empty;
        }

        string result = input;
        result = result.Replace('\u2212', '-');
        result = result.Replace('\u00D7', '*'); // × Unicode times
        result = result.Replace('\u00F7', '/'); // ÷ Unicode divide
        result = result.Replace("²", "^2");
        result = result.Replace("³", "^3");
        result = result.Replace(',', '.');       // German decimal comma
        result = result.Replace("--", "+");
        result = result.Replace("+-", "-");
        result = result.Replace("-+", "-");
        result = remove_whitespace(result);

        return result;
    }

    private static string remove_whitespace(string input) 
    {
        char[] buffer = new char[input.Length];
        int write = 0;
        for(int i = 0; i < input.Length; i++) 
        {
            if(!char.IsWhiteSpace(input[i])) 
            {
                buffer[write++] = input[i];
            }
        }
        return new string(buffer, 0, write);
    }
 
}
