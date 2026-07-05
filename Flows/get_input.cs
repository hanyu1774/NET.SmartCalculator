namespace ConsoleApp.Calculation.Flows;

internal sealed class GetInput
{
    public string run()
    {
        Console.Write("Input: ");
        return Console.ReadLine() ?? string.Empty;
    }
}
