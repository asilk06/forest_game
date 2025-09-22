public class Player
{
    public string Name { get; }
    public List<Card> playerHand { get; } = new List<Card>();
    public List<Card> cardsCollected { get; } = new List<Card>();

    public Player(string name)
    {
        Name = name;
    }

    public void DrawCard(Deck deck)
    {
        var card = deck.Draw();
    }
}