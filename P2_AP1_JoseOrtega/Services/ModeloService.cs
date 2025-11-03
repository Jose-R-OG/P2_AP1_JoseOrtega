using Microsoft.EntityFrameworkCore;
using P2_AP1_JoseOrtega.Components.Pages.ModeloPages;
using P2_AP1_JoseOrtega.DAL;
using P2_AP1_JoseOrtega.Models;
using System.Linq.Expressions;

namespace P2_AP1_JoseOrtega.Services
{
    public class ModeloService(IDbContextFactory<Contexto> DbFactory)
    {
        public async Task<List<Pedidos>> Listar(Expression<Func<Pedidos, bool>> criterio)
        {
            await using var contexto = await DbFactory.CreateDbContextAsync();
            return await contexto.pedidos
                .Where(criterio)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
