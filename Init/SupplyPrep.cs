using DominionSimulator2.Data;

namespace DominionSimulator2;

/// <summary>
/// Handles preparing the supply for the next game.
/// </summary>
public class SupplyPrep
{
    public CardDB CardDB { get; set; } = null;

    public SupplyPrep(CardDB cardDB)
    {
        CardDB = cardDB;
    }

    public Supply CreateSupply()
    {
        var supply = new Supply();
        
        // Treasures
        supply.AddTreasure(CardDB.GetCard("Copper"), 0, 60);
        supply.AddTreasure(CardDB.GetCard("Silver"), 0, 40);
        supply.AddTreasure(CardDB.GetCard("Gold"), 0, 30);

        // Victory
        supply.AddVictory(CardDB.GetCard("Estate"), 0, 24);
        supply.AddVictory(CardDB.GetCard("Duchy"), 0, 12);
        supply.AddVictory(CardDB.GetCard("Province"), 0, 12);
        supply.AddVictory(CardDB.GetCard("Curse"), 0, 40);

        // Kingdom
        supply.AddKingdom(CardDB.GetCard("Market"), 0, 10);
        supply.AddKingdom(CardDB.GetCard("Smithy"), 0, 10);
        supply.AddKingdom(CardDB.GetCard("Village"), 0, 10);
        supply.AddKingdom(CardDB.GetCard("Chapel"), 0, 10);
        supply.AddKingdom(CardDB.GetCard("Workshop"), 0, 10);
        supply.AddKingdom(CardDB.GetCard("Mine"), 0, 10);
        supply.AddKingdom(CardDB.GetCard("Throne Room"), 0, 10);
        supply.AddKingdom(CardDB.GetCard("Remodel"), 0, 10);
        supply.AddKingdom(CardDB.GetCard("Moat"), 0, 10);

        return supply;
    }

}