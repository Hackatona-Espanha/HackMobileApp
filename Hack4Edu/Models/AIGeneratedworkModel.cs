using System.Text.Json.Serialization;

namespace Hack4Edu.Models
{
    public class AIGeneratedworkModel
    {
        [JsonPropertyName("img")]
        public string ImageBase64 { get; set; }

        [JsonPropertyName("story")]
        public string Text { get; set; }
    }
}
