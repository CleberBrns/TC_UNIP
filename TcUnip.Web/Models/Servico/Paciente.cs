﻿using Newtonsoft.Json;
using System.Collections.Generic;

namespace TcUnip.Web.Models.Servico
{
    public class Paciente
    {
        [JsonProperty(PropertyName = "nome")]
        public string Nome { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }
        [JsonProperty(PropertyName = "cpf")]
        public string Cpf { get; set; }
        public string Id { get; set; }
        [JsonProperty(PropertyName = "telefone")]
        public string Telefone { get; set; }
        [JsonProperty(PropertyName = "status")]
        public string Status { get; set; }
        [JsonProperty(PropertyName = "endereco")]
        public Endereco Endereco { get; set; }
        [JsonProperty(PropertyName = "consultas")]
        public List<Consulta> Consultas { get; set; }

        public Paciente GetModelDefault()
        {
            var model = new Endereco();
            var endereco = model.GetModelDefault();

            return new Paciente
            {
                Cpf = string.Empty,
                Email = string.Empty,
                Nome = string.Empty,
                Telefone = string.Empty,
                Status = string.Empty,
                Endereco = endereco
            };
        }
    }
}