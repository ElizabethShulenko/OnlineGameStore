using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.DB.Entities
{
    public class Game : Entity
    {

        [Required]
        public string? GameAlias { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Description { get; set; }
    }
}
