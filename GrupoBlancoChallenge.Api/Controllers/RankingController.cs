using GrupoBlancoChallenge.Application.Dtos;
using GrupoBlancoChallenge.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrupoBlancoChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly IGameService _gameService;

        public RankingController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RankingResponse>>> GetRanking()
        {
            var result = await _gameService.GetRankingAsync();

            return Ok(result);
        }
    }
}
