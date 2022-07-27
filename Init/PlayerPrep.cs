using DominionSimulator2.Data;

namespace DominionSimulator2;

public class PlayerPrep
{
    public CardDB CardDB { get; set; } = null;
    public PlayerPrep(CardDB cardDB) { CardDB = cardDB; }

    public List<Player> CreatePlayers(int numPlayers)
    {
        var players = new List<Player>();
        for (int i = 0; i < numPlayers; i++)
        {
            var player = new Player();
            player.Name = $"Player {i + 1}";
            player.Cards = new CardAreas();

            // Add the default cards to the player's deck
            for (int j = 0; j < 7; ++j) player.Cards.Deck.Add(CardDB.GetCard("Copper"));

            // TODO: Determine best way to handle card effects: 
            // 1. Create a new class "CardEffect" that implements ICardEffect, differentiate on Type field
            // 2. Keep all of the card effects, create a static method to handle assignment of the player and supply objects (if necessary)
            // 3. Set the player and supply on every effect, regardless of if needed.

            for (int j = 0; j < 3; ++j) player.Cards.Deck.Add(CardDB.GetCard("Estate"));

            player.Cards.ShuffleDeck();
            player.Cards.Draw(5);

            players.Add(player);
        }
        return players;
    }
}