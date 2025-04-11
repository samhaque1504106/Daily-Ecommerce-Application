using AutoMapper;


using Mango.Services.CouponAPI.Models;
using Mango.Services.CouponAPI.Models.Dto;

namespace Mango.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(Config =>
            {
                Config.CreateMap<Coupon, CouponDTO>();
                Config.CreateMap<CouponDTO, Coupon>();
            });
            return mappingConfig;
        }
    }
}
