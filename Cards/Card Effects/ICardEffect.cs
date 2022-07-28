namespace DominionSimulator2;
using System.Text.Json;
using System.Text.Json.Serialization;
using DominionSimulator2.Data;

public interface ICardEffect
{
    void Handle(string name, Player player = null, Supply supply = null);
}

public class AddActionEffect : ICardEffect
{
    public int Actions { get; set; } = 0;
    public void Handle(string name, Player player = null, Supply supply = null)
    {
        player.Actions += Actions;
    }
}

public class AddCoinEffect : ICardEffect
{
    public int Coins { get; set; } = 0;
    public void Handle(string name, Player player = null, Supply supply = null)
    {
        player.Coins += Coins;
    }
}

public class AddBuyEffect : ICardEffect
{
    public int Buys { get; set; } = 0;
    public void Handle(string name, Player player = null, Supply supply = null)
    {
        player.Buys += Buys;
    }
}

public class AddCardEffect : ICardEffect
{
    public int Cards { get; set; } = 0;
    public void Handle(string name, Player player = null, Supply supply = null)
    {
        player.Cards.Draw(Cards);
    }
}

#region void Effects
public class VictoryEffect : ICardEffect
{
    public int Points { get; set; } = 0;
    public void Handle(string name, Player player = null, Supply supply = null) {}
}

public class ReactionEffect : ICardEffect
{
    public void Handle(string name, Player player = null, Supply supply = null) {}
}

#endregion