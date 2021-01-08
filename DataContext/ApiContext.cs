using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace app_reto_bcp.DataContext
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options) 
            :base(options)
        {
        }

        public DbSet<Models.ExchangeRate> tipoCambios { get; set; }
        public DbSet<Models.UsuarioLogin> usuarioLogins { get; set; }
        public DbSet<Models.UsuarioInfo> usuarioInfos { get; set; }
    }
}
