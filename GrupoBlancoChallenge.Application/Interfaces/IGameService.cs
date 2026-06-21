using GrupoBlancoChallenge.Application.Dtos;

namespace GrupoBlancoChallenge.Application.Interfaces
{
    public interface IGameService
    {
        Task<GameSessionResponse> StartGameAsync(StartGameRequest request);
        Task<GameSessionResponse?> GetGameAsync(Guid gameSessionId);
        Task<DecisionResultResponse> ChooseOptionAsync(ChooseOptionRequest request);
        Task<List<RankingResponse>> GetRankingAsync();
    }
}
