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
    public class PedidoStore : IPedidoStore
    {
        internal LojaContext Db { get; set; }
        internal DbSet<Entities.Pedido> DbSet { get; set; }
        public PedidoStore(LojaContext context)
        {
            this.Db = context;
            this.DbSet = context.Pedidos;
        }



        public async Task SalvarPedido(Pedido pedido, string usuario)
        {
            var entity = pedido.ToEntity();
            entity.Usuario = usuario;
            await DbSet.AddAsync(entity);
            await Db.SaveChangesAsync();
        }

        public async Task<Pedido> ObterPorIdentificador(string identificador, string usuario)
        {
            var pedido = await DbSet.Include(i => i.Produtos).FirstOrDefaultAsync(w => w.IdentificadorUnico == identificador && w.Usuario == usuario);

            return pedido.ToModel();
        }

        public async Task<IEnumerable<Pedido>> ListarPedidos(string usuario)
        {
            var pedidos = await DbSet.Include(i => i.Produtos).Where(w => w.Usuario == usuario).ToListAsync();
            return pedidos.Select(s => s.ToModel());
        }

        public async Task AtualizarStatus(string identificador, string usuario, StatusPedido status)
        {
            var pedido =
                await DbSet.FirstOrDefaultAsync(w => w.IdentificadorUnico == identificador && w.Usuario == usuario);

            pedido.StatusPedido = status;
            await Db.SaveChangesAsync();

        }
    }
}
