using APICatalogo.Context;
using APICatalogo.Repository;
using APICatalogo.RepositoryImpl;

namespace APICatalogo.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProdutoRepository produtoRepository;
        private CategoriaRepository categoriaRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProdutoRepository ProdutoRepository
        {
            get
            {
                return produtoRepository = produtoRepository ?? new ProdutoRepository(_context);
            }
        }

        public ICategoriaRepository CategoriaRepository
        {
            get
            {
                return categoriaRepository = categoriaRepository ?? new CategoriaRepository(_context);
            }
        }

        public async Task Commit()
        {
            await  _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
