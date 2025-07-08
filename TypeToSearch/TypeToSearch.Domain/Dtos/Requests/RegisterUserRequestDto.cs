using System.ComponentModel.DataAnnotations;

namespace TypeToSearch.Domain.Dtos.Requests
{
    public class RegisterUserRequestDto
    {
        [Required]
        public string NmUser { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string PasswordHash { get; set; }
    }
}
