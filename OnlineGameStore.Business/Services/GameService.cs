using OnlineGameStore.Core.Models;
using OnlineGameStore.DB.Entities;
using OnlineGameStore.DB.Repository;

namespace OnlineGameStore.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;

        public GameService(IRepository<Game> gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public async Task CreateGameAsync(GameModel gameModel)
        {
            if (string.IsNullOrEmpty(gameModel.Name))
            {
                throw new ArgumentNullException("gameModel.Name", "Parameter name is required");
            }

            if (string.IsNullOrEmpty(gameModel.GameAlias))
            {
                gameModel.GameAlias = gameModel.Name.Trim().Replace(" ", "-").ToLower();
            }

            var isGameAliasExist = await _gameRepository.IsExist(m => m.GameAlias.ToLower() == gameModel.GameAlias.ToLower());

            if (isGameAliasExist)
            {
                throw new Exception("Game with such alias is already exist");
            }

            var game = new Game();

            game.Name = gameModel.Name;
            game.GameAlias = gameModel.GameAlias;
            game.Description = gameModel.Description;

            await _gameRepository.AddAsync(game);
        }

        public async Task<string?> GetDescriptionAsync(string gameAlias)
        {
            var game = await _gameRepository.SingleOrDefaultAsync(m => m.GameAlias.ToLower() == gameAlias.ToLower());

            if (game == null)
            {
                return null;
            }

            var result = new GameModel
            {
                Id = game.Id,
                Name = game.Name,
                GameAlias = game.GameAlias,
                Description = game.Description,
            };

            return result.ToString();
        }
    }
}
