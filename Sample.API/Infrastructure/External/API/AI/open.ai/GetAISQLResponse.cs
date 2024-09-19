﻿using OpenAI_API;

namespace Sample.API.Infrastructure.External.API.AI.open.ai
{
    public class GetAISQLResponse : IGetAISQLResponse
    {
        public async Task<string> GetSQLAsync(string chat)
        {
            var openAi = new OpenAIAPI(new APIAuthentication(""));
            var conversation = openAi.Chat.CreateConversation();

            var testString = @"
                    Using folowing classes, write a SQL query to " +

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

                Without explanation, just raw sql, please";

            conversation.AppendUserInput(testString);

            var response = await conversation.GetResponseFromChatbotAsync();

            return response;
        }

    }
}