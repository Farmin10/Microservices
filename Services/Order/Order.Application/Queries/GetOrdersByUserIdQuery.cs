using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Order.Application.Dtos;
using Shared.Dtos;

namespace Order.Application.Queries
{
    public class GetOrdersByUserIdQuery:IRequest<ResponseDto<List<OrderDto>>>
    {
        public string  UserId { get; set; }
    }
}
