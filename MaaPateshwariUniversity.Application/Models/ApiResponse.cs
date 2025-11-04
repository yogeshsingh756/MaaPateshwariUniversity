using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaaPateshwariUniversity.Application.Models
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "Success";
        public T? Data { get; set; }
        public object? Errors { get; set; }

        public static ApiResponse<T> Ok(T data, string? message = null)
            => new() { Success = true, Data = data, Message = message ?? "Success" };

        public static ApiResponse<T> Fail(string message, object? errors = null)
            => new() { Success = false, Message = message, Errors = errors };
    }
}
