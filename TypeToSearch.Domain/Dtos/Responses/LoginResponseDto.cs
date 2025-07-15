namespace TypeToSearch.Domain.Dtos.Responses
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public DateTime Created { get; set; }
        public DateTime Expires { get; set; }
    }
}