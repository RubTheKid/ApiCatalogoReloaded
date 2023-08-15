using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace ApiCatalogo.Models;

public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
    [Key]
    public int CategoriaId { get; set; }

    [Required]
    public string Nome { get; set; }

    
    public string ImagemUrl { get; set; }
    public ICollection<Produto> Produtos { get; set; }
}
