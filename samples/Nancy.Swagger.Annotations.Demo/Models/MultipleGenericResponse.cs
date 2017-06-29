using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nancy.Swagger.Demo.Models
{
    public class MultipleGenericResponse<T, U, V>
    {
        public List<string> Errors { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public U Data2 { get; set; }
        public V Data3 { get; set; }

        public MultipleGenericResponse(T data, U data2, V data3)
        {
            Data = data;
            Data2 = data2;
            Data3 = data3;
        }
    }
}

