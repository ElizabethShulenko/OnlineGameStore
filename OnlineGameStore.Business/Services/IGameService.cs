using OnlineGameStore.Core.Models;

namespace OnlineGameStore.Core.Services
{
    public interface IGameService
    {
        Task CreateGameAsync(GameModel gameModel);
    }
}
