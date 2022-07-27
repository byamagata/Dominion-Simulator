using System.Text.Json;
using System.Text.Json.Serialization;

namespace DominionSimulator2.Data;

public class CardDB
{
    [JsonPropertyName("cards")]
    public CardDTO[] Cards { get; set; } = null;

    public CardDB Load()
    {
        var filePath = @".\data\cards.json";
        return JsonSerializer.Deserialize<CardDB>(File.ReadAllText(filePath));
    }

    public Card GetCard(string name) => new Card(Cards.Where(x => x.Name == name).First());

    public IEnumerable<Card> GetCards(IEnumerable<string> names) => names.Select(x => GetCard(x));
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