public class Joker : Card
{
    public int Value { get; }
    public Joker(string name, string type, int value) : base(name, type)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Name} ({Type}) - Värde: {Value}";
    }
}

public class Wolf : Card
{
    public int Value { get; }
    public Wolf(string name, string type, int value) : base(name, type)
    {
        Value = value;
    }

    public override string ToString()
    {
        return $"{Name} ({Type}) - Värde: {Value}";
    }
}