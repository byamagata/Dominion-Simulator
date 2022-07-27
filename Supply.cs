namespace DominionSimulator2;

public class Supply
{
    public List<Pile> Treasures { get; set; } = new();
    public List<Pile> Victory { get; set; } = new();
    public List<Pile> Kingdom { get; set; } = new();
    public List<Card> Trash { get; set; } = new();

    public void AddTreasure(Card card, int weight, int numCards) => Treasures.Add(new(card, weight, numCards));
    public void AddVictory(Card card, int weight, int numCards) => Victory.Add(new(card, weight, numCards));
    public void AddKingdom(Card card, int weight, int numCards) => Kingdom.Add(new(card, weight, numCards));

    /// <summary>
    /// Decreases the card count from the corresponding pile.
    /// </summary>
    /// <param name="name">Name of the card the player is purchasing</param>
    public void PurchaseCard(string name)
    {
        if(Treasures.Any(p => p.Name == name))
            Treasures.Find(x => x.Name == name).CardsInPile--;
        else if(Victory.Any(p => p.Name == name))
            Victory.Find(x => x.Name == name).CardsInPile--;
        else if(Kingdom.Any(p => p.Name == name))
            Kingdom.Find(x => x.Name == name).CardsInPile--;
    }

    public const int MAX_EMPTY_PILES = 3;
    public bool IsGameOver() => GetEmptySupplyPiles() >= MAX_EMPTY_PILES || Victory.Find(p => p.Name == "Province").CardsInPile <= 0;

    public int GetEmptySupplyPiles() => Treasures.Count(p => p.CardsInPile == 0) + Victory.Count(p => p.CardsInPile == 0) + Kingdom.Count(p => p.CardsInPile == 0);

    public List<string> GetAffordableCards(int coins)
    {
        var affordableCards = new List<string>();
        affordableCards.AddRange(Treasures.Where(x => x.Cost <= coins && x.CardsInPile > 0).Select(x => x.Name).ToList());
        affordableCards.AddRange(Victory.Where(x => x.Cost <= coins && x.CardsInPile > 0).Select(x => x.Name).ToList());
        affordableCards.AddRange(Kingdom.Where(x => x.Cost <= coins && x.CardsInPile > 0).Select(x => x.Name).ToList());
        // TODO: Figure out how to remove this later and have the program figure it out on its own.
        affordableCards.Remove("Copper");
        affordableCards.Remove("Curse");
        if (affordableCards.Contains("Estate"))
            affordableCards.Remove("Estate");
        return affordableCards;
    }

    public void TrashCard(Card card) => Trash.Add(card);
}


public class Pile
{
    public const int MAX_CARDS_IN_PILE = 10;

    public string Name { get; set; } = "";
    public int CardsInPile { get; set; } = 0;
    public int Cost { get; set; } = 0;
    public int Weight { get; set; } = 0;

    public Pile(Card card, int weight, int cardsInPile = MAX_CARDS_IN_PILE)
    {
        Name = card.Name;
        Cost = card.Cost;
        Weight = weight;
        CardsInPile = cardsInPile;
    }

    public void PurchaseCard() => CardsInPile--;
}