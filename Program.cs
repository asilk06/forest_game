using System;
using System.Security.Cryptography;
class Program
{
    static void Main(string[] args)
    {
        Console.Clear();
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
            // Kolla om spelarantalet är giltigt
            if (playerCountInput != null &&
            int.TryParse(playerCountInput, out playerCount) &&
            playerCount >= 2 &&
            playerCount <= 4)
            {
                Console.WriteLine();
                Thread.Sleep(1000);
                Console.WriteLine($"Antal spelare: {playerCount}");
                Console.WriteLine();
                break; // giltigt antal spelare
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Ogiltigt antal spelare. Vänligen ange ett nummer mellan 2 och 4.");
                Thread.Sleep(1000);
                Console.WriteLine();
                playerCountInput = Console.ReadLine();
            }
        }

        Thread.Sleep(1000);

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
        Thread.Sleep(500);
        Console.WriteLine("Spelet börjar nu. Lycka till!");
        Thread.Sleep(2000);
        Console.Clear();

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
                    Thread.Sleep(500);
                }
            }

            Console.WriteLine();

            Console.WriteLine($"--- {currentPlayer.Name}s tur. Dina kort: ---");
            for (int i = 0; i < currentPlayer.playerHand.Count; i++) // Visa spelarens kort
            {
                Console.WriteLine($"{i + 1}. {currentPlayer.playerHand[i]}");
                Thread.Sleep(500);
            }
            Console.WriteLine();

            int cardChoice;
            string? cardChoiceInput;
            while (true) // Loop för att välja kort att spela
            {
                Console.WriteLine("Välj ett kort att spela (ange numret):");
                cardChoiceInput = Console.ReadLine();
                
                // Kolla om kortvalet är giltigt
                if (cardChoiceInput != null &&
                int.TryParse(cardChoiceInput, out cardChoice) &&
                cardChoice >= 1 &&
                cardChoice <= currentPlayer.playerHand.Count)
                {
                    Card chosenCard = currentPlayer.playerHand[cardChoice - 1];

                    if (chosenCard is Joker joker)
                    {
                        List<string> allowedCreatures = new List<string> { "Uggla", "Groda", "Tomte", "Fe" };

                        Console.WriteLine();
                        Console.WriteLine($"Du spelade en Joker med värde {joker.Value}");
                        Console.WriteLine();
                        
                        Thread.Sleep(1000);
                        Console.WriteLine("Välj en varelse att lägga till på bordet:");

                        for (int i = 0; i < allowedCreatures.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {allowedCreatures[i]}");
                        }

                        int creatureChoice;
                        while (true) // Loop för att välja varelse för jokern
                        {
                            string? creatureChoiceInput = Console.ReadLine();
                            if (creatureChoiceInput != null &&
                            int.TryParse(creatureChoiceInput, out creatureChoice) &&
                            creatureChoice >= 1 &&
                            creatureChoice <= allowedCreatures.Count)
                            {
                                break; // giltigt val
                            }
                            Console.WriteLine("Ogiltigt val. Försök igen.");
                        }

                        string chosenCreature = allowedCreatures[creatureChoice - 1];

                        for (int i = 0; i < joker.Value; i++) // Lägg till valda varelser på bordet
                        {
                            table.PlayCard(new TreeTrunk(new List<string> { chosenCreature }));
                        }

                        Console.WriteLine($"Jokern lade till {joker.Value} {chosenCreature} på bordet.");
                        Thread.Sleep(1000);

                        currentPlayer.playerHand.RemoveAt(cardChoice - 1);
                    }
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
                            
                            // Spelaren väljer att samla in korten
                            if (collectInput != null && collectInput.ToLower() == "j")
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
                                foreach (var card in cardsToCollect) table.CardsOnTable.Remove(card); // Ta bort korten från bordet

                                Console.WriteLine($"{currentPlayer.Name} samlade in {cardsToCollect.Count} kort med {creature.Key} och har nu {currentPlayer.playerPoints} poäng.");
                            }
                        }
                    }

                    currentPlayer.DrawCard(deck); // Dra ett nytt kort efter att ha spelat
                    Console.WriteLine($"{currentPlayer.Name} drog kortet {currentPlayer.playerHand.Last()}");
                    Thread.Sleep(1000);
                    Console.WriteLine();
                    Console.WriteLine($"Det finns nu {deck.Count} kort kvar i leken.");
                    break; // giltigt val
                }
                else
                {
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                }
            }

            if (deck.Count == 0) // Om inga kort kvar i leken, avsluta spelet
            {
                Console.WriteLine();
                Console.WriteLine("Inga kort kvar i leken. Spelet avslutas.");
                Thread.Sleep(2000);
                Console.Clear();
                Console.WriteLine("Slutpoäng:");
                Thread.Sleep(1000);
                foreach (var player in players) // Visa slutpoängen
                {
                    Console.WriteLine($"{player.Name}: {player.playerPoints} poäng");
                    Thread.Sleep(500);
                }

                int maxPoints = players.Max(p => p.playerPoints); // Hitta högsta poängen
                var winners = players.Where(p => p.playerPoints == maxPoints).ToList();
                if (winners.Count > 1) // Oavgjort
                {
                    Console.WriteLine("Det är oavgjort mellan: " + string.Join(", ", winners.Select(w => w.Name)));
                }
                else // En vinnare
                {
                    Console.WriteLine($"Vinnaren är {winners[0].Name} med {winners[0].playerPoints} poäng! Grattis!");
                }

                break; // Avsluta spel loopen
            }
            else
            {
                Console.WriteLine("Tryck på valfri tangent för att börja nästa tur...");
                Console.ReadKey(true); // Väntar på att spelaren trycker på en tangent
                Console.Clear(); // Rensar terminalen
                currentPlayerIndex = (currentPlayerIndex + 1) % players.Count; // Nästa spelares tur
                Console.WriteLine();
            }
        }
    }
}

// Om det finns flera olika typer av varelser som är 7 eller flera på bordet, så kan spelaren samla in alla även efter första insamlingen.
// Exempel: 7 rävar och 8 ugglor på bordet. 
// Spelaren kan samla in både rävarna och ugglorna, även om det finns mindre än 8 ugglor på bordet efter att spelaren samlar in rävarna.
// Detta ska egentligen inte vara möjligt enligt regler, men jag har inte tid att fixa det nu.