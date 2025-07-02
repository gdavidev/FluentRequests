using System.Text.Json.Serialization;

namespace Examples.PokeAPIClient.Data
{
    internal class Pokemon
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        [JsonPropertyName("base_experience")] 
        public int BaseExperience { get; set; }

        public string Height { get; set; } = string.Empty;

        [JsonPropertyName("is_default")]
        public bool IsDefault { get; set; }
        
        public int Order { get; set; }
        
        public int Weight { get; set; }
    }
}
