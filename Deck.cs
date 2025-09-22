public class Deck
{
    private List<Deck> cards = new List<Deck>();
    private Random rng = new Random();

    public void Add(Card card) => cards.Add(card);
    public void Shuffle()
    {
        for (int i = cards.Count - 1; i > 0; i--)
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]);
        }
    }

    public Card Draw()
    {
        if (cards.Count == 0) return null;
        var top = cards[0];
        cards.RemoveAt(0);
        return top;
    }

    public int Count => cards.Count;
}