namespace GrupoBlancoChallenge.Application.Dtos
{
    public class ChooseOptionRequest
    {
        public Guid GameSessionId { get; set; }
        public Guid OptionId { get; set; }
    }
}
