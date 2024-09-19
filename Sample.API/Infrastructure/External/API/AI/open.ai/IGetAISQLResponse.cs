
namespace Sample.API.Infrastructure.External.API.AI.open.ai
{
    public interface IGetAISQLResponse
    {
        Task<string> GetSQLAsync(string chat);
    }
}