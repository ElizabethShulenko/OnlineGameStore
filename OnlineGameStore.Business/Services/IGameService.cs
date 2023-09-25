using OnlineGameStore.Core.Models;

namespace OnlineGameStore.Core.Services
{
    public interface IGameService
    {
        Task CreateGameAsync(GameModel gameModel);

        Task<string?> GetDescriptionAsync(string gameAlias);

        Task UpdateAsync(GameModel gameModel);
    }
}
