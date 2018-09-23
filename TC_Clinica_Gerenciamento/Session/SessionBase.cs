using System;
using System.Collections.Generic;
using System.Web;
using TCC_Unip.Contracts;

namespace TCC_Unip.SessionBase
{
    public class SessionBase<TModel> : ISessionBase<TModel>
        where TModel : class, new()
    {
        public void AddModelToSession(TModel model, string sessionName)
        {
            HttpContext.Current.Session[sessionName] = model;
        }

        public void AddListToSession(List<TModel> list, string sessionName)
        {
            HttpContext.Current.Session[sessionName] = list;
        }

        public Tuple<TModel, bool> GetModelFromSession(string sessionName)
        {
            var sessionNameaValida = false;
            var model = new TModel();

            if ((TModel)HttpContext.Current.Session[sessionName] != null)
            {
                sessionNameaValida = true;
                model = (TModel)HttpContext.Current.Session[sessionName];
            }

            return new Tuple<TModel, bool>(model, sessionNameaValida);
        }

        public Tuple<List<TModel>, bool> GetListFromSession(string sessionName)
        {
            var sessionNameaValida = false;
            var list = new List<TModel>();

            if ((List<TModel>)HttpContext.Current.Session[sessionName] != null)
            {
                sessionNameaValida = true;
                list = (List<TModel>)HttpContext.Current.Session[sessionName];
            }

            return new Tuple<List<TModel>, bool>(list, sessionNameaValida);
        }

        public void RemoveFromSession(string sessionName)
        {
            HttpContext.Current.Session.Remove(sessionName);
        }
    }
}