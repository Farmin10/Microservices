using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Order.Application.Dtos;
using Order.Application.Mappings;
using Order.Application.Queries;
using Order.Infrastructure;
using Shared.Dtos;

namespace Order.Application.Handlers
{
    public class GetOrdersByUserIdQueryHandler : IRequestHandler<GetOrdersByUserIdQuery, ResponseDto<List<OrderDto>>>
    {
        private readonly OrderDbContext _context;

        public GetOrdersByUserIdQueryHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<List<OrderDto>>> Handle(GetOrdersByUserIdQuery request, CancellationToken cancellationToken)
        {
            var orders = await _context.Orders.Include(x => x.OrderItems).Where(x => x.BuyerId == request.UserId).ToListAsync(cancellationToken);
            if (!orders.Any())
            {
                return ResponseDto<List<OrderDto>>.Success(new List<OrderDto>(), 200);
            }

            var orderDto = ObjectMapper.Mapper.Map<List<OrderDto>>(orders);
            return ResponseDto<List<OrderDto>>.Success(orderDto, 200);
        }
    }
}
