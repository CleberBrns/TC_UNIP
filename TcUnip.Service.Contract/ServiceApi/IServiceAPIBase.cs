using System.Collections.Generic;

namespace TcUnip.Service.Contract.ServiceApi
{
    public interface IServiceApiBase<TModel>
    {
        TModel Get(string id);
        List<TModel> List();
        bool Save(TModel model);
        bool Update(TModel model, string id);
        bool Delete(string id);
    }
}
