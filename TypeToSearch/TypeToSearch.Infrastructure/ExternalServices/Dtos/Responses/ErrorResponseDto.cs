namespace TypeToSearch.Infrastructure.ExternalServices.Dtos.Responses
{
    public class ErrorResponseDto
    {
        public int status { get; set; }
        public string code { get; set; }
        public string message { get; set; }
    }
}