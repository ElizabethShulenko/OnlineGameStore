using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.API.Models.Request;
using OnlineGameStore.Core.Models;
using OnlineGameStore.Core.Services;
using System.Text;

namespace OnlineGameStore.Web.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GameController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateGame([FromBody] GameRequest gameRequest)
        {
            try
            {
                var gameModel = _mapper.Map<GameModel>(gameRequest);

                await _gameService.CreateGameAsync(gameModel);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{gamealias}")]
        public async Task<IActionResult> GetGameDetails(string gamealias)
        {
            var gameDetails = await _gameService.GetDescriptionAsync(gamealias);

            if (gameDetails == null)
            {
                return NotFound();
            }

            return Ok(gameDetails);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateGame([FromBody] GameRequest gameRequest)
        {
            try
            {
                if (gameRequest.Id == null)
                {
                    return NotFound();
                }

                var gameModel = _mapper.Map<GameModel>(gameRequest);

                await _gameService.UpdateAsync(gameModel);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("remove")]
        public async Task<IActionResult> DeleteGame(long gameId)
        {
            try
            {
                if (gameId < 0)
                {
                    return BadRequest();
                }

                await _gameService.DeleteAsync(gameId);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{gameAlias}/download")]
        public async Task<IActionResult> DownloadGame(string gameAlias)
        {
            try
            {
                var gameInfo = await _gameService.GetDescriptionAsync(gameAlias);

                if (gameInfo == null)
                {
                    return NotFound();
                }

                var game = await _gameService.GetGameAsync(gameAlias);

                string fileName = $"{game.Name}_{DateTime.UtcNow:yyyy/MM/dd/HH/mm/ss}.txt";

                byte[] fileBytes = Encoding.UTF8.GetBytes(gameInfo);

                return File(fileBytes, "text/plain", fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGamesAsync()
        {
            try
            {                
                var games = await _gameService.GetAllGames();
                               
                return Ok(games);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
