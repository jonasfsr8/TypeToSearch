using System.ComponentModel.DataAnnotations;

namespace TypeToSearch.Application.Dtos.Requests
{
    public class LoginRequestDto
    {
        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
