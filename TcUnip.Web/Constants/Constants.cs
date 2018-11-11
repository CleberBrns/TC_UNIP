using System;
using System.Collections.Generic;
using TcUnip.Web.Models.Local;

namespace TcUnip.Web.Constants
{
    public class Constants
    {
        #region Mensagens
        public const string msgFalhaAoSalvar = "Falha ao salvar!";
        public const string msgFalhaAoListar = "Falha ao listar!";
        public const string msgFalhaAoListarHorarios = "Falha ao listar os horários disponíveis!";
        public const string msgFalhaAoExcluir = "Falha ao excluir!";
        public const string msgFalhaAoCarregar = "Falha ao carregar!";
        public const string msgFalhaAoAutenticar = "Falha ao autenticar!";
        public const string msgFalhaPadrao = "Requição não atende ao método solicitado!";
        #endregion

        public List<DataSelectControl> ListStatus()
        {            
            return new List<DataSelectControl>
            {
                new DataSelectControl { Name = "Ativo", Value = "ATIVO"},
                new DataSelectControl { Name = "Inativo", Value = "INATIVO" }
            };
        }

        public List<DataSelectControl> ListModalidades()
        {
            return new List<DataSelectControl>
            {
                new DataSelectControl { Name = "Osteopatia", Value = "Osteopatia"},
                new DataSelectControl { Name = "Fisioterapia", Value = "Fisioterapia" },
                new DataSelectControl { Name = "Pilates", Value = "Pilates" },
                new DataSelectControl { Name = "Treinamento Funcional", Value = "TreinamentoFuncional" },
                new DataSelectControl { Name = "RPG", Value = "RPG" }
            };
        }

        public List<DataSelectControl> ListPermissoesPerfil()
        {
            return new List<DataSelectControl>
            {
                new DataSelectControl { Name = "Administrador", Value = "ADMINISTRACAO"},
                new DataSelectControl { Name = "Recepcionista", Value = "RECEPCAO" },
                new DataSelectControl { Name = "Profissional", Value = "PROFISSIONAL" }
            };
        }

        public List<DataSelectControl> ListStatusConsulta()
        {
            return new List<DataSelectControl>
            {
                new DataSelectControl { Name = "Pendente", Value = "PENDENTE"},
                new DataSelectControl { Name = "Não compareceu", Value = "NAO_COMPARECEU" },
                new DataSelectControl { Name = "Não compareceu, pagamento pendente", Value = "NAO_COMPARECEU_PAGAMENTO_PENDENTE" },
                new DataSelectControl { Name = "Compareceu, pagamento pendente", Value = "COMPARECEU_PAGAMENTO_PENDENTE" },
                new DataSelectControl { Name = "Concluído", Value = "OK" }
            };
        }

        public List<DataSelectControl> ListHorariosConsultas()
        {            
            return GetHorariosDoDia();
        }

        private List<DataSelectControl> GetHorariosDoDia()
        {
            var listHorarios = new List<DataSelectControl>();
            var startDate = DateTime.Today.AddHours(7);
            var endDate = DateTime.Today.AddHours(20);

            List<string> times = new List<string>();

            var currentTime = startDate;
            if (currentTime.Minute != 0 || currentTime.Second != 0)
            {
                //Pega a próxima hora
                currentTime = currentTime.AddHours(1).AddMinutes(currentTime.Minute * -1);
            }

            while (currentTime <= endDate)
            {
                var horario = string.Format("{0:00}:00", currentTime.Hour);

                listHorarios.Add(new DataSelectControl {
                    Value = horario,
                    Name = horario
                });
                
                currentTime = currentTime.AddHours(1);
            }

            return listHorarios;
        }
    }
}