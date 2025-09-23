public class Player
{
    public string Name { get; }
    public List<Card> playerHand { get; } = new List<Card>();
    public int playerPoints { get; set; } = 0;

    public Player(string name)
    {
        Name = name;
    }

    public void DrawCard(Deck deck)
    {
        var card = deck.Draw();
        if (card != null) playerHand.Add(card);
    }
}