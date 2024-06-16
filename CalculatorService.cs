namespace Calculator
{
    public class CalculatorService
    {
        private Dictionary<string, double> registers;
        private List<Operation> operations;

        public CalculatorService()
        {
            registers = new Dictionary<string, double>();
            operations = new List<Operation>();
        }

        public void ProcessInput(string register, string action = "", string value = "")
        {
            if(register == "quit") {
                Environment.Exit(0);
            }
            else if (action == "print")
            {
                ExecuteOperations();
                if (registers.ContainsKey(register))
                {
                    Console.WriteLine($"{register}: {registers[register]}");
                }
            }
            else
            {
                operations.Add(new Operation(register, action, value));
            }
        }

        private void ExecuteOperations()
        {
            // Sort operations: numeric values first, then registers
            // Only works in the current form. Needs to be revised if the order changes
            var sortedOperations = operations
                .OrderBy(op => !double.TryParse(op.Value, out _))
                .ThenBy(op => op.Register)
                .ToList();

            foreach (Operation op in sortedOperations)
            {
                double numericValue;

                // Check if the value is a register
                if (registers.ContainsKey(op.Value))
                {
                    numericValue = registers[op.Value];
                }
                else if (double.TryParse(op.Value, out numericValue))
                {
                    // Do nothing
                }

                if (!registers.ContainsKey(op.Register))
                {
                    registers[op.Register] = 0;
                }

                switch (op.Action)
                {
                    case "add":
                        registers[op.Register] += numericValue;
                        break;
                    case "subtract":
                        registers[op.Register] -= numericValue;
                        break;
                    case "multiply":
                        registers[op.Register] *= numericValue;
                        break;
                    default:
                        Console.WriteLine($"Invalid action {op.Action} provided for {op.Register}.");
                        break;
                }
            }
            operations.Clear();
        }

        public double GetRegisterValue(string register)
        {
            return registers.ContainsKey(register) ? registers[register] : 0;
        }
    }

}