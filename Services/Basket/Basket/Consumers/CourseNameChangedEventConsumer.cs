using Basket.Services;
using MassTransit;
using Shared.Messages;
using System.Threading.Tasks;

namespace Basket.Consumers
{
    public class CourseNameChangedEventConsumer : IConsumer<BasketNameChangedEvent>
    {
        private readonly IBasketService _basketService;

        public CourseNameChangedEventConsumer(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task Consume(ConsumeContext<BasketNameChangedEvent> context)
        {
            var baskets = await _basketService.GetBasket(context.Message.UserId);
            baskets.Data.BasketItems.ForEach(basket => {
                basket.CourseName = context.Message.UpdatedName;
            });
            await _basketService.SaveOrUpdate(baskets.Data);
        }
    }
}
