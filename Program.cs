using System;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Välkommen till Forest!");
        Deck deck = new Deck();
        deck.InitializeDeck();
        deck.Shuffle();

        Console.WriteLine($"Deck skapat med {deck.Count} kort.");

        Player player1 = new Player("Spelare 1");

        Console.WriteLine($"{player1.Name} drar 3 kort.");
        for (int i = 0; i < 3; i++)
        {
            player1.DrawCard(deck);
            Console.WriteLine($"Drog: {player1.playerHand[i]}");
        }

        Console.WriteLine($"Kort kvar i deck: {deck.Count}");

        Table table = new Table();
        table.PlayCard(player1.playerHand[0]);
        Console.WriteLine($"Spelade kort på bordet: {player1.playerHand[0]}");
        player1.playerHand.RemoveAt(0);

        Console.WriteLine($"Kort kvar i hand:");
        foreach (var card in player1.playerHand)
        {
            Console.WriteLine(card);
        }
    }
}