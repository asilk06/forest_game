public class Deck
{
    private List<Card> cards = new List<Card>();
    private Random rng = new Random();

    public void Add(Card card) => cards.Add(card);
    public void Shuffle() // Blandning av kort med Fisher-Yates algoritmen
    {
        for (int i = cards.Count - 1; i > 0; i--) // Går baklänges genom listan. För varje index i, väljer den en slumpad plats j mellan 0 och i.
        {
            int j = rng.Next(i + 1);
            (cards[i], cards[j]) = (cards[j], cards[i]); // Sedan byter den plats på korten på i och j.
        }
    }

    public Card? Draw()
    {
        if (cards.Count == 0) return null;
        var top = cards[0];
        cards.RemoveAt(0);
        return top;
    }
    public void InitializeDeck()
    {
        // här ska decket genereras
    }

    public int Count => cards.Count;
}