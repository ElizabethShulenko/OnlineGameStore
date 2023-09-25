using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.DB.Entities
{
    public class Genre : Entity
    {
        public string Name { get; set; }

        public List<Game> Games { get; set; }
    }
}
