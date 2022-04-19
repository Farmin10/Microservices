using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Order.Application.Commands;
using Order.Application.Dtos;
using Order.Infrastructure;
using Shared.Dtos;

namespace Order.Application.Handlers
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, ResponseDto<OrderDtoForCreate>>
    {
        private readonly OrderDbContext _context;

        public CreateOrderCommandHandler(OrderDbContext context)
        {
            _context = context;
        }

        public async Task<ResponseDto<OrderDtoForCreate>> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var newAddress = new Domain.OrderAggregate.Address(request.Address.Province, request.Address.District,request.Address.Street, request.Address.ZipCode, request.Address.Line);
            Domain.OrderAggregate.Order newOrder = new(request.BuyerId, newAddress);
            request.OrderItems.ForEach(x =>
            {
                newOrder.AddOrderItem(x.ProductId, x.ProductName, x.Price, x.PictureUrl);
            });

            _context.Orders.Add(newOrder);
            await _context.SaveChangesAsync(cancellationToken);

            return ResponseDto<OrderDtoForCreate>.Success(new OrderDtoForCreate { OrderId = newOrder.Id }, 200);
        }
    }
}
