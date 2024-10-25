using System.Text.Json.Serialization;

namespace Hack4Edu.Models
{
    public class AIGeneratedworkModel
    {
        [JsonPropertyName("img")]
        public string ImageBase64 { get; set; }

        [JsonPropertyName("story")]
        public string Text { get; set; }

        public Stream ImageStream { get; set; }
        public ImageSource Image => ImageSource.FromStream(() => new MemoryStream(Convert.FromBase64String(ImageBase64)));

        [JsonPropertyName("audio")]
        public string AudioBase64 { get; set; }

        public Stream AudioStream { get; set; }

        public void ConvertAudioStream()
        {
            if (!string.IsNullOrEmpty(AudioBase64))
            {
                AudioStream = new MemoryStream(Convert.FromBase64String(AudioBase64));
            }
        }
    }
}
