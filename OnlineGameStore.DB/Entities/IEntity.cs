using System.ComponentModel.DataAnnotations;

namespace OnlineGameStore.DB.Entities
{
    public interface IEntity
    {
        [Key]
        long Id { get; set; }
    }
}
