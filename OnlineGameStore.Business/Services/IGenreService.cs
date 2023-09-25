using OnlineGameStore.Core.Models;
using OnlineGameStore.DB.Entities;

namespace OnlineGameStore.Core.Services
{
    public interface IGenreService
    {
        Task CreateGenreAsync(GenreModel genreModel);
    }
}
