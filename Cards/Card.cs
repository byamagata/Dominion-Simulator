using DominionSimulator2.Data;

namespace DominionSimulator2;

public class Card
{
    public string Name { get; set; } = "";
    public string SetName { get; set; } = "";
    public int Cost { get; set; } = 0;
    public int Weight { get; set; } = 0;

    private List<CardType> _types = new();
    public List<CardType> Types
    {
        get => _types;
        set
        {
            if (value is not null && !value.Contains(CardType.Any))
                value.Add(CardType.Any);
            else if (value is null)
                value = new List<CardType> { CardType.Any };
            _types = value;
        }
    }
    public List<ICardEffect> Effects { get; set; } = new();

#region Copy Constructor

    public Card(Card card)
    {
        Name = card.Name;
        SetName = card.SetName;
        Cost = card.Cost;
        Weight = card.Weight;
        Types = card.Types;
        Effects = card.Effects;
    }

    public Card(CardDTO cardObj)
    {
        Name = cardObj.Name;
        SetName = cardObj.SetName;
        Cost = cardObj.Cost;
        Weight = cardObj.Weight;
        Types = GetTypes(cardObj.Types);
        Effects = GetEffects(cardObj.Effects);
    }

    public List<CardType> GetTypes(string[] types) => types.Select(x => (CardType)Enum.Parse(typeof(CardType), x)).ToList();

    public List<ICardEffect> GetEffects(string[] effects)
    {
        var result = new List<ICardEffect>();

        foreach (var effect in effects)
        {
            if (effect.Contains("|"))
                result.Add(ParseEffect(effect.Split("|")[0].Split(":")[0], effect));
            else
                result.Add(ParseEffect(effect.Split(":")[0], effect));
        }
        return result;
    }

    private ICardEffect ParseEffect(string type, string effect) => type switch
    {
        "Coin" => new AddCoinEffect { Coins = int.Parse(effect.Split(":")[1]) },
        "Action" => new AddActionEffect { Actions = int.Parse(effect.Split(":")[1]) },
        "Buy" => new AddBuyEffect { Buys = int.Parse(effect.Split(":")[1]) },
        "Card" => new AddCardEffect { Cards = int.Parse(effect.Split(":")[1]) },
        "Throne Room" => new ThroneRoomEffect(),
        "Trash" => new TrashEffect(effect),
        "Gain" => new GainEffect(effect),
        "Reaction" => new ReactionEffect(),
        "Victory" => new VictoryEffect { Points = int.Parse(effect.Split(":")[1]) },
        _ => throw new Exception($"Unknown effect type: {type}")
    };
#endregion

    public void Play(Player player = null, Supply supply = null)
    {
        foreach(var effect in Effects)
        {
            effect.Handle(Name, player, supply);
        }
    }

    public override string ToString()
    {
        return Name;
    }
}

public enum CardType
{
    Treasure,
    Victory,
    Curse,
    Action,
    Reaction,
    Attack,
    Any
}

