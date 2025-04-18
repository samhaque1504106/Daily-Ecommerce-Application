using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> GetAllCouponAsync();
        Task<ResponseDto?> GetACouponByIdAsync(int id);
        Task<ResponseDto?> CreateCouponsAsync(CouponDTO coupon);
        Task<ResponseDto?> UpdateCouponsAsync(CouponDTO coupon);
        Task<ResponseDto?> DeleteCouponsAsync(int id);
    }
}
