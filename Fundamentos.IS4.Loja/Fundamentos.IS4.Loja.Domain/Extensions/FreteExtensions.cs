using Fundamentos.IS4.Loja.Domain.ViewObjects;
using System.Collections.Generic;
using System.Linq;

namespace Fundamentos.IS4.Loja.Domain.Extensions
{
    public static class FreteExtensions
    {
        public static Frete Modalidade(this IEnumerable<Frete> fretes, string modalidade) =>
            fretes.FirstOrDefault(f => f.Modalidade.ToUpper().Equals(modalidade.ToUpper()));
    }
}
