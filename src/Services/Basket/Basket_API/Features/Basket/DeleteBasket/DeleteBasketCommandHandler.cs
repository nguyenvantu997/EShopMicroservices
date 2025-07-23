
using Basket_API.Data;

namespace Basket_API.Features.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;
    public record DeleteBasketResult(bool IsSuccess);

    public class DeleteBasketCommandHandler(IBasketRepository basketRepository) : ICommandHanlder<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand command, CancellationToken cancellationToken)
        {
            await basketRepository.DeleteBasketAsync(command.UserName, cancellationToken);
            return new DeleteBasketResult(true);
        }
    }
}
