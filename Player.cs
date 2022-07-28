using DominionSimulator2.Data;

namespace DominionSimulator2;

public class Player
{
    public string Name { get; set; } = "";
    public int Actions { get; set; } = 0;
    public int Buys { get; set; } = 0;
    public int Coins { get; set; } = 0;
    public CardAreas Cards { get; set; } = new();

    public void PlayActions(Supply supply)
    {
        var actionCards = Cards.GetCardByType(CardType.Action).ToList();
        while(Actions > 0 && actionCards.Any())
        {
            var bestAction = CardHandling.GetBest(actionCards).First();
            Actions--;
            Cards.PlayCard(bestAction, this, supply);
            bestAction.Weight += 2;
            actionCards = Cards.GetCardByType(CardType.Action).ToList();
        }
        // Discard remaining action cards
        Cards.DiscardCards(actionCards);
    }

    public void BuyCards(Supply supply)
    {
        var potentialCoins = Coins + Cards.GetCoinsInHand();
        var cardsForPurchase = CardDB.GetCards(supply.GetAffordableCards(potentialCoins)).ToList();
        while(Buys > 0 && cardsForPurchase.Any())
        {
            var bestCard = CardHandling.GetBest(cardsForPurchase).First();
            Buys--;
            Cards.PlayTreasures(bestCard.Cost, this);
            Coins -= bestCard.Cost;
            supply.PurchaseCard(bestCard.Name);
            Cards.Discard.Add(new(bestCard));

            // Prep for next check
            potentialCoins = Coins + Cards.GetCoinsInHand();
            cardsForPurchase = CardDB.GetCards(supply.GetAffordableCards(potentialCoins)).ToList();
        }
    }

    public void EndTurn()
    {
        Cards.DiscardCards(Cards.Hand, true);
        Cards.CleanPlayArea();
        Cards.Draw(5);
    }

    public override string ToString()
    {
        return Name;
    }
}