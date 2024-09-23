using Microsoft.Extensions.Options;
using Microsoft.SqlServer.Management.Smo;
using OpenAI_API;
using Sample.API.Persistence.Dto.AI;
using Sample.API.Services.Interfaces;

namespace Sample.API.Infrastructure.External.API.AI.open.ai
{
    public class GetAISQLResponse : IGetAISQLResponse
    {
        private readonly string _openAiApiKey;
        private readonly IDbRepresentationService _dbSchema;
        
        public GetAISQLResponse(IOptions<ExternalAISettings> options, IDbRepresentationService dbSchema)
        {
            _openAiApiKey = options.Value.OpenAI;
            _dbSchema = dbSchema;
        }
        public async Task<string> GetSQLAsync(string chat)
        {
            var sql = await _dbSchema.GenerateSchemaScriptAsync();
            
            var openAi = new OpenAIAPI(new APIAuthentication(_openAiApiKey));
            var conversation = openAi.Chat.CreateConversation();
            //Set prompt for the conversation
            var testString = @"
                    Using folowing sql representation, write a MS SQL query to " +

                chat + 
                " " + 
                sql +
              @"  Without explanation, just raw sql, please.
                If SQL script returns data add FOR JSON AUTO, INCLUDE_NULL_VALUES at the end of SQL script";


            conversation.AppendUserInput(testString);

            var response = await conversation.GetResponseFromChatbotAsync();

            return response;
        }

    }
}
