using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository;

namespace APICatalogo.RepositoryImpl
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(AppDbContext contentx) : base(contentx)
        {
        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(c => c.Preco).ToList();
        }
    }
}
