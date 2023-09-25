using AutoMapper;
using OnlineGameStore.Core.Models;
using OnlineGameStore.DB.Entities;
using OnlineGameStore.DB.Repository;
using System.Collections.Generic;

namespace OnlineGameStore.Core.Services
{
    public class GameService : IGameService
    {
        private readonly IRepository<Game> _gameRepository;
        private readonly IMapper _mapper;

        public GameService(IRepository<Game> gameRepository, IMapper mapper)
        {
            _gameRepository = gameRepository;
            _mapper = mapper;
        }

        public async Task CreateGameAsync(GameModel gameModel)
        {
            if (string.IsNullOrEmpty(gameModel.Name))
            {
                throw new ArgumentNullException("gameModel.Name", "Parameter name is required");
            }

            ValidateGameAlias(gameModel);

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

        public async Task DeleteAsync(long gameId)
        {
            var game = await _gameRepository.GetByIdAsync(gameId);

            if (game == null)
            {
                throw new ArgumentNullException("gameId", "There is no game with such ID");
            }

            await _gameRepository.DeleteAsync(game);
        }

        public async Task<string?> GetDescriptionAsync(string gameAlias)
        {
            var game = await GetGameAsync(gameAlias);

            if (game == null)
            {
                return null;
            }

            return game.ToString();
        }

        public async Task<GameModel?> GetGameAsync(string gameAlias)
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

            return result;
        }

        public async Task UpdateAsync(GameModel gameModel)
        {
            if (string.IsNullOrEmpty(gameModel.Name))
            {
                throw new ArgumentNullException("gameModel.Name", "Parameter name is required");
            }

            ValidateGameAlias(gameModel);

            var game = await _gameRepository.GetByIdAsync(gameModel.Id);

            if (game == null)
            {
                throw new ArgumentException("There is no game with such ID");
            }

            game.Name = gameModel.Name;
            game.GameAlias = gameModel.GameAlias;
            game.Description = gameModel.Description;

            await _gameRepository.UpdateAsync(game);
        }

        public async Task<IEnumerable<GameModel>> GetAllGames()
        {
            var result = new List<GameModel>();
            
            var games = await _gameRepository.GetAllAsync();

            foreach (var game in games) 
            {
                result.Add(new GameModel
                {
                    Id = game.Id,
                    Name = game.Name,
                    GameAlias = game.GameAlias,
                    Description = game.Description,
                });
            }

            return result;
        }

        private void ValidateGameAlias(GameModel gameModel)
        {
            if (string.IsNullOrEmpty(gameModel.GameAlias) && !string.IsNullOrEmpty(gameModel.Name))
            {
                gameModel.GameAlias = gameModel.Name.Trim().Replace(" ", "-").ToLower();
            }
        }
    }
}
