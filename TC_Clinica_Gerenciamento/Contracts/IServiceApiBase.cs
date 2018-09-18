using System.Collections.Generic;
using TCC_Unip.Models.Local;

namespace TCC_Unip.Contracts
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
