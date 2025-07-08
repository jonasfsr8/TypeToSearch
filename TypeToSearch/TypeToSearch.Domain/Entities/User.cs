namespace TypeToSearch.Domain.Entities
{
    public class User
    {
        public int CdUser { get; set; }
        public string NmUser { get; set; }
        public string Login { get; set; }
        public string PasswordHash { get; set; }
        public DateTime DtCriacao { get; set; }
    }
}