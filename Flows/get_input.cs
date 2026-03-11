namespace ConsoleApp.Calculation.Flows;

internal sealed class GetInput
{
    public string run()
    {
        Console.Write("Eingabe: ");
        return Console.ReadLine() ?? string.Empty;
    }
}
