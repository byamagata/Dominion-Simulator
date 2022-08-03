using System.Text.Json;
using DominionSimulator2;
using DominionSimulator2.Data;

// Play the game X times
const int MAX_GAMES = 1000;

// 0 out the weights from the previous trials
CardDB.ResetDB();

var watch = System.Diagnostics.Stopwatch.StartNew();
for (int i = 0; i < MAX_GAMES; i++)
{
    if (i % 10 == 0)
    {
        Console.WriteLine($"Game {i}");
    }

    // Prep the Supply
    CardDB.Init();
    var supply = new SupplyPrep().CreateSupply();

    // Prep the Players
    var players = new PlayerPrep().CreatePlayers(4);

    // Before the game begins, pick a random order of players:
    var random = new Random();
    players = players.OrderBy(x => random.Next()).ToList();

    // Play the game
    var gameOver = false;
    int turn = 1;
    while (!gameOver)
    {
        // Each player takes a turn
        foreach (var player in players)
        {
            // Set Player Base Values
            player.Actions = 1;
            player.Buys = 1;
            player.Coins = 0;

            // Play Action Cards
            player.PlayActions(supply);

            // Buy Cards
            player.BuyCards(supply);

            // Player Ends Turn
            player.EndTurn();

            // Check if game is over
            if (supply.IsGameOver())
            {
                gameOver = true;
                break;
            }
        }
        turn++;
        if(turn > 30)
        {
            // Check to see if everyone is stuck because no one can buy anything
        }
    }

    // Update the CardDB and save to file
    CardDB.Save();
}
watch.Stop();

// print elapsed time in seconds
Console.WriteLine($"Elapsed Time: {watch.ElapsedMilliseconds / 1000} seconds");
















// // Check for winner
// var allCards = new List<Card>();
// foreach(var player in players)
// {
//     player.Cards.ResetDeck();
//     var points = player.Cards.GetVictoryPoints();
//     Console.WriteLine($"{player.Name} has {points} points");
//     player.Cards.GroupCards();
//     allCards.AddRange(player.Cards.Deck);
// }

// var cardWeights = new Dictionary<string, int>();
// foreach(var card in allCards)
// {
//     if(!cardWeights.Keys.Contains(card.Name))
//         cardWeights.Add(card.Name, 0);
//     cardWeights[card.Name] += card.Effects.Sum(e => (e is VictoryEffect) ? (e as VictoryEffect).Points : 0);
// }

// Console.WriteLine("\n\nCard Weights\n");
// foreach(var card in cardWeights.OrderByDescending(x => x.Value))
// {
//     CardDB.UpdateWeight(cardWeights[card.Key], card.Key);
// }

// Console.WriteLine("Card Name\t\tWeight\t\tTotal Quantity");
// foreach(var card in CardDB.Cards.OrderBy(c => c.Weight))
// {
//     Console.WriteLine($"{card.Name}\t\t\t{card.Weight}\t\t\t{allCards.Sum(x => x.Name == card.Name ? 1 : 0)}");
// }