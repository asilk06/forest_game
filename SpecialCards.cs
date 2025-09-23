public class Joker : Card
{
    public int Value { get; }
    public Joker(string name, string type, int value) : base(name, type)
    {
        Value = value;
    }
}

public class Wolf : Card
{
    public int Value { get; }
    public Wolf(string name, string type, int value) : base(name, type)
    {
        Value = value;
    }
}