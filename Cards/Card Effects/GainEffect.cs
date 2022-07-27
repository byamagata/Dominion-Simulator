using System.Text.RegularExpressions;

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

        public void Handle(Player player)
        {}
    }
}