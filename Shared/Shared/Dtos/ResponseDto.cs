using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Shared.Dtos
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; set; }
        [JsonIgnore]
        public bool IsSuccess { get; set; }
        public List<string> Errors { get; set; }

        public static ResponseDto<T> Success(T data, int statusCode)
        {
            return new ResponseDto<T> { Data = data, IsSuccess = true, StatusCode = statusCode };
        }

        public static ResponseDto<T> Success(int statusCode)
        {
            return new ResponseDto<T> { Data = default, IsSuccess = true, StatusCode = statusCode };
        }

        public static ResponseDto<T> Fail(List<string> errors,int statusCode)
        {
            return new ResponseDto<T> { Errors=errors, IsSuccess = false, StatusCode = statusCode };
        }

        public static ResponseDto<T> Fail(string error, int statusCode)
        {
            return new ResponseDto<T> { Errors =new List<string> { error }, IsSuccess = false, StatusCode = statusCode };
        }
    }
}
