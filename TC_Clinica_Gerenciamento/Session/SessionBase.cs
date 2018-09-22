using System;
using System.Collections.Generic;
using System.Web;
using TCC_Unip.Contracts;

namespace TCC_Unip.Session
{
    public class SessionBase<TModel> : ISessionBase<TModel>
        where TModel : class, new()
    {
        public void AddModelToSession(TModel model, string session)
        {
            HttpContext.Current.Session[session] = model;
        }

        public void AddListToSession(List<TModel> list, string session)
        {
            HttpContext.Current.Session[session] = list;
        }

        public Tuple<TModel, bool> GetModelFromSession(string session)
        {
            var sessionaValida = false;
            var model = new TModel();

            if ((TModel)HttpContext.Current.Session[session] != null)
            {
                sessionaValida = true;
                model = (TModel)HttpContext.Current.Session[session];
            }

            return new Tuple<TModel, bool>(model, sessionaValida);
        }

        public Tuple<List<TModel>, bool> GetListFromSession(string session)
        {
            var sessionaValida = false;
            var list = new List<TModel>();

            if ((List<TModel>)HttpContext.Current.Session[session] != null)
            {
                sessionaValida = true;
                list = (List<TModel>)HttpContext.Current.Session[session];
            }

            return new Tuple<List<TModel>, bool>(list, sessionaValida);
        }

        public void RemoveFromSession(string session)
        {
            HttpContext.Current.Session.Remove(session);
        }

        public void RemoveAllSessions()
        {
            HttpContext.Current.Session.RemoveAll();
        }
    }
}