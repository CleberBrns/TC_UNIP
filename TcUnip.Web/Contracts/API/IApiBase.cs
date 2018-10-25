using System.Collections.Generic;

namespace TcUnip.Web.Contracts.API
{
    public interface IAPIBase<TModel>
    {
        TModel Get(string id);
        List<TModel> List();
        bool Save(TModel model);
        bool Update(TModel model);
        bool Delete(string id);
    }
}
