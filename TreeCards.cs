using System.Collections.Generic;

public class TreeTop : Card
{
    public List<string> Creatures { get; }

    public TreeTop(List<string> creatures) 
        : base("Tree Top", "Tree")
    {
        Creatures = creatures;
    }

    public override string ToString()
    {
        return $"{Name} ({Type}) - [{string.Join(", ", Creatures)}]";
    }
}

public class TreeTrunk : Card
{
    public List<string> Creatures { get; }

    public TreeTrunk(List<string> creatures)
        : base("Tree Trunk", "Tree")
    {
        Creatures = creatures;
    }

    public override string ToString()
    {
        return $"{Name} ({Type}) - [{string.Join(", ", Creatures)}]";
    }
}
