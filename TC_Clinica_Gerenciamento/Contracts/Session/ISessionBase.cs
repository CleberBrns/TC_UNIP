using System;
using System.Collections.Generic;

namespace TCC_Unip.Contracts
{
    public interface ISessionBase<TModel>
    {
        Tuple<TModel, bool> GetModelFromSession(string session);
        Tuple<List<TModel>, bool> GetListFromSession(string session);
        void AddModelToSession(TModel model, string session);
        void AddListToSession(List<TModel> list, string session);
        void RemoveFromSession(string session);
        void RemoveAllSessions();
    }
}
