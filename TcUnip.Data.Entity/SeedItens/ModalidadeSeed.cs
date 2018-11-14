using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using TcUnip.Data.Entity.Modelagem.Common;

namespace TcUnip.Data.Entity.SeedItens
{
    public class ModalidadeSeed
    {
        public static void Seed(TcUnipContext context)
        {
            var listBD = context.Modalidade.ToList();

            if (!listBD.Any())
            {
                var arraySalvar = new Modalidade[]
                {
                    new Modalidade {Id = 1, Nome = "Osteopatia"},
                    new Modalidade {Id = 2, Nome = "Fisioterapia"},
                    new Modalidade {Id = 3, Nome = "Pilates"},
                    new Modalidade {Id = 4, Nome = "Treinamento Funcional"},
                    new Modalidade {Id = 5, Nome = "RPG"}
                };

                var listSalvar = new List<Modalidade>();

                foreach (var item in arraySalvar)
                {
                    if (listBD.Find(c => c.Id == item.Id && c.Nome == item.Nome) == null)
                    {
                        listSalvar.Add(item);
                    }
                }

                if (listSalvar.Count > 0)
                {
                    context.Modalidade.AddOrUpdate(listSalvar.ToArray());
                    context.SaveChanges();
                }
            }

        }
    }
}
