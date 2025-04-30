using Mango.Services.Web.Models;
using Mango.Web.Models;
using Mango.Web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;

namespace Mango.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = new List<ProductDto>();
            ResponseDto? response = await _productService.GetAllProductAsync();

            if (response != null && response.IsSuccess==true) 
            {
                
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            else
            {
                TempData["error"] = response?.Message;
            }


            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {
                
                ResponseDto? response = await _productService.CreateProductsAsync(model);

                if (response != null && response.IsSuccess == true)
                {
                    TempData["success"] = "Product created";
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
			ResponseDto? response = await _productService.GetAProductByIdAsync(productId);

			if (response != null && response.IsSuccess == true)
			{
                ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                
                return View(model);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]   //in api it is httpdelete
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            ResponseDto? response = await _productService.DeleteProductsAsync(productDto.ProductId);

            if (response != null && response.IsSuccess == true)
            {
                TempData["success"] = "Product deleted";
                return RedirectToAction(nameof(ProductIndex));

            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(productDto);
        }

		public async Task<IActionResult> ProductEdit(int productId)
		{
			ResponseDto? response = await _productService.GetAProductByIdAsync(productId);

			if (response != null && response.IsSuccess == true)
			{
				ProductDto? model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));

				return View(model);
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return NotFound();
		}

		[HttpPost]   //in api it is httpdelete
		public async Task<IActionResult> ProductEdit(ProductDto productDto)
		{
			ResponseDto? response = await _productService.UpdateProductsAsync(productDto);

			if (response != null && response.IsSuccess == true)
			{
				TempData["success"] = "Product updated";
				return RedirectToAction(nameof(ProductIndex));

			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return View(productDto);
		}
	}
}
