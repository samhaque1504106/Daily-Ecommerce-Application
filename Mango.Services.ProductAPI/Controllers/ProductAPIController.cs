using AutoMapper;
using Azure;
using Mango.Services.ProductAPI.Data; 
using Mango.Services.ProductAPI.Models; 
using Mango.Services.ProductAPI.Models.Dto;  
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.Mvc; 

namespace Mango.Services.CouponAPI.Controllers
{
    [Route("api/product")]
    [ApiController]
    
    public class ProductAPIController : ControllerBase
    {
        private readonly AppDbContext _db;  
        private readonly ResponseDto _response;  
        private IMapper _mapper;

        // Constructor to inject dependencies
        public ProductAPIController(AppDbContext db, IMapper mapper)
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
                IEnumerable<Product> objList = _db.Products.ToList();

                // Set the result of the API response to the list of coupons
                //_response.Result = objList;
                _response.Result = _mapper.Map<IEnumerable<ProductDto>>(objList);

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

        // Get a specific product by ID
        [HttpGet]
        [Route("{id:int}")]  // Define the route with a dynamic parameter, the product ID
        public ResponseDto Get(int id)
        {
            try
            {
                // Fetch the Product with the specified ID from the database
                Product obj = _db.Products.First(u => u.ProductId == id);

                // Set the result of the API response to the found Product
                //_response.Result = obj;
                _response.Result = _mapper.Map<ProductDto>(obj);


                // If the Product is found, the response will contain it in the Result field
            }
            catch (Exception ex)
            {
                // If an error occurs (e.g., Product not found), set the success flag to false
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }

            // Return the response object, which includes the result or error message
            return _response;
        }



        [HttpPost]

        [Authorize(Roles = "ADMIN")]
        public ResponseDto Post([FromBody] ProductDto productDto) 
        { 
            try
            {
                Product obj = _mapper.Map<Product>(productDto); 
                _db.Products.Add(obj); //add obj 
                _db.SaveChanges();   // make sure to save, without it will not be saved
                
                _response.Result = _mapper.Map<ProductDto>(obj); // when we return back we again convert to CouponDTO 
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
        public ResponseDto Put([FromBody] ProductDto productDTO)
        {
            try
            {
                Product obj = _mapper.Map<Product>(productDTO);  
                _db.Products.Update(obj); //update obj 
                _db.SaveChanges();   // make sure to save, without it will not be saved

                _response.Result = _mapper.Map<ProductDto>(obj); // when we return back we again convert to CouponDTO 

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
                Product obj = _db.Products.First(u => u.ProductId == id);
                _db.Products.Remove(obj); //update obj 
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
