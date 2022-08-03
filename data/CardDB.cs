using System.Text.Json;
using System.Text.Json.Serialization;

namespace DominionSimulator2.Data;

public class CardDB
{
    const string filePath = @".\data\CardDB.json";
    private static double learnRate = 0.05;
    private static double decayRate = 0.01;

    public static Dictionary<string, CardDTO> Cards { get; set; } = null;

    public static void Init()
    {
        var cards = Load();
        Cards = new Dictionary<string, CardDTO>();
        cards.ToList().ForEach(cards => Cards.Add(cards.Name, cards));
    }
    public static CardDTO[] Load() => JsonSerializer.Deserialize<CardDTO[]>(File.ReadAllText(filePath));
    public static void Save()
    {
        var oldCards = Load();
        var OldCardsDict = new Dictionary<string, CardDTO>();
        oldCards.ToList().ForEach(cards => OldCardsDict.Add(cards.Name, cards));
        OldCardsDict.Keys.ToList().ForEach(key => {
            if (Cards.ContainsKey(key))
            {
                OldCardsDict[key].Weight = OldCardsDict[key].Weight + (learnRate * Cards[key].Weight);
            }
        });
        File.WriteAllText(filePath, JsonSerializer.Serialize(OldCardsDict.Values.ToArray()));
    }

    public static void UpdateWeight(int weightDiff, string name) => Cards[name].Weight += weightDiff;

    public static Card GetCard(string name) => new Card(Cards[name]);

    public static IEnumerable<Card> GetCards(IEnumerable<string> names) => names.Select(x => GetCard(x));

    public static void ResetDB()
    {
        var cards = Load();
        cards.ToList().ForEach(cards => cards.Weight = 0);
        File.WriteAllText(filePath, JsonSerializer.Serialize(cards));
    }
}

public class CardDTO
{
    public string Name { get; set; } = "";
    public string SetName { get; set; } = "";
    public int Cost { get; set; } = 0;
    public double Weight { get; set; } = 0;

    public string[] Types { get; set; } = null;
    public string[] Effects { get; set; } = null;
}