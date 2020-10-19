using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PetData.Entities
{
    public class Owner
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("gender")]
        public string Gender { get; set; }
        [JsonPropertyName("age")]
        public int Age { get; set; }
        public string City { get; set; }
        [JsonPropertyName("pets")]
        public IEnumerable<Pet> Pets { get; set; }
    }
}
