using System.Text.RegularExpressions;
using DominionSimulator2.Data;

namespace DominionSimulator2
{
    internal class GainEffect : ICardEffect
    {
        public int Amount { get; set; }
        public CardType Type { get; set; }
        public int? TotalCost { get; set; }
        public int? AddedCost { get; set; }

        public GainEffect(string effect)
        {
            Regex regex = new Regex(@"Gain:(?<amt>\d)\|Type:(?<type>.*?)\|Cost:(?:(?<totCost>\d)|\+(?<addCost>\d))");
            var match = regex.Match(effect);
            if (match.Success)
            {
                Amount = int.Parse(match.Groups["amt"].Value);
                Type = (CardType)Enum.Parse(typeof(CardType), match.Groups["type"].Value);
                if (match.Groups["totCost"].Success)
                    TotalCost = int.Parse(match.Groups["totCost"].Value);
                else
                    AddedCost = int.Parse(match.Groups["addCost"].Value);
            }
        }

        public void Handle(string name, Player player = null, Supply supply = null)
        {
            List<string> possibleCards;
            if (TotalCost is not null && TotalCost.HasValue)
                possibleCards = supply.GetAffordableCards(TotalCost.Value);
            else if (AddedCost is not null && AddedCost.HasValue)
            {
                var prevTrashed = supply.Trash.Last().Cost;
                possibleCards = supply.GetAffordableCards(prevTrashed + AddedCost.Value);
            }
            else throw new Exception("TotalCost and AddedCost were both null");

            if (possibleCards.Any())
                player.Cards.Discard.Add(CardHandling.GetBest(CardDB.GetCards(possibleCards).ToList()).First());
            else return;
        }
    }
}