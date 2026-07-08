using GrupoBlancoChallenge.Application.Dtos;
using GrupoBlancoChallenge.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GrupoBlancoChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpPost("start")]
        public async Task<ActionResult<GameSessionResponse>> StartGame(
            [FromBody] StartGameRequest request)
        {
            try
            {
                var result = await _gameService.StartGameAsync(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{gameSessionId:guid}")]
        public async Task<ActionResult<GameSessionResponse>> GetGame(Guid gameSessionId)
        {
            var result = await _gameService.GetGameAsync(gameSessionId);

            if (result is null)
                return NotFound(new { message = "La partida no existe." });

            return Ok(result);
        }

        [HttpPost("choose-option")]
        public async Task<ActionResult<DecisionResultResponse>> ChooseOption(
            [FromBody] ChooseOptionRequest request)
        {
            try
            {
                var result = await _gameService.ChooseOptionAsync(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("sell-company")]
        public async Task<ActionResult<GameSessionResponse>> SellCompany(
            [FromBody] SellCompanyRequest request)
        {
            try
            {
                var result = await _gameService.SellCompanyAsync(request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
