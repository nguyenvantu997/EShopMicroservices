using Basket_API.Data;
using Basket_API.Dtos;
using BuildingBlocks.Message.Events;
using MassTransit;

namespace Basket_API.Features.Basket.CheckoutBasket
{
    public record CheckoutBasketCommand(BasketCheckoutDto Dto) : ICommand<CheckoutBasketResult>;
    public record CheckoutBasketResult(bool IsSuccess);
    public class CheckoutBasketValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketValidator()
        {
            RuleFor(x => x.Dto).NotNull().WithMessage("BasketCheckoutDto can't be null");
            RuleFor(x => x.Dto.Username).NotEmpty().WithMessage("Username is required");
        }
    }

    public class CheckoutBasketHandler(IBasketRepository basketRepository, IPublishEndpoint publishEndpoint) : ICommandHanlder<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            //281
            var basket = await basketRepository.GetBasketAsync(command.Dto.Username, cancellationToken);

            if (basket == null)
                return new CheckoutBasketResult(false);

            var eventMessage = command.Dto.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await basketRepository.DeleteBasketAsync(command.Dto.Username, cancellationToken);
            return new CheckoutBasketResult(true);
        }
    }
}
