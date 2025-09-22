using System;
class Program
{
    static void Main()
    {
        Deck deck = new Deck();
        // deck.Add(new Card("Uggla", "Djur"));
        // deck.Add(new Card("Groda", "Djur"));
        // deck.Add(new Card("Älg", "Djur"));
        deck.Shuffle();

        // Skapa spelare
        Player alice = new Player("Alice");

        // Dra kort
        alice.DrawCard(deck);

        // Visa resultat
        Console.WriteLine($"{alice.Name} drog: {alice.playerHand[0]}");
        Console.WriteLine($"Kort kvar i leken: {deck.Count}");

        // Skapa bord och spela kort
        Table table = new Table();
        table.PlayCard(alice.playerHand[0]);
        Console.WriteLine($"Kort på bordet: {table.CardsOnTable[0]}");
    }
}