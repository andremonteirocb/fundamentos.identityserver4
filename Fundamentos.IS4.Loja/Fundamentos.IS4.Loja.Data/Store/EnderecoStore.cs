using Microsoft.EntityFrameworkCore;
using Fundamentos.IS4.Loja.Data.Context;
using Fundamentos.IS4.Loja.Data.Mappers;
using  Fundamentos.IS4.Loja.Domain.Interfaces;
using  Fundamentos.IS4.Loja.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fundamentos.IS4.Loja.Data.Store
{
    public class EnderecoStore : IEnderecoStore
    {
        internal DbSet<Entities.Endereco> DbSet { get; set; }
        public EnderecoStore(LojaContext context)
        {
            this.DbSet = context.Enderecos;
        }


        public async Task<IEnumerable<Endereco>> ObterDoUsuario(string usuario)
        {
            var enderecos = await DbSet.Where(w => w.Usuario == usuario).ToListAsync();
            return enderecos.Select(s => s.ToModel());
        }
    }
}
