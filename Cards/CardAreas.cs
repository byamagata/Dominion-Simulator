namespace DominionSimulator2;

public class CardAreas
{
    public List<Card> Hand { get; set; } = new();
    public List<Card> Deck { get; set; } = new();
    public List<Card> Discard { get; set; } = new();
    public List<Card> Played { get; set; } = new();

    public void ShuffleDeck(bool addDiscard = false)
    {
        if (addDiscard)
        {
            Deck.AddRange(Discard);
            Discard.Clear();
        }
        var random = new Random();
        Deck = Deck.OrderBy(x => random.Next()).ToList();
    }

    public void Draw(int numCards)
    {
        if(numCards < Deck.Count)
        {
            Hand.AddRange(Deck.Take(numCards));
            Deck.RemoveRange(0, numCards);
        }
        else if(numCards >= Deck.Count)
        {
            Hand.AddRange(Deck.Take(Deck.Count));
            numCards -= Deck.Count;
            Deck.Clear();
            if(Discard.Count <= 0)
                return;
            ShuffleDeck(true);
            Draw(numCards);
        }
    }

    public void PlayCard(Card card, Player player)
    {
        Hand.Remove(card);
        Played.Add(card);
        card.Play(player);
    }

    public IEnumerable<Card> GetCardByType(CardType type) => Hand.Where(c => c.Types.Contains(type));
    public int GetCoinsInHand() => Hand.Select(c => c.Effects.Where(e => e is AddCoinEffect).Sum(e => (e as AddCoinEffect).Coins)).Sum();

    public void PlayTreasures(int cost, Player player)
    {
        var treasures = GetCardByType(CardType.Treasure).OrderByDescending(c => (c.Effects.Where(e => e is AddCoinEffect).Sum(e => (e as AddCoinEffect).Coins))).ToList();
        while(cost > player.Coins)
        {
            var bestTreasure = treasures.First();
            treasures.Remove(bestTreasure);
            PlayCard(bestTreasure, player);
        }
    }

    public void CleanPlayArea()
    {
        Discard.AddRange(Played);
        Played.Clear();
    }

    public void DiscardHand()
    {
        Discard.AddRange(Hand);
        Hand.ForEach(c => c.Weight -= 1);
        Hand.Clear();
    }

    public void ResetDeck()
    {
        Deck.AddRange(Discard);
        Deck.AddRange(Played);
        Deck.AddRange(Hand);
    }

    public void GroupCards()
    {
        Dictionary<string, int> cardCount = new();
        foreach(var card in Deck)
        {
            if(!cardCount.Keys.Contains(card.Name))
                cardCount.Add(card.Name, 0);
            cardCount[card.Name]++;
        }
        foreach(var card in cardCount.Keys)
        {
            Console.WriteLine($"{card} x{cardCount[card]}");
        }
    }

    public int GetVictoryPoints() => Deck.Select(c => c.Effects.Where(e => e is VictoryEffect).Sum(e => (e as VictoryEffect).Points)).Sum();

    public override string ToString() => $"Hand: {Hand.Count}, Deck: {Deck.Count}, Discard: {Discard.Count}, Played: {Played.Count}";
}