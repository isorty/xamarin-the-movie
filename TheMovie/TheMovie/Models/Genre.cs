using System.Collections.Generic;
using Newtonsoft.Json;

namespace TheMovie.Models
{
    public class Genre
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "amount")]
        public int Amount { get; set; }

        [JsonProperty(PropertyName = "movies")]
        public List<Movie> Movies { get; set; }      
    }
}