using OnlineGameStore.Core.Models;
using OnlineGameStore.DB.Entities;
using OnlineGameStore.DB.Repository;

namespace OnlineGameStore.Core.Services
{
    public class GenreService : IGenreService
    {
        private readonly IRepository<Genre> _genreRepository;

        public GenreService(IRepository<Genre> genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task CreateGenreAsync(GenreModel genreModel)
        {
            if (string.IsNullOrEmpty(genreModel.Name))
            {
                throw new ArgumentNullException("genreModel.Name", "Parameter name is required");
            }

            var isGenreExist = await _genreRepository.IsExist(m => m.Name.ToLower() == genreModel.Name.ToLower());

            if (isGenreExist)
            {
                throw new ArgumentException("Genre with such name is already exist");
            }

            var genre = new Genre();

            genre.Name = genreModel.Name;

            await _genreRepository.AddAsync(genre);
        }

        public async Task<string> GetGenreDetails(long genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);

            if (genre == null)
            {
                return null;
            }

            var genreModel = new GenreModel
            { 
                Id = genre.Id,
                Name = genre.Name,
            };

            return genreModel.ToString();
        }
    }
}
