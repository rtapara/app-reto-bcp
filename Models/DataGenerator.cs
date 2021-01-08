using app_reto_bcp.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace app_reto_bcp.Models
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApiContext(serviceProvider.GetRequiredService<DbContextOptions<ApiContext>>()))
            {
                if (context.tipoCambios.Any())
                {
                    return;
                }

                context.tipoCambios.AddRange(
                        new ExchangeRate {
                            Id= 1,
                            monedaOrigen = "S",
                            monedaDestino = "D",
                            tipoCambio = 3.62
                        },
                        new ExchangeRate
                        {
                            Id=2,
                            monedaOrigen = "S",
                            monedaDestino = "E",
                            tipoCambio = 3.90
                        }
                    );

                context.usuarioLogins.AddRange(
                        new UsuarioLogin
                        {
                            Usuario = "roly.tapara",
                            Password = "pruebas123."
                        },
                        new UsuarioLogin
                        {
                            Usuario = "manuel.Zegarra",
                            Password = "pruebas123."
                        }
                    );

                context.usuarioInfos.AddRange(
                        new UsuarioInfo
                        {
                            Id = Guid.NewGuid(),
                            Nombre = "Roly",
                            Apellidos = "Tapara Huamani",
                            Email = "roly5882@gmail.com",
                            Rol = "Postulante",
                            Usuario = "roly.tapara"

                        },
                        new UsuarioInfo
                        {
                            Id = Guid.NewGuid(),
                            Nombre = "Manuel Alberto",
                            Apellidos = "Zegarra Sanchez",
                            Email = "manuelzegarra@bcp.com.pe",
                            Rol = "Evaluador",
                            Usuario = "manuel.Zegarra"
                        }
                    ); ;

                context.SaveChanges();
            }
        }

    }
}
