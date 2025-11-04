using Microsoft.EntityFrameworkCore;
using P2_AP1_JoseOrtega.DAL;
using P2_AP1_JoseOrtega.Models;
using System.Linq.Expressions;

namespace P2_AP1_JoseOrtega.Services
{
    public class PedidosService(IDbContextFactory<Contexto> DbFactory)
    {
        private async Task<bool> Existe(int idPedido)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.pedidos.AnyAsync(p => p.PedidoId == idPedido);
        }

        private async Task AfectarExistencia(PedidosDetalle[] detalle, TipoOperacion tipoOperacion)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            foreach (var item in detalle)
            {
                var producto = await contexto.Componentes.SingleAsync(p => p.ComponenteId == item.ComponenteId);
                if (tipoOperacion == TipoOperacion.Resta)
                    producto.Existencia -= item.Cantidad;
                else
                    producto.Existencia += item.Cantidad;
                await contexto.SaveChangesAsync();
            }
        }


        private async Task<bool> Insertar(Pedidos pedidos)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            contexto.pedidos.Add(pedidos);
            await AfectarExistencia(pedidos.pedidosDetalles.ToArray(), TipoOperacion.Resta);
            return await contexto.SaveChangesAsync() > 0;
        }

        private async Task<bool> Modificar(Pedidos pedidos)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var original = await contexto.pedidos
                .Include(p => p.pedidosDetalles)
                .AsNoTracking()
                .SingleOrDefaultAsync(p => p.PedidoId == pedidos.PedidoId);

            if (original == null) return false;

            await AfectarExistencia(original.pedidosDetalles.ToArray(), TipoOperacion.Suma);

            contexto.pedidos.RemoveRange((Pedidos)original.pedidosDetalles);

            contexto.Update(pedidos);

            await AfectarExistencia(pedidos.pedidosDetalles.ToArray(), TipoOperacion.Resta);

            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<bool> Guardar(Pedidos pedidos)
        {
            if (!await Existe(pedidos.PedidoId))
            {
                return await Insertar(pedidos);
            }
            else
            {
                return await Modificar(pedidos);
            }
        }

        public async Task<Pedidos?> Buscar(int pedidoid)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.pedidos.Include(e => e.pedidosDetalles).FirstOrDefaultAsync(p => p.PedidoId == pedidoid);
        }

        public async Task<bool> Eliminar(int pedidoid)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            var pedido = await Buscar(pedidoid);

            await AfectarExistencia(pedido.pedidosDetalles.ToArray(), TipoOperacion.Suma);
            contexto.pedidos.RemoveRange((Pedidos)pedido.pedidosDetalles);
            contexto.pedidos.Remove(pedido);
            return await contexto.SaveChangesAsync() > 0;
        }

        public async Task<List<Pedidos>> Listar(Expression<Func<Pedidos, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.pedidos.
                Include(p => p.pedidosDetalles)
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }

        public enum TipoOperacion
        {
            Suma = 1,
            Resta = 2
        }
    }
}
