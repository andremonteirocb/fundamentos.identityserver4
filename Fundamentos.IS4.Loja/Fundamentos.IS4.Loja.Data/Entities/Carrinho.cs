using Fundamentos.IS4.Loja.Domain.Models;
using Fundamentos.IS4.Loja.Domain.ViewObjects;
using System.Collections.Generic;

namespace Fundamentos.IS4.Loja.Data.Entities
{
    internal class Carrinho
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string Cupom { get; set; }
        public decimal Desconto { get; set; }
        public decimal PercentualDesconto { get; set; }
        public Frete Frete { get; set; }
        public ICollection<ItemCarrinho> CarrinhoProdutos { get; set; }

        public void Atualizar(Domain.Models.Carrinho carrinho)
        {
            Cupom = carrinho.Cupom;
            Desconto = carrinho.Desconto;
            PercentualDesconto = carrinho.PercentualDesconto;
            Frete = carrinho.Frete;
        }

    }
}
