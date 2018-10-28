using System.Configuration;
using TcUnip.Model.Pessoa;
using TcUnip.Service.Contract.ServiceApi;
using TcUnip.ServiceApi.Common;

namespace TcUnip.ServiceApi.Pessoa
{
    public class FuncionarioApi : ServiceApiBase<Funcionario>, IFuncionarioApi
    {
        public FuncionarioApi() : base("funcionario", ConfigurationManager.AppSettings["BaseUrlApi"])
        {
        }

        #region Definições Url

        //Listar todos
        //GET BaseUrl 

        //Listar id(cpf)
        //GET BaseUrl + model/1

        //Apagar um
        //DELETE BaseUrl + model/1

        //Inserir um
        //POST BaseUrl + model

        //Atualizar um por id(cpf)
        //PUT BaseUrl + model/1

        #endregion

    }
}