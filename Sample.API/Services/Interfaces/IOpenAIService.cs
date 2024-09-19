namespace Sample.API.Services.Interfaces
{
    public interface IOpenAIService
    {
        Task<dynamic> GetSQLAsync(string chat);
    }
}