
public class TreeTop : Card
{
    public Dictionary<CreatureType, int> Creatures { get; }

    public TreeTop(string name, string type, Dictionary<CreatureType, int> creatures) : base(name, type)
    {
        Creatures = creatures;
    }

}

public class TreeTrunk : Card
{
    public Dictionary<CreatureType, int> Creatures { get; }

    public TreeTrunk(string name, string type, Dictionary<CreatureType, int> creatures) : base(name, type)
    {
        Creatures = creatures;
    }
}

//Jokerkort lägger till en redan existerande varelse
public class JokerCard : Card
{
    public int Value { get; }

    public JokerCard(string name, string type, int value) : base(name, type)
    {
        Value = value;
    }
}

// Wolfkort tar bort en typ av varelser från en stam
public class WolfCard : Card
{
    public int Value { get; }

    public WolfCard(string name, string type, int value) : base(name, type)
    {
        Value = value;
    }
}
