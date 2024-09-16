using Sample.API.Persistence.Dto.CustomerDto;
using Sample.API.Persistence.Dto.OrderDetailDto;

namespace Sample.API.Persistence.Dto.OrderDto
{
    public class GetOrderDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public GetCustomerDto Customer { get; set; }
        public List<GetOrderDetailDto> OrderDetails { get; set; }
    }
}
