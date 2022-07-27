namespace DominionSimulator2;

public class CardHandling
{
    public CardHandling() { }

    /// <summary>
    /// Gets the best card(s) from a list of cards.
    /// </summary>
    /// <param name="cards">List of cards to choose the best card(s) from.</param>
    /// <param name="numCards">Number of cards to return.</param>
    /// <returns>A list of the best cards</returns>
    public static IEnumerable<Card> GetBest(List<Card> cards, int numCards = 1)
    {
        if (cards is null) throw new ArgumentNullException(nameof(cards));

        if (numCards <= 0) throw new ArgumentException("Number of cards must be greater than 0.", nameof(numCards));

        if (numCards > cards.Count)
            return cards.OrderByDescending(c => c.Weight);

        if (cards.All(c => c.Weight == 0))
            return cards.OrderByDescending(c => c.Cost).Take(numCards);
        
        cards = cards.OrderByDescending(c => c.Weight).ToList();
        var toReturn = new List<Card>();
        for (int i = 0; i < numCards; ++i)
        {
            // 5% chance of getting a random card
            if(new Random().Next(0,100) < 5)
                toReturn.Add(cards[new Random().Next(0, cards.Count)]);
            else
            {
                toReturn.Add(cards[0]);
                cards.RemoveAt(0);
            }
        }

        return toReturn;
    }
}