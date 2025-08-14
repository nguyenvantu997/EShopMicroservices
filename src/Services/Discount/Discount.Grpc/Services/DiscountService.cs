using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Services
{
    public class DiscountService
        (DiscountContext dbContext, ILogger<DiscountService> logger) : DiscountProtoService.DiscountProtoServiceBase
    {
        public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon == null)
            {
                coupon = new Models.Coupon() { ProductName = request.ProductName, Description = "No Discount Description", Amount = 0 };
            }

            logger.LogInformation("Discount is retrieved for ProductName : {productName} , Amount : {amount} , Description : {description}",
                coupon.ProductName, coupon.Amount, coupon.Description);

            var couponModel = coupon.Adapt<CouponModel>();
            return couponModel;
        }

        public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
        {
            var coupon = request.Coupon.Adapt<Coupon>();

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            dbContext.Coupons.Add(coupon);

            await dbContext.SaveChangesAsync();

            var counponModel = coupon.Adapt<CouponModel>();

            logger.LogInformation("Discount is successfully created. ProductName : {productName}, Amount : {amount}, Description: {description}",
                counponModel.ProductName, counponModel.Amount, counponModel.Description);

            return counponModel;
        }

        public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.Coupon.ProductName);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            coupon.ProductName = request.Coupon.ProductName;
            coupon.Amount = request.Coupon.Amount;
            coupon.Description = request.Coupon.Description;

            dbContext.Coupons.Update(coupon);

            await dbContext.SaveChangesAsync();

            var counponModel = coupon.Adapt<CouponModel>();

            logger.LogInformation("Discount is successfully updated. ProductName : {productName}, Amount : {amount}, Description: {description}",
                counponModel.ProductName, counponModel.Amount, counponModel.Description);

            return counponModel;
        }

        public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
        {
            var coupon = await dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);

            if (coupon is null)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid request object"));

            dbContext.Coupons.Remove(coupon);
            await dbContext.SaveChangesAsync();

            logger.LogInformation("Discount is successfully deleted. ProductName : {productName}",
               request.ProductName);

            return new DeleteDiscountResponse() { Success = true };
        }

    }
}
