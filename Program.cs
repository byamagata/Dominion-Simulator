using System.Text.Json;
using DominionSimulator2;
using DominionSimulator2.Data;

// Prep the Supply
var cardDb = new CardDB();
cardDb = cardDb.Load();
var supply = new SupplyPrep(cardDb).CreateSupply();

// Prep the Players
var players = new PlayerPrep(cardDb).CreatePlayers(4);

// Before the game begins, pick a random order of players:
var random = new Random();
players = players.OrderBy(x => random.Next()).ToList();

// Play the game
var gameOver = false;
int turn = 1;
while(!gameOver)
{
    Console.WriteLine($"Turn {turn}");
    // Each player takes a turn
    foreach (var player in players)
    {
        // Set Player Base Values
        player.Actions = 1;
        player.Buys = 1;
        player.Coins = 0;

        // Play Action Cards
        player.PlayActions();

        // Buy Cards
        player.BuyCards(supply, cardDb);

        // Player Ends Turn
        player.EndTurn();

        // Check if game is over
        if(supply.IsGameOver())
        {
            gameOver = true;
            break;
        }
    }
    turn++;
}

// Check for winner
foreach(var player in players)
{
    player.Cards.ResetDeck();
    var points = player.Cards.GetVictoryPoints();
    Console.WriteLine($"{player.Name} has {points} points");
    player.Cards.GroupCards();
}