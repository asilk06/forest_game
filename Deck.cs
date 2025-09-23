public class Deck
{
    private List<Card> cards = new List<Card>();
    private Random rng = new Random();
    public int Count => cards.Count;
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
        string[] creaturePool = { "Uggla", "Groda", "Tomte", "Fe" };

        // Metod för att generera en slumpmässig lista av varelser
        List<string> RandomCreatures(int maxCount)
        {
            int count = rng.Next(1, maxCount + 1);
            List<string> creatures = new List<string>();
            for (int i = 0; i < count; i++)
            {
                string creature = creaturePool[rng.Next(creaturePool.Length)];
                creatures.Add(creature);
            }
            return creatures;
        }

        // 17 tree tops
        for (int i = 0; i < 17; i++)
        {
            cards.Add(new TreeTop(RandomCreatures(4)));
        }

        // 36 tree trunks
        for (int i = 0; i < 36; i++)
        {
            cards.Add(new TreeTrunk(RandomCreatures(4)));
        }

        int maxValue = rng.Next(1, 5); // Slumpmässigt värde mellan 1 och 4 för jokrar och vargar

        // 2 jokers och 2 wolves
        for (int i = 0; i < 2; i++)
        {
            cards.Add(new Joker("Joker", "Specialkort", maxValue));
            cards.Add(new Wolf("Varg", "Specialkort", maxValue));
        }
    }
}
