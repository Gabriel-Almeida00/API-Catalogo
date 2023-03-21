using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.RepositoryImpl
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext contentx) : base(contentx)
        {
        }

        public IEnumerable<Categoria> GetCategoriasProdutos()
        {
            return Get().Include(x => x.Produtos);
        }
    }
}
