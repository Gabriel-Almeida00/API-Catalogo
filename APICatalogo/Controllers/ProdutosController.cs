using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.Models;
using APICatalogo.Pagination;
using APICatalogo.Repository;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        public ProdutosController(IUnitOfWork contexto, IMapper mapper)
        {
            _uof = contexto;
            _mapper = mapper;
        }

        [HttpGet("{nome}")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutoByNome(string nome)
        {
            var produto = await _uof.ProdutoRepository.GetProdutoByNome(nome);
            var produtoDTO = _mapper.Map<List<ProdutoDTO>>(produto); 

            return produtoDTO;
        }


        [HttpGet("menorpreco")]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutosPreco()
        {
            var produtos = await _uof.ProdutoRepository.GetProdutosPorPreco();
            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);

            return produtosDTO;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoDTO>>> Get([FromQuery] ProdutosParameters produtosParameters)
        {
            var produtos = await _uof.ProdutoRepository.GetProdutos(produtosParameters);

            var metadata = new
            {
                produtos.TotalCount,
                produtos.PageSize,
                produtos.CurrentPage,
                produtos.TotalPage,
                produtos.HasNext,
                produtos.HasPrevius
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));


            var produtosDTO = _mapper.Map<List<ProdutoDTO>>(produtos);
            return produtosDTO;
        }

        [HttpGet("{id:int}", Name = "ObterProduto")]
        public async Task<ActionResult<ProdutoDTO>> GetById(int id)
        {
            var produto = await _uof.ProdutoRepository.GetById(p=> p.ProdutoId ==id);
            if (produto is null)
            {
                return NotFound("Produto não encontrado...");
            }

            var produtoDTO = _mapper.Map<ProdutoDTO>(produto);
            return produtoDTO;
        }

        [HttpPost]
        public async Task<ActionResult> Post(ProdutoDTO produtoDTO)
        {
            if (produtoDTO is null)
                return BadRequest();

            var produto = _mapper.Map<Produto>(produtoDTO);

            _uof.ProdutoRepository.Add(produto);
            await _uof.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return new CreatedAtRouteResult("ObterProduto",
                new { id = produto.ProdutoId }, produtoDto);
        }

        [HttpPut("{id:int}")]
        //Usar [FromBody] é opcional
        public async Task<ActionResult> Put(int id, ProdutoDTO produtoDTO)
        {
            if (id != produtoDTO.ProdutoId)
            {
                return BadRequest();
            }

            var produto = _mapper.Map<Produto>(produtoDTO);

            _uof.ProdutoRepository.Update(produto);
            await _uof.Commit();

            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ProdutoDTO>> Delete(int id)
        {
            var produto = await _uof.ProdutoRepository.GetById(p => p.ProdutoId == id);
            //var produto = _uof.Produtos.Find(id);

            if (produto is null)
            {
                return NotFound("Produto não localizado...");
            }
            _uof.ProdutoRepository.Delete(produto);
            await _uof.Commit();

            var produtoDto = _mapper.Map<ProdutoDTO>(produto);

            return Ok(produtoDto);
        }
    }
}
