using System.Collections.Generic;
using TC_Clinica_Gerenciamento.Models.Local;

namespace TC_Clinica_Gerenciamento.Contracts
{
    public interface IServiceApiBase<TModel>
    {
        TModel Get(string cpf);
        List<TModel> List();
        bool Save(TModel model);
        bool Update(TModel model);
        bool Delete(string cpf);
    }
}
