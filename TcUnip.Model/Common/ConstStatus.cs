using System.Collections.Generic;

namespace TcUnip.Model.Common
{
    public class ConstStatus
    {
        public class Agendamento
        {
            public const string pendente = "PENDENTE";
            public const string ok = "OK";
            public const string naoCompareceu = "NAO_COMPARECEU";
            public const string naoCompareceuPagtoPend = "NAO_COMPARECEU_PAGAMENTO_PENDENTE";
            public const string compareceuPagtoPend = "COMPARECEU_PAGAMENTO_PENDENTE";

            public List<DataControlValue> GetListStatusAgendamento()
            {
                return new List<DataControlValue>
                {
                    new DataControlValue
                    {
                        Name = "Pendente",
                        Value = Agendamento.pendente
                    },
                    new DataControlValue
                    {
                        Name = "Concluído",
                        Value = Agendamento.ok
                    },
                    new DataControlValue
                    {
                        Name = "Não compareceu",
                        Value = Agendamento.naoCompareceu
                    },
                    new DataControlValue
                    {
                        Name = "Não compareceu, pagamento está pendente",
                        Value = Agendamento.naoCompareceuPagtoPend
                    },
                    new DataControlValue
                    {
                        Name = "Compareceu, pagamento está pendente",
                        Value = Agendamento.compareceuPagtoPend
                    }
                };
            }            
        }
    }
}
