﻿using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly IUnitOfWork _uof;
        public CategoriasController(IUnitOfWork context)
        {
            _uof = context;
        }

        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
        {
            try
            {
                return _uof.Categorias.Include(p => p.Produtos).ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                return _uof.Categorias.AsNoTracking().ToList();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema ao tratar a sua solicitação.");
            }

        }

        [HttpGet("{id:int}", Name = "ObterCategoria")]
        public ActionResult<Categoria> Get(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"Categoria com id= {id} não encontrada...");
                }
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                           "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPost]
        public ActionResult Post(Categoria categoria)
        {
            try
            {
                if (categoria is null)
                    return BadRequest("Dados inválidos");

                _uof.CategoriaRepository.Add(categoria);
                _uof.Commit();

                return new CreatedAtRouteResult("ObterCategoria",
                    new { id = categoria.CategoriaId }, categoria);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                          "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                {
                    return BadRequest("Dados inválidos");
                }
                _uof.CategoriaRepository.Update(categoria);
                _uof.Commit();
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                       "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _uof.CategoriaRepository.GetById(p => p.CategoriaId == id);

                if (categoria == null)
                {
                    return NotFound($"Categoria com id={id} não encontrada...");
                }
                _uof.CategoriaRepository.Delete(categoria);
                _uof.Commit();
                return Ok(categoria);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                               "Ocorreu um problema ao tratar a sua solicitação.");
            }
        }
    }
}
