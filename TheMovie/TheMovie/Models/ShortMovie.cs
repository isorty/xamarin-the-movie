using Newtonsoft.Json;

namespace TheMovie.Models
{
    public class ShortMovie
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public double? Rating { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }      
    }
}