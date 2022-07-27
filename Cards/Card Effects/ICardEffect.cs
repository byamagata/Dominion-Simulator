namespace DominionSimulator2;
using System.Text.Json;
using System.Text.Json.Serialization;

public interface ICardEffect
{
    public Player Player { get; set; }
    public Supply Supply { get; set; }
    void Handle();
}

public class AddActionEffect : ICardEffect
{
    public int Actions { get; set; } = 0;
    public Player Player { get; set; } = null;
    public Supply Supply { get; set; } = null;

    public void Handle()
    {
        Player.Actions += Actions;
    }
}

public class AddCoinEffect : ICardEffect
{
    public int Coins { get; set; } = 0;
    public Player Player { get; set; } = null;
    public Supply Supply { get; set; } = null;

    public void Handle()
    {
        Player.Coins += Coins;
    }
}

public class AddBuyEffect : ICardEffect
{
    public int Buys { get; set; } = 0;
    public Player Player { get; set; } = null;
    public Supply Supply { get; set; } = null;

    public void Handle()
    {
        Player.Buys += Buys;
    }
}

public class AddCardEffect : ICardEffect
{
    public int Cards { get; set; } = 0;
    public Player Player { get; set; } = null;
    public Supply Supply { get; set; } = null;

    public void Handle()
    {
        Player.Cards.Draw(Cards);
    }
}

public class VictoryEffect : ICardEffect
{
    public int Points { get; set; } = 0;
    public Player Player { get; set; } = null;
    public Supply Supply { get; set; } = null;

    public void Handle() {}
}