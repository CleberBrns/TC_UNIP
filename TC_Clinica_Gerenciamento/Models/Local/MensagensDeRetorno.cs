using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TC_Clinica_Gerenciamento.Models.Local
{
    public class MensagensDeRetorno
    {
        public string MensagemExibicao { get; set; }
        public string MensagemAnalise { get; set; }
        public bool Status { get; set; }
    }
}