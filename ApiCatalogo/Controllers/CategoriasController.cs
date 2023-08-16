using ApiCatalogo.Context;
using ApiCatalogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiCatalogo.Controllers;

[Route("api/[Controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;

    public CategoriasController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    [HttpGet]
    [ActionName("GetAll")]
    public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    {
        return _context.Categorias.Include(x => x.Produtos).ToList();
    }

    [HttpGet("{id}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id) 
    { 
        var categoria = _context.Categorias.AsNoTracking().FirstOrDefault(p => p.CategoriaId == id);

        if(categoria == null)
        {
            return NotFound();
        }
        return categoria;
    }

    [HttpPost]
    [ActionName("Add")]
    public ActionResult Post([FromBody] Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        _context.SaveChanges();

        return new CreatedAtRouteResult("ObterCategoria", new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id}")]
    [ActionName("Edit")]
    public ActionResult Put(int id, [FromBody] Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }

        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok();
    }

    [HttpDelete("{id}")]
    public ActionResult<Categoria> Delete(int id) 
    {
        var categoria = _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);

        if (categoria == null)
        {
            return NotFound();
        }

        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return categoria;
    }
}
