using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.RepositoryImpl;

namespace APICatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<PagedList<Categoria>> GetCategorias(CategoriaParameters parameters);
        Task<IEnumerable<Categoria>> GetCategoriasProdutos();
    }
}
