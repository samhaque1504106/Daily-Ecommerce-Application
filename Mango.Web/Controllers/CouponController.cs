using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace Mango.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO>? list = new List<CouponDTO>();
            ResponseDto? response = await _couponService.GetAllCouponAsync();

            if (response != null && response.IsSuccess==true) 
            {
                list = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
            }


            return View(list);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO model)
        {
            if (ModelState.IsValid)
            {
                
                ResponseDto? response = await _couponService.CreateCouponsAsync(model);

                if (response != null && response.IsSuccess == true)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> CouponDelete(int couponId)
        {
			ResponseDto? response = await _couponService.GetACouponByIdAsync(couponId);

			if (response != null && response.IsSuccess == true)
			{
				CouponDTO? model = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(model);
			}
			return NotFound();
        }

        [HttpPost]   //in api it is httpdelete
        public async Task<IActionResult> CouponDelete(CouponDTO couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponsAsync(couponDto.CouponId);

            if (response != null && response.IsSuccess == true)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            return View(couponDto);
        }
    }
}
