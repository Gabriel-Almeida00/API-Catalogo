using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.RepositoryImpl
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly AppDbContext contentx;
        public ProdutoRepository(AppDbContext contentx) : base(contentx)
        {
            contentx = contentx;
        }

        public  IEnumerable<Produto> GetProdutoByNome(string nome)
        {
            return Get().Where(x => x.Nome.Contains(nome));
        }

        public IEnumerable<Produto> GetProdutosPorPreco()
        {
            return Get().OrderBy(c => c.Preco).ToList();
        }
    }
}
