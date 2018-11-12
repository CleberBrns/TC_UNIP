using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TcUnip.Data.Entity.Modelagem.Agenda;

namespace TcUnip.Data.Entity.SeedItens
{
    public class StatusSessaoSeed
    {
        public static void Seed(TcUnipContext context)
        {
            var listBD = context.StatusSessao.ToList();

            if (!listBD.Any())
            {
                var arraySalvar = new StatusSessao[]
                {
                    new StatusSessao {Id = 1, Descricao = "Pendente"},
                    new StatusSessao {Id = 2, Descricao = "Paciente não compareceu, pagamento pendente"},
                    new StatusSessao {Id = 3, Descricao = "Paciente não compareceu, pagamento ok"},
                    new StatusSessao {Id = 4, Descricao = "Paciente compareceu, pagamento pendente"},
                    new StatusSessao {Id = 5, Descricao = "Profissional não estava disponível, pagamento pendente"},
                    new StatusSessao {Id = 6, Descricao = "Profissional não estava disponível, pagamento ok"},
                    new StatusSessao {Id = 7, Descricao = "Ok"}
                };

                var listSalvar = new List<StatusSessao>();

                foreach (var item in arraySalvar)
                {
                    if (listBD.Find(c => c.Id == item.Id && c.Descricao == item.Descricao) == null)
                    {
                        listSalvar.Add(item);
                    }
                }

                if (listSalvar.Count > 0)
                {
                    context.StatusSessao.AddOrUpdate(listSalvar.ToArray());
                    context.SaveChanges();
                }

            }
        }
    }
}
