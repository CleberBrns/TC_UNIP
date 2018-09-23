using System.Collections.Generic;
using TCC_Unip.Models.Local;
using TCC_Unip.Models.Servico;
using TCC_Unip.API;
using TCC_Unip.Contracts.Service;

namespace TCC_Unip.Services
{
    public class FuncionarioService : IServiceFuncionario
    {
        FuncionarioAPI service = new FuncionarioAPI();

        public ResultService<Funcionario> Get(string cpf)
        {
            var result = new ResultService<Funcionario>();

            var retorno = service.Get(cpf);
            result.value = retorno;

            if (string.IsNullOrEmpty(result.value.Cpf))
                result.message = "O Funcionário não existe mais na base de dados do serviço!";

            return result;
        }

        public ResultService<List<Funcionario>> List()
        {
            var result = new ResultService<List<Funcionario>>();

            var retorno = service.List();
            result.value = retorno;

            return result;
        }

        public ResultService<bool> Save(Funcionario model)
        {
            string msg = string.Empty;
            string msgErro = string.Empty;

            var result = new ResultService<bool>();

            var registroExistente = service.Get(model.Cpf);

            if (string.IsNullOrEmpty(registroExistente.Nome))
            {
                var retorno = service.Save(model);
                result.value = retorno;

                if (result.value)
                    msg = "Funcionário salvo com sucesso!";
                else
                    msg = "Falha ao salvar o Funcionário!";
            }
            else
            {
                var retorno = service.Update(model);
                result.value = retorno;

                if (result.value)
                    msg = "Funcionário atualizado com sucesso!";
                else
                    msg = "Falha ao atualizar o Funcionário!";
            }

            result.message = msg;
            result.errorMessage = msgErro;

            return result;
        }
        

        public ResultService<bool> Delete(string cpf)
        {
            var result = new ResultService<bool>();

            var retorno = service.Delete(cpf);
            result.value = retorno;

            if (result.value)
                result.message = "Funcionário excluído com sucesso!";
            else
                result.message = "Falha ao excluir o Funcionário!";

            return result;
        }

    }
}