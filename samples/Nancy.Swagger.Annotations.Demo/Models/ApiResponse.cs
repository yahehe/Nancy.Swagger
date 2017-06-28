using System.Collections.Generic;

namespace Nancy.Swagger.Demo.Models
{
    public class ApiResponse<T>
    {
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        public ApiResponse(T data)
        {
            Data = data;
        }
    }
}