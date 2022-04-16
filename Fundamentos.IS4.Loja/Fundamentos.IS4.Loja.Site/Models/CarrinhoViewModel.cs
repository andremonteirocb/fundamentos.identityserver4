using  Fundamentos.IS4.Loja.Domain.Models;
using Fundamentos.IS4.Loja.Domain.ViewObjects;
using System.Collections.Generic;

namespace Fundamentos.IS4.Loja.Site.Models
{
    public class CarrinhoViewModel
    {
        public Carrinho Carrinho { get; set; }
        public IEnumerable<Frete> Fretes { get; set; }
    }
}
