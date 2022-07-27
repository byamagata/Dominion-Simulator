using System.Text.RegularExpressions;

namespace DominionSimulator2;

public class TrashEffect : ICardEffect
{
    public int Amount { get; set; }
    public CardType Type { get; set; }
    public Player Player { get; set; } = null;
    public Supply Supply { get; set; } = null;
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

    public void Handle()
    {
        
    }
}