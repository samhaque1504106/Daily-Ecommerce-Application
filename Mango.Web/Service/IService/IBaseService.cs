using Mango.Web.Models;

namespace Mango.Web.Service.IService
{
    // Define an interface named IBaseService
    public interface IBaseService
    {
        // Declare an asynchronous method named SendAsync that takes a RequestDto as input
        // and returns a Task that wraps a nullable ResponseDto
        Task<ResponseDto?> SendAsync(RequestDto requestDto, bool withBearer = true);
    }
}