using OnlineGameStore.Core.Models;

namespace OnlineGameStore.Core.Services
{
    public interface IGenreService
    {
        Task CreateGenreAsync(GenreModel genreModel);

        Task<string> GetGenreDetails(long genreId);
    }
}
