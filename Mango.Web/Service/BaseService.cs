using Mango.Web.Models;
using Mango.Web.Service.IService;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using static Mango.Web.Utility.SD;

namespace Mango.Web.Service
{
    // This class implements the IBaseService interface, meaning it must provide an implementation for SendAsync
    public class BaseService : IBaseService
    {
        // Field to hold the HTTP client factory for creating HttpClient instances
        private readonly IHttpClientFactory _httpClientFactory;

        // Constructor with dependency injection of IHttpClientFactory
        public BaseService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // This method sends an HTTP request asynchronously and returns a ResponseDto or null
        public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
        {
            try
            {
                // Create an HttpClient named "MangoApi" from the factory
                HttpClient client = _httpClientFactory.CreateClient("MangoApi");

                // Create a new HTTP request
                HttpRequestMessage message = new();

                // Set header to indicate the request body is JSON
                message.Headers.Add("Accept", "application/json");

                // (Optional) Token logic would go here

                // Set the target URL of the request
                message.RequestUri = new Uri(requestDto.Url);

                // If there's data to send in the body (for POST/PUT), serialize it as JSON and attach
                if (requestDto.Data != null)
                {
                    message.Content = new StringContent(
                        JsonConvert.SerializeObject(requestDto.Data), // Serialize data to JSON
                        Encoding.UTF8,                                // Use UTF-8 encoding
                        "application/json"                            // Set content type
                    );
                }

                // Declare variable for the response
                HttpResponseMessage? apiResponse = null;

                // Set the HTTP method depending on the ApiType (GET, POST, PUT, DELETE)
                switch (requestDto.ApiType)
                {
                    case ApiType.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiType.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiType.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                // Send the request asynchronously and wait for the response
                apiResponse = await client.SendAsync(message);

                // Handle response codes
                switch (apiResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSuccess = false, Message = "not found!" };

                    case HttpStatusCode.Forbidden:
                        return new() { IsSuccess = false, Message = "access denied!" };

                    case HttpStatusCode.Unauthorized:
                        return new() { IsSuccess = false, Message = "unauthorized!" };

                    case HttpStatusCode.InternalServerError:
                        return new() { IsSuccess = false, Message = "internal server error!" };

                    // For success or other codes, read the response content
                    default:
                        var apiContent = await apiResponse.Content.ReadAsStringAsync(); // Read body as string
                        var apiResponseDto = JsonConvert.DeserializeObject<ResponseDto>(apiContent); // Convert JSON back to object
                        return apiResponseDto; // Return the deserialized result
                }
            }
            catch (Exception ex)
            {
                // If any error occurs, return a ResponseDto with error message
                var Dto = new ResponseDto
                {
                    Result = ex.Message.ToString(), // Store exception message
                    IsSuccess = false
                };
                return Dto;
            }
        }
    }
}
