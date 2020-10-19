using System.Text.Json.Serialization;

namespace PetData.Entities
{
    public class Pet
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string PetType { get; set; }
    }
}
