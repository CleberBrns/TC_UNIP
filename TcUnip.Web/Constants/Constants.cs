﻿using System;
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
                new DataSelectControl { Name = "Ativo", IntValue = 1, Value = "True"},
                new DataSelectControl { Name = "Inativo", IntValue = 0, Value = "False" }
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