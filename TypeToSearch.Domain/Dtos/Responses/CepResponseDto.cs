namespace TypeToSearch.Domain.Dtos.Responses
{
    public class CepResponseDto
    {
        public string cep { get; set; }
        public string address_type { get; set; }
        public string address_name { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string district { get; set; }
        public string lat { get; set; }
        public string lng { get; set; }
        public string city { get; set; }
        public string city_ibge { get; set; }
        public string ddd { get; set; }
    }
}
