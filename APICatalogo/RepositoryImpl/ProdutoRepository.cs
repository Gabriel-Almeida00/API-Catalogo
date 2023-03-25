using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
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

        public async  Task<IEnumerable<Produto>> GetProdutoByNome(string nome)
        {
            return await Get().Where(x => x.Nome.Contains(nome)).ToListAsync();
        }

        public async Task<PagedList<Produto>> GetProdutos(ProdutosParameters produtosParameters)
        {
            return await PagedList<Produto>.ToPagedList(Get().OrderBy(on => on.Nome),
                produtosParameters.PageNumber, produtosParameters.PageSize);
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorPreco()
        {
            return await Get().OrderBy(c => c.Preco).ToListAsync();
        }
    }
}
