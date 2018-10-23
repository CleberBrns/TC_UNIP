using System.Collections.Generic;

namespace TcUnip.Service.Contract.ServiceApi
{
    public interface IServiceAPIBase<TModel>
    {
        TModel Get(string id);
        List<TModel> List();
        bool Save(TModel model);
        bool Update(TModel model);
        bool Delete(string id);
    }
}
