// See https://aka.ms/new-console-template for more information


using Calculator;

internal class Program
{

    static void Main(string[] args)
    {
        CalculatorService calculator = new CalculatorService();
        bool calcAlive = true;
        while (calcAlive)
        {
            var input = Console.ReadLine();
            var inputArray = input.Split(" ");

            string filePath = inputArray[0].Trim();
            if (File.Exists(filePath))
            {
                SendFileInput(filePath, calculator);
            }
            else
            {
                SendInput(inputArray, calculator);
            }
        }


    }

    private static void SendFileInput(string filePath, CalculatorService calculator)
    {

        string[] lines = File.ReadAllLines(filePath);
        int count = 0;
        foreach (string line in lines)
        {
            var fileInputArray = line.Split(" ");
            var caseInsensitiveArray = IgnoreCase(fileInputArray);
            SendInput(caseInsensitiveArray, calculator);
            count++;
            if(count == lines.Length) Environment.Exit(0);
        }

    }

    private static void SendInput(string[] inputArray, CalculatorService calculator)
    {
        var caseInsensitive = IgnoreCase(inputArray);
        switch (caseInsensitive.Length)
        {
            case 3:
                calculator.ProcessInput(caseInsensitive[0], caseInsensitive[1], caseInsensitive[2]);
                break;
            case 2:
                calculator.ProcessInput(caseInsensitive[1], caseInsensitive[0]);
                break;
            case 1:
                calculator.ProcessInput(caseInsensitive[0]);
                break;
            default:
                break;
        }
    }

    private static string[] IgnoreCase(string[] inputArray)
    {
        var caseInsensitive = new List<string>();
        foreach (var s in inputArray)
        {
            caseInsensitive.Add(s.ToLower().Trim());
        }
        return caseInsensitive.ToArray();
    }
}