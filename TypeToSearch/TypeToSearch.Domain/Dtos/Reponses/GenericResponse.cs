namespace TypeToSearch.Domain.Dtos.Reponses
{
    public class GenericResponse<T>
    {
        public T? Content { get; set; }
        public string? Msg { get; set; }
        public int StatusCode { get; set; }
    }
}