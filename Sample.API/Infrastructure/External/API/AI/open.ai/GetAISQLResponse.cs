using Microsoft.Extensions.Options;
using OpenAI_API;
using Sample.API.Persistence.Dto.AI;

namespace Sample.API.Infrastructure.External.API.AI.open.ai
{
    public class GetAISQLResponse : IGetAISQLResponse
    {
        private readonly string _openAiApiKey;

        public GetAISQLResponse(IOptions<ExternalAISettings> options)
        {
            _openAiApiKey = options.Value.OpenAI;
        }
        public async Task<string> GetSQLAsync(string chat)
        {
            var openAi = new OpenAIAPI(new APIAuthentication(_openAiApiKey));
            var conversation = openAi.Chat.CreateConversation();

            var testString = @"
                    Using folowing classes, write a MS SQL query to " +

                chat +
              @"  
                public class Customers
                {
                    public int CustomerId { get; set; }
                    public string Name { get; set; }
                    public ICollection<Order> Orders { get; set; }
                }

                public class Products
                {
                    public int ProductId { get; set; }
                    public string ProductName { get; set; }
                    public decimal Price { get; set; }
                    public ICollection<OrderDetail> OrderDetails { get; set; }
                }

                public class Orders
                {
                    public int OrderId { get; set; }
                    public DateTime OrderDate { get; set; }
                    public int CustomerId { get; set; }
                    public Customer Customer { get; set; }
                    public ICollection<OrderDetail> OrderDetails { get; set; }
                }

                public class OrderDetails
                {
                    public int OrderId { get; set; }
                    public int ProductId { get; set; }
                    public int Quantity { get; set; }
                    public Order Order { get; set; }
                    public Product Product { get; set; }
                }

                Without explanation, just raw sql and add FOR JSON AUTO, INCLUDE_NULL_VALUES at the end of SQL script, please.
                If SQL script returns data add FOR JSON AUTO, INCLUDE_NULL_VALUES at the end of SQL script";


            conversation.AppendUserInput(testString);

            var response = await conversation.GetResponseFromChatbotAsync();

            return response;
        }

    }
}
