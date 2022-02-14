namespace BeliVGames.ApiPlayer.Application.Exceptions
{
    public class BadRequestException: ApplicationException
    {

        public BadRequestException(string name, object key): base($"{name} ({key}) is not found")
        {
            
        }
    }
}