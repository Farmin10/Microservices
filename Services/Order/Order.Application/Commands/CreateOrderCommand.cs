using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.Dtos;
using Shared.Dtos;

namespace Order.Application.Commands
{
    public class CreateOrderCommand:IRequest<ResponseDto<OrderDtoForCreate>>
    {
        public string BuyerId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
        public AddressDto Address { get; set; }
    }
}
