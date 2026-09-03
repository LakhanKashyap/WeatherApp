namespace WeatherApp.Api.DTOs
{

    //DTO used to return consistent error responses from our API
    public class ErrorResponse
    {
        public string Error { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;

    }
}
