public class Table
{
    public List<Card> CardsOnTable { get; set;  } = new List<Card>();

    public void PlayCard(Card card)
    {
        CardsOnTable.Add(card);
    }
}