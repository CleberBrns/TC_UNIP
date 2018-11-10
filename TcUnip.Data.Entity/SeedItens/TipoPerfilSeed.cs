using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcUnip.Data.Entity.Modelagem.Cadastro;

namespace TcUnip.Data.Entity.SeedItens
{
    public class TipoPerfilSeed
    {
        public static List<TipoPerfil> Seed(TcUnipContext context)
        {
            var listBD = context.TipoPerfil.ToList();
            if (listBD.Any())
                return listBD.ToList();

            var arraySalvar = new TipoPerfil[]
            {
                    new TipoPerfil {Id = 1, Tipo = "Administrador", Permissao = "ADMINISTRACAO"},
                    new TipoPerfil {Id = 2, Tipo = "Recepcionista", Permissao = "RECEPCAO"},
                    new TipoPerfil {Id = 3, Tipo = "Profissional", Permissao = "PROFISSIONAL"}
            };

            var listSalvar = new List<TipoPerfil>();

            foreach (var item in arraySalvar)
            {
                if (listBD.Find(c => c.Id == item.Id && 
                                     c.Tipo == item.Tipo && 
                                     c.Permissao == item.Permissao) == null)
                {
                    listSalvar.Add(item);
                }
            }

            if (listSalvar.Count > 0)
            {
                context.TipoPerfil.AddOrUpdate(listSalvar.ToArray());
                context.SaveChanges();

                listSalvar = context.TipoPerfil.ToList();
            }

            return context.TipoPerfil.ToList();
        }
    }
}
