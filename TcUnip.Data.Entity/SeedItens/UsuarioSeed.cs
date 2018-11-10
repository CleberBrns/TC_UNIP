using System.Linq;
using System.Data.Entity;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using System.Collections.Generic;

namespace TcUnip.Data.Entity.SeedItens
{
    public class UsuarioSeed
    {
        public static void Seed(TcUnipContext context, List<TipoPerfil> listTipoPerfil)
        {
            var perfilAdm = listTipoPerfil.Where(l => l.Permissao == "ADMINISTRACAO")
                                          .FirstOrDefault();

            if (perfilAdm != null)
            {
                var admUser = new Usuario
                {
                    Email = "master@tcunip.com.br",
                    Senha = "@dmin56784321",
                    TipoPerfil = perfilAdm
                };

                var userBd = context.Usuario
                                    .Include(t => t.TipoPerfil)
                                    .Where(u => u.Email == admUser.Email &&
                                                        u.Senha == admUser.Senha &&
                                                        u.TipoPerfil.Permissao == admUser.TipoPerfil.Permissao)
                                    .FirstOrDefault();

                if (userBd == null)
                {
                    context.Usuario.Add(admUser);
                    context.SaveChanges();
                }
            }
        }
    }
}
