using System.Linq;
using System.Data.Entity;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using System.Collections.Generic;
using System.Data.Entity.Migrations;

namespace TcUnip.Data.Entity.SeedItens
{
    public class UsuarioSeed
    {
        public static void Seed(TcUnipContext context)
        {
            var perfilAdm = context.TipoPerfil.Where(l => l.Permissao == "ADMINISTRACAO")
                                          .FirstOrDefault();

            if (perfilAdm != null)
            {
                var admUser = new Usuario
                {
                    Email = "master@tcunip.com.br",
                    Senha = "@dmin56784321",
                    Cpf = "111.222.333-44",
                    Ativo = true,
                    Excluido = true,
                    IdTipoPerfil = perfilAdm.Id                    
                };

                var userBd = context.Usuario.Where(u => u.Email == admUser.Email &&
                                                        u.Senha == admUser.Senha &&
                                                        u.Cpf == admUser.Cpf &&
                                                        u.IdTipoPerfil == admUser.IdTipoPerfil)
                                            .FirstOrDefault();

                if (userBd == null)
                {
                    context.Usuario.AddOrUpdate(admUser);
                    context.SaveChanges();
                }
            }
        }
    }
}
