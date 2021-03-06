﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using TcUnip.Data.Entity.Modelagem.Agenda;

namespace TcUnip.Data.Entity.Modelagem.Cadastro
{
    public class Funcionario
    {
        public int Id { get; set; }
        public bool Ativo { get; set; }
        public bool Excluido { get; set; }

        [ForeignKey("Pessoa")]
        public int IdPessoa { get; set; }
        public virtual Pessoa Pessoa { get; set; }

        public virtual List<ModalidadeFuncionario> Modalidades { get; set; }
    }
}
