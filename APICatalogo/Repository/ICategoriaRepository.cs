using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.RepositoryImpl;

namespace APICatalogo.Repository
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategorias(CategoriaParameters parameters);
        IEnumerable<Categoria> GetCategoriasProdutos();
    }
}
