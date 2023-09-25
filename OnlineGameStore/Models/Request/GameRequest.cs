using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.API.Models.Request
{
    public class GameRequest
    {
        public long? Id { get; set; }

        public long? GenreId { get; set; }

        public string? GameAlias { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
