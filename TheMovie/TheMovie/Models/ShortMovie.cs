using Newtonsoft.Json;
using SQLite;

namespace TheMovie.Models
{
    [Table("Movie")]
    public class ShortMovie
    {
        [JsonProperty(PropertyName = "id")]
        [PrimaryKey, Column("id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public double? Rating { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }      
    }
}