using System.Collections.Generic;
using TCC_Unip.Models.Local;

namespace TCC_Unip.Constants
{
    public class Constants
    {
        #region Mensagens
        public const string msgFalhaAoSalvar = "Falha ao salvar!";
        public const string msgFalhaAoListar = "Falha ao listar!";
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
                new DataSelectControl { Name = "Treinamento Funcional", Value = "Treinamento Funcional" },
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
    }
}