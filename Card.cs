public abstract class Card
{
    public enum CreatureType { Santa, Owl, Frog, Fairy }
    public string Name { get; }
    public string Type { get; }

    public Card(string name, string type)
    {
        Name = name;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Name} {Type}";
    }
}