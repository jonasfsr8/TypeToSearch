using TypeToSearch.Domain.Entities;

namespace TypeToSearch.Domain.Interfaces.Repositories
{
    public interface ICombinacaoRepository
    {
        Task<List<Combinacao>> ListAsync();
        Task<bool> CheckCombination(string code);
    }
}
