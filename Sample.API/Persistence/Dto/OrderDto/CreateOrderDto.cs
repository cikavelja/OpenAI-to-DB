namespace Sample.API.Persistence.Dto.OrderDto
{
    public class CreateOrderDto
    {
        public int CustomerId { get; set; }
        public List<int> ProductIds { get; set; }
    }
}
