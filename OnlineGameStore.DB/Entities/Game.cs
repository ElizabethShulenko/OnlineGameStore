using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineGameStore.DB.Entities
{
    public class Game : Entity
    {
        [Required]
        public string? GameAlias { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public long? GenreId { get; set; }

        public Genre? Genre { get; set; }
    }
}
