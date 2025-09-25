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

        while (true) // Loop för att få giltigt antal spelare
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
                Thread.Sleep(500);
            }
            else // Visa korten på bordet
            {
                table.CardsOnTable = table.CardsOnTable
                    .OrderByDescending(c => c is TreeTop)    // TreeTops först
                    .ThenByDescending(c => c is TreeTrunk)   // TreeTrunks efter
                    .ToList();

                foreach (var card in table.CardsOnTable)
                {
                    Console.WriteLine(card);
                    Thread.Sleep(400);
                }
            }

            Console.WriteLine();

            Console.WriteLine($"{currentPlayer.Name}s tur. Dina kort:");
            for (int i = 0; i < currentPlayer.playerHand.Count; i++) // Visa spelarens kort
            {
                Console.WriteLine($"{i + 1}. {currentPlayer.playerHand[i]}");
                Thread.Sleep(400);
            }
            Console.WriteLine();
            
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

                    Console.WriteLine();
                    Console.WriteLine($"{currentPlayer.Name} spelade: {chosenCard}");
                    Thread.Sleep(1000);
                    Console.WriteLine();

                    Dictionary<string, int> creatureCount = new Dictionary<string, int>();
                    foreach (var card in table.CardsOnTable) // Räkna varelser på bordet
                    {
                        List<string> creatures = new List<string>();
                        if (card is TreeTop treeTop) creatures = treeTop.Creatures;
                        if (card is TreeTrunk treeTrunk) creatures = treeTrunk.Creatures;

                        foreach (var creature in creatures)
                        {
                            if (!creatureCount.ContainsKey(creature)) 
                                creatureCount[creature] = 0;
                            creatureCount[creature]++;
                        }
                    }

                    foreach (var creature in creatureCount)
                    {
                        if (creature.Value >= 7) // Om 7 eller fler av samma varelse, låt spelaren samla in korten
                        {
                            Console.WriteLine($"Det finns 7 eller fler {creature.Key} på bordet! Vill du samla in dem? (j/n)");
                            string? collectInput = Console.ReadLine();
                            if (collectInput != null && collectInput.ToLower() == "j") // Spelaren väljer att samla in korten
                            {
                                var cardsToCollect = new List<Card>();
                                foreach (var card in table.CardsOnTable) // Hitta och samla in kort med den varelsen
                                {
                                    if ((card is TreeTop treeTop && treeTop.Creatures.Contains(creature.Key)) ||
                                        (card is TreeTrunk treeTrunk && treeTrunk.Creatures.Contains(creature.Key)))
                                    {
                                        cardsToCollect.Add(card);
                                    }
                                }

                                currentPlayer.playerPoints += cardsToCollect.Count; // Lägg till poäng
                                foreach (var card in cardsToCollect) table.CardsOnTable.Remove(card);

                                Console.WriteLine($"{currentPlayer.Name} samlade in {cardsToCollect.Count} kort med {creature.Key} och har nu {currentPlayer.playerPoints} poäng.");
                            }
                        }
                    }

                    currentPlayer.DrawCard(deck); // Dra ett nytt kort efter att ha spelat
                    Console.WriteLine($"{currentPlayer} drog kortet {currentPlayer.playerHand.Last()}");
                    Console.WriteLine();
                    Thread.Sleep(1000);
                    Console.WriteLine($"Det finns nu {deck.Count} kort kvar i leken.");
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
    }
}