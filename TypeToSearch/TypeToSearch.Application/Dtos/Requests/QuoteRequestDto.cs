using System.ComponentModel.DataAnnotations;

namespace TypeToSearch.Application.Dtos.Requests
{
    public class QuoteRequestDto
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string CodeIn { get; set; }
    }
}
