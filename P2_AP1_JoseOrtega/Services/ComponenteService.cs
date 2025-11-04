using Microsoft.EntityFrameworkCore;
using P2_AP1_JoseOrtega.DAL;
using P2_AP1_JoseOrtega.Models;

namespace P2_AP1_JoseOrtega.Services;

public class ComponenteService(IDbContextFactory<Contexto> DbFactory)
{
    public async Task<List<Componente>> ListarComponenetes()
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();
        return await contexto.Componentes.Where(p => p.ComponenteId > 0).AsNoTracking().ToListAsync();
    }
}
