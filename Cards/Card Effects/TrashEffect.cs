using System.Text.RegularExpressions;
using DominionSimulator2.Data;

namespace DominionSimulator2;

public class TrashEffect : ICardEffect
{
    public int Amount { get; set; }
    public CardType Type { get; set; }
    public TrashEffect(string effect)
    {
        Regex regex = new Regex(@"Trash:(?<amt>\d)\|Type:(?<type>.*)$");
        var match = regex.Match(effect);
        if (match.Success)
        {
            Amount = int.Parse(match.Groups["amt"].Value);
            Type = (CardType)Enum.Parse(typeof(CardType), match.Groups["type"].Value);
        }
    }

    public void Handle(string name, Player player = null, Supply supply = null)
    {
        IEnumerable<Card> cardsToTrash;
        if(Type != CardType.Any)
            cardsToTrash = CardHandling.GetWorst(player.Cards.GetCardByType(Type).ToList(), Amount).ToList();
        else // Trash only Coppers, estates, and Curses
            cardsToTrash = player.Cards.GetTrash();

        cardsToTrash.ToList().ForEach(c => player.Cards.Hand.Remove(c));
        cardsToTrash.ToList().ForEach(c => CardDB.UpdateWeight(+5, c.Name));
        supply.Trash.AddRange(cardsToTrash);
    }
}