using  Fundamentos.IS4.Loja.Domain.Models;
using System.Collections.Generic;
using Fundamentos.IS4.Loja.Domain.ViewObjects;

namespace Fundamentos.IS4.Loja.Site.Models
{
    public class ProdutosPrincipalViewModel
    {
        public ListOf<Produto> Produtos { get; set; }
        public string ImagemCapa { get; set; }
        public string Titulo { get; set; }
        public string SubTitulo { get; set; }
        public List<Categoria> Categorias { get; set; }
        public IEnumerable<Marca> Marcas { get; set; }
        public PesquisarProdutoVo PesquisaAtual { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
    }
}
