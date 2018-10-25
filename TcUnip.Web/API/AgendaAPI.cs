using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using TCC_Unip.Contracts.API;
using TCC_Unip.Models.Servico;

namespace TCC_Unip.API
{
    public class AgendaAPI : IAPIBase<Agenda>
    {
        readonly string agenda = "agenda";
        readonly string agendaPaciente = "agenda/paciente";
        readonly string agendaFuncionario  = "agenda/funcionario";

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

        public string BaseUrl
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["BaseUrlApi"];
            }
        }

        public Agenda Get(string id)
        {
            string action = string.Format("{0}{1}/{2}", BaseUrl, agenda, id);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var model = new Agenda();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                model = JsonConvert.DeserializeObject<Agenda>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        public List<Agenda> ListAgendasPeriodo(string dateFrom, string dateTo)
        {
            var parametros = string.Format("{0}", "from/" + dateFrom + "/to/" + dateTo);
            string action = string.Format("{0}{1}", BaseUrl, agenda + "/" + parametros);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var listModel = new List<Agenda>();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                listModel = JsonConvert.DeserializeObject<List<Agenda>>(response.Content.ReadAsStringAsync().Result);

            return listModel;
        }

        public Paciente ConsultasPeriodoPaciente(string cpf, string dateFrom, string dateTo)
        {
            //https://clinica-unip.herokuapp.com/clinica/agenda/paciente/88888888888/from/12/09/2018/to/15/09/2018
            var parametros = "/" + cpf + "/from/" + dateFrom + "/to/" + dateTo;
            string action = string.Format("{0}{1}{2}", BaseUrl, agendaPaciente, parametros);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var model = new Paciente();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                model = JsonConvert.DeserializeObject<Paciente>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        public Funcionario ConsultasPeriodoFuncionario(string cpf, string dateFrom, string dateTo)
        {
            //https://clinica-unip.herokuapp.com/clinica/agenda/funcionario/88888888888/from/12/09/2018/to/15/09/2018
            var parametros = "/" + cpf + "/from/" + dateFrom + "/to/" + dateTo;
            string action = string.Format("{0}{1}{2}", BaseUrl, agendaFuncionario, parametros);

            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            var model = new Funcionario();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                model = JsonConvert.DeserializeObject<Funcionario>(response.Content.ReadAsStringAsync().Result);

            return model;
        }

        public bool Save(Agenda model)
        {
            var agendaJS = new Models.Json.AgendaJS();
            agendaJS = agendaJS.ConvertToJS(model);

            var jsonModel = JsonConvert.SerializeObject(agendaJS);
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}", BaseUrl, agenda);

            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().PostAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Update(Agenda model)
        {
            var jsonModel = JsonConvert.SerializeObject(model);
            var jsonContent = new StringContent(jsonModel, Encoding.UTF8, "application/json");
            string action = string.Format("{0}{1}/{2}", BaseUrl, agenda, model.Id);

            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().PutAsync(action, jsonContent).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        public bool Delete(string id)
        {
            string action = string.Format("{0}{1}/{2}", BaseUrl, agenda, id);
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Delete, action);
            HttpResponseMessage response = Tools.HttpInstance.GetHttpClientInstance().SendAsync(request).Result;

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
                return true;

            return false;
        }

        #region Métodos Privados

        #endregion

        #region Métodos Não Implementados
        public List<Agenda> List()
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}