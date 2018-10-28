using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using TcUnip.Web.WebApiClient;

namespace TcUnip.Web.Util
{
    public class ErrosService
    {
        public static Tuple<string, string> GetMensagemService(Exception ex, HttpResponseBase response)
        {
            AdicionaBadRequestCustomizado(response);

            string msgExibicao = string.Empty;
            string msgAnalise = string.Empty;

            bool erroMapeado = false;
            HttpStatusCode statusCodeRetorno = new HttpStatusCode();

            try
            {
                statusCodeRetorno = ((ApiException)ex).StatusCode;
                erroMapeado = true;
            }
            catch (Exception) { }


            if (erroMapeado && statusCodeRetorno.Equals(HttpStatusCode.BadRequest))
                msgExibicao = GetRetornoDeErros(((ApiException)ex));
            else
            {
                msgExibicao = "Erro desconhecido!";
                msgAnalise = $"Mensagem da Exception; {ex.Message}";
            }

            return new Tuple<string, string>(msgExibicao, msgAnalise);
        }

        private static string GetRetornoDeErros(ApiException apiException)
        {
            string retornoApi = string.Empty;
            var badRequestData = JsonConvert.DeserializeObject<List<string>>(apiException.JsonData);

            if (badRequestData != null)
                foreach (var error in badRequestData)
                    retornoApi += error.Replace("\"", string.Empty) + (badRequestData.Count > 1 ? ", " : string.Empty);

            return retornoApi;
        }
        /// <summary>
        /// Adiciona manualmente pois o servidor não aceita a chamada direta
        /// </summary>
        public static void AdicionaBadRequestCustomizado(HttpResponseBase responseController)
        {
            responseController.Clear();
            responseController.TrySkipIisCustomErrors = true;

            responseController.StatusCode = (int)HttpStatusCode.BadRequest;
        }
    }
}