using Newtonsoft.Json;

namespace TheMovie.Models
{
    public class Movie
    {
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "year")]
        public int? Year { get; set; }

        [JsonProperty(PropertyName = "director")]
        public string Director { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "actors")]
        public string Actors { get; set; }

        [JsonProperty(PropertyName = "rating")]
        public double? Rating { get; set; }

        [JsonProperty(PropertyName = "poster")]
        public string Poster { get; set; }

        [JsonProperty(PropertyName = "genreId")]
        public int GenreId { get; set; }

        [JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        [JsonIgnore]
        public Xamarin.Forms.ImageSource ImageSource { get; set; }
    }
}
