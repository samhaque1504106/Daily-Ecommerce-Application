using AutoMapper;
using Azure;
using Mango.Services.CouponAPI.Data;  // Namespace for the database context (AppDbContext)
using Mango.Services.CouponAPI.Models;  // Namespace for your Coupon model
using Mango.Services.CouponAPI.Models.Dto;  // Namespace for the ResponseDto (Data Transfer Object)
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;  // ASP.NET Core classes related to HTTP
using Microsoft.AspNetCore.Mvc;  // ASP.NET Core MVC classes

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponAPIController : ControllerBase
    {
        private readonly AppDbContext _db;  // The database context for interacting with the database
        private readonly ResponseDto _response;  // A DTO used to standardize API responses
        private IMapper _mapper;

        // Constructor to inject dependencies
        public CouponAPIController(AppDbContext db, IMapper mapper)
        {
            _db = db;  
            _response = new ResponseDto();  
            _mapper = mapper; 
        }

        // Get all coupons
        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                // Fetch all coupons from the database
                IEnumerable<Coupon> objList = _db.Coupons.ToList();

                // Set the result of the API response to the list of coupons
                //_response.Result = objList;
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);

                // If successful, the response will contain the coupons list in the Result field
            }
            catch (Exception ex)
            {
                // In case of an error, set the success flag to false and add the error message
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            // Return the response object, which includes the result or error message
            return _response;
        }

        // Get a specific coupon by ID
        [HttpGet]
        [Route("{id:int}")]  // Define the route with a dynamic parameter, the coupon ID
        public ResponseDto Get(int id)
        {
            try
            {
                // Fetch the coupon with the specified ID from the database
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);

                // Set the result of the API response to the found coupon
                //_response.Result = obj;
                _response.Result = _mapper.Map<CouponDTO>(obj);


                // If the coupon is found, the response will contain it in the Result field
            }
            catch (Exception ex)
            {
                // If an error occurs (e.g., coupon not found), set the success flag to false
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            // Return the response object, which includes the result or error message
            return _response;
        }


        [HttpGet]
        [Route("GetByCode/{code}")]  // Define the route with a dynamic parameter, the coupon ID
        public ResponseDto GetByCode(string code)
        {
            try
            {
                // Fetch the coupon with the specified ID from the database
                Coupon obj = _db.Coupons.First(u => u.CouponCode.ToLower() == code.ToLower());

                // Set the result of the API response to the found coupon
                //_response.Result = obj;
                _response.Result = _mapper.Map<CouponDTO>(obj);


                // If the coupon is found, the response will contain it in the Result field
            }
            catch (Exception ex)
            {
                // If an error occurs (e.g., coupon not found), set the success flag to false
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            // Return the response object, which includes the result or error message
            return _response;
        }


        [HttpPost]

        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] CouponDTO couponDto) 
        { 
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDto);  // getting CouponDTO FromBody which is POST method body, mapping this to Coupon to save
                _db.Coupons.Add(obj); //add obj 
                _db.SaveChanges();   // make sure to save, without it will not be saved
                
                _response.Result = _mapper.Map<CouponDTO>(obj); // when we return back we again convert to CouponDTO 
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]

        [Authorize(Roles = "ADMIN")]
        public ResponseDto Put([FromBody] CouponDTO couponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);  // getting CouponDTO FromBody which is POST method body, mapping this to Coupon to save
                _db.Coupons.Update(obj); //update obj 
                _db.SaveChanges();   // make sure to save, without it will not be saved

                _response.Result = _mapper.Map<CouponDTO>(obj); // when we return back we again convert to CouponDTO 

            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete]
        [Route("{id:int}")]
        [Authorize(Roles = "ADMIN")]
        public ResponseDto Delete(int id)
        {
            try
            {
                Coupon obj = _db.Coupons.First(u => u.CouponId == id);
                _db.Coupons.Remove(obj); //update obj 
                _db.SaveChanges();   // make sure to save, without it will not be saved

              
            }

            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
