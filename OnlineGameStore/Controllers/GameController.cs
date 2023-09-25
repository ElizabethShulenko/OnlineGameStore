using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.API.Models.Request;
using OnlineGameStore.Core.Models;
using OnlineGameStore.Core.Services;

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
    }
}
