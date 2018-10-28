using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Text;
using TcUnip.Model.Calendario;
using TcUnip.Model.Json;
using TcUnip.Model.Pessoa;
using TcUnip.Service.Contract.ServiceApi;
using TcUnip.ServiceApi.Common;

namespace TcUnip.ServiceApi.Calendario
{
    public class AgendaApi : ServiceApiBase<Agenda>, IAgendaApi
    {
        readonly string baseRoute = "agenda";
        readonly string agendaPaciente = "agenda/paciente";
        readonly string agendaFuncionario  = "agenda/funcionario";
        readonly string baseUrl = ConfigurationManager.AppSettings["BaseUrlApi"];

        public AgendaApi() : base("agenda", ConfigurationManager.AppSettings["BaseUrlApi"])
        {
        }

        #region Definições Url

        //Listar todos por período
        //GET BaseUrl  agenda/from/12/09/2018/to/12/09/2018

        //Seleciona by id
        //GET BaseUrl + agenda/1

        //Apagar um
        //DELETE BaseUrl + agenda/1

        //Inserir um
        //POST BaseUrl + agenda

        //Atualizar um por id
        //PUT BaseUrl + agenda/1

        //Listar um Paciente com todas suas consultas
        //GET agenda/paciente/cpfPaciente/from/12/09/2018/to/15/09/2018

        //Listar um Funcionario com todas suas consultas
        //GET agenda/funcionario/cpfFuncionario/from/12/09/2018/to/15/09/2018


        #endregion

        /// <summary>
        /// https://clinica-unip.herokuapp.com/clinica/agenda/from/12/09/2018/to/12/09/2018
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public List<Agenda> ListAgendasPeriodo(string dateFrom, string dateTo)
        {
            var parametros = string.Format("{0}", "from/" + dateFrom + "/to/" + dateTo);
            string action = string.Format("{0}{1}", baseUrl, baseRoute + "/" + parametros);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var listModel = new List<Agenda>();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                listModel = JsonConvert.DeserializeObject<List<Agenda>>(response.Content.ReadAsStringAsync().Result);

            return listModel;
        }

        /// <summary>
        /// https://clinica-unip.herokuapp.com/clinica/agenda/paciente/88888888888/from/12/09/2018/to/15/09/2018
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public Paciente ConsultasPeriodoPaciente(string cpf, string dateFrom, string dateTo)
        {            
            var parametros = "/" + cpf + "/from/" + dateFrom + "/to/" + dateTo;
            string action = string.Format("{0}{1}{2}", baseUrl, agendaPaciente, parametros);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var model = new Paciente();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                model = JsonConvert.DeserializeObject<Paciente>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        /// <summary>
        /// https://clinica-unip.herokuapp.com/clinica/agenda/funcionario/88888888888/from/12/09/2018/to/15/09/2018
        /// </summary>
        /// <param name="cpf"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        public Funcionario ConsultasPeriodoFuncionario(string cpf, string dateFrom, string dateTo)
        {            
            var parametros = "/" + cpf + "/from/" + dateFrom + "/to/" + dateTo;
            string action = string.Format("{0}{1}{2}", baseUrl, agendaFuncionario, parametros);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var model = new Funcionario();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                model = JsonConvert.DeserializeObject<Funcionario>(response.Content.ReadAsStringAsync().Result);

            return model;
        }
    }
}