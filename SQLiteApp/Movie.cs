using SQLite;
using SQLite.Net.Attributes;

namespace SQLiteApp
{
    [Table("Movie")]
    public class Movie
    {
        [PrimaryKey, Column("id")]
        public int Id { get; set; }
        public string Description { get; set; }
        public int? Year { get; set; }
        public string Director { get; set; }
        public string Title { get; set; }
        public string Actors { get; set; }
        public double? Rating { get; set; }
        public string Poster { get; set; }
        public int GenreId { get; set; }

        [Ignore]
        public string Genre { get; set; }
    }
}
