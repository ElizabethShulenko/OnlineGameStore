using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using OnlineGameStore.API.Models.Request;
using OnlineGameStore.Core.Models;
using OnlineGameStore.Core.Services;

namespace OnlineGameStore.API.Controllers
{
    [ApiController]
    [Route("api/genres")]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMapper _mapper;

        public GenreController(IGenreService genreService, IMapper mapper)
        {
            _genreService = genreService;
            _mapper = mapper;
        }

        [HttpPost("new")]
        public async Task<IActionResult> CreateGenre([FromBody] GenreRequest genreRequest)
        {
            try
            {
                var genreModel = _mapper.Map<GenreModel>(genreRequest);

                await _genreService.CreateGenreAsync(genreModel);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{ID}")]
        public async Task<IActionResult> GetGenreDetails(long ID)
        {
            var genereDetails = await _genreService.GetGenreDetails(ID);

            if (genereDetails == null)
            {
                return NotFound();
            }

            return Ok(genereDetails);
        }
    }
}
