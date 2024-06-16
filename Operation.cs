public class Operation {
    public string Register { get; set; }
    public string Action { get; set; }
    public string Value { get; set; }

    public Operation(string register, string action,string value )
    {
        Register = register;
        Action = action;
        Value = value;
    }
}