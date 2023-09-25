namespace OnlineGameStore.Core.Models
{
    public class GameModel
    {
        public long Id { get; set; }

        public string? GameAlias { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, GameAlias: {GameAlias}, Name: {Name}, Description: {Description}";
        }
    }    
}
