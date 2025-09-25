using System;
using System.Security.Cryptography;
class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Välkommen till Forest!");
        Deck deck = new Deck();
        deck.InitializeDeck();
        deck.Shuffle();
        Thread.Sleep(1000);

        Console.WriteLine("Hur många spelare? (2-4)");
        string? playerCountInput = Console.ReadLine();
        int playerCount;

        while (true)
        {
            if (playerCountInput != null && int.TryParse(playerCountInput, out playerCount) && playerCount >= 2 && playerCount <= 4) // Kolla om spelarantalet är giltigt
            {
                Console.WriteLine();
                Console.WriteLine($"Antal spelare: {playerCount}");
                Console.WriteLine();
                break; // giltigt antal spelare
            }
            else
            {
                Console.WriteLine("Ogiltigt antal spelare. Vänligen ange ett nummer mellan 2 och 4.");
                Console.WriteLine();
                playerCountInput = Console.ReadLine();
            }
        }

        Thread.Sleep(750);
        List<Player> players = new List<Player>(); // Skapa spelarlistan
        for (int i = 1; i <= playerCount; i++)
        {
            players.Add(new Player($"Spelare {i}"));
        }

        foreach (var player in players) // Dra 3 kort till varje spelare
        {
            Console.WriteLine($"{player.Name} drar 3 kort.");
            for (int i = 0; i < 3; i++)
            {
                player.DrawCard(deck);
            }
            Thread.Sleep(500);
        }

        Console.WriteLine();
        Console.WriteLine("Spelet börjar nu. Lycka till!");
        Console.WriteLine();
        Thread.Sleep(1000);

        int currentPlayerIndex = 0;
        Table table = new Table();

        while (true) // Spel loopen
        {
            Player currentPlayer = players[currentPlayerIndex];
            Console.WriteLine("Kort på bordet:");
            Thread.Sleep(500);
            if (table.CardsOnTable.Count == 0) // Om inga kort på bordet
            {
                Console.WriteLine("Inga kort på bordet.");
            }
            else // Visa korten på bordet
            {
                foreach (var card in table.CardsOnTable)
                {
                    Console.WriteLine(card);
                    Thread.Sleep(300);
                }
            }

            Console.WriteLine();

            Console.WriteLine($"{currentPlayer.Name}s tur. Dina kort:");
            for (int i = 0; i < currentPlayer.playerHand.Count; i++) // Visa spelarens kort
            {
                Console.WriteLine($"{i + 1}. {currentPlayer.playerHand[i]}");
                Thread.Sleep(300);
            }

            int cardChoice;
            string? cardChoiceInput;
            while (true) // Loop för att välja kort att spela
            {
                Console.WriteLine("Välj ett kort att spela (ange numret):");
                cardChoiceInput = Console.ReadLine();
                if (cardChoiceInput != null && int.TryParse(cardChoiceInput, out cardChoice) && cardChoice >= 1 && cardChoice <= currentPlayer.playerHand.Count) // Kolla om kortvalet är giltigt
                {
                    Card chosenCard = currentPlayer.playerHand[cardChoice - 1];
                    table.PlayCard(chosenCard);
                    currentPlayer.playerHand.RemoveAt(cardChoice - 1);

                    Console.WriteLine($"{currentPlayer.Name} spelade: {chosenCard}");
                    Console.WriteLine();

                    Console.WriteLine("Kort på bordet:");
                    foreach (var card in table.CardsOnTable)
                    {
                        Console.WriteLine(card);
                        Thread.Sleep(300);
                    }
                    Thread.Sleep(500);
                    break; // giltigt val
                }
                else
                {
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                }
            }
            currentPlayerIndex = (currentPlayerIndex + 1) % players.Count; // Nästa spelares tur
            Console.WriteLine();


        }

        // Console.WriteLine($"Deck skapat med {deck.Count} kort.");

        // Player player1 = new Player("Spelare 1");

        // Console.WriteLine($"{player1.Name} drar 3 kort.");
        // for (int i = 0; i < 3; i++)
        // {
        //     player1.DrawCard(deck);
        //     Console.WriteLine($"Drog: {player1.playerHand[i]}");
        // }

        // Console.WriteLine($"Kort kvar i deck: {deck.Count}");

        // Table table = new Table();
        // table.PlayCard(player1.playerHand[0]);
        // Console.WriteLine($"Spelade kort på bordet: {player1.playerHand[0]}");
        // player1.playerHand.RemoveAt(0);

        // Console.WriteLine($"Kort kvar i hand:");
        // foreach (var card in player1.playerHand)
        // {
        //     Console.WriteLine(card);
        // }
    }
}