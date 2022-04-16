using  Fundamentos.IS4.Loja.Domain.Models;
using Fundamentos.IS4.Loja.Domain.ViewObjects;
using System.Collections.Generic;
using System.Linq;

namespace Fundamentos.IS4.Loja.Site.Models
{
    public class ProdutoDetalhesViewModel
    {
        public Produto Produto { get; set; }
        public IEnumerable<Produto> ProdutosRelacionados { get; set; }
        public IEnumerable<DetalhesFrete> OpcoesFrete { get; set; }

        public double NotaMedia()
        {
            return (double)Produto.Avaliacoes.Sum(s => s.Nota) / (double)Produto.Avaliacoes.Count();
        }
    }
}
