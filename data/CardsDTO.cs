using System.Text.Json;
using System.Text.Json.Serialization;

namespace DominionSimulator2.Data;

public class CardDB
{
    [JsonPropertyName("cards")]
    public static CardDTO[] Cards { get; set; } = null;

    public static CardDTO[] Load()
    {
        var filePath = @".\data\cards.json";
        return JsonSerializer.Deserialize<CardDTO[]>(File.ReadAllText(filePath));
    }

    public static void UpdateWeight(int weightDiff, string name) => Cards.First(x => x.Name == name).Weight += weightDiff;

    public static Card GetCard(string name) => new Card(Cards.Where(x => x.Name == name).First());

    public static IEnumerable<Card> GetCards(IEnumerable<string> names) => names.Select(x => GetCard(x));
}

public class CardDTO
{
    public string Name { get; set; } = "";
    public string SetName { get; set; } = "";
    public int Cost { get; set; } = 0;
    public int Weight { get; set; } = 0;

    public string[] Types { get; set; } = null;
    public string[] Effects { get; set; } = null;
}