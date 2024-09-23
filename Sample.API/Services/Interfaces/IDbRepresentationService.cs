namespace Sample.API.Services.Interfaces
{
    public interface IDbRepresentationService
    {
        Task<string> GenerateSchemaScriptAsync();
    }
}