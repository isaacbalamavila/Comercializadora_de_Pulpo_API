namespace comercializadora_de_pulpo_api.Models
{
    public class Response<T>
    {
        required public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public ErrorResponse? Error {get; set;}

        // Helper para respuesta exitosa
        public static Response<T> Ok(T data, int statusCode = 200)
        {
            return new Response<T>
            {
                IsSuccess = true,
                StatusCode = statusCode,
                Data = data
            };
        }

        // Helper para respuesta fallida
        public static Response<T> Fail( string message, string? details = null, int statusCode = 500)
        {
            return new Response<T>
            {
                IsSuccess = false,
                StatusCode = statusCode,
                Error = new ErrorResponse
                {
                    Message = message,
                    ErrorDetails = details
                }
            };
        }

        public class ErrorResponse
        {
            required public string Message { get; set; } = null!;
            required public string? ErrorDetails { get; set; } = null!;

        }
    }
}
