using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace TcUnip.Data.Contract
{
    public interface IRepositoryBase<TModel>
    {
        TModel Salvar(TModel model);
        bool Atualizar(TModel model);
        IList<TModel> SalvarLista(IList<TModel> listModel);
        bool AtualizarLista(IList<TModel> listModel);
        bool Excluir(TModel model);
        bool Excluir(Expression<Func<TModel, bool>> expression);
        IList<TModel> Lista();
        IList<TModel> Lista(params Expression<Func<TModel, object>>[] includes);
        IList<TModel> Lista(Expression<Func<TModel, bool>> expression);
        IList<TModel> Lista(Expression<Func<TModel, bool>> expression, params Expression<Func<TModel, object>>[] includes);
        TModel SelecionarUm(Expression<Func<TModel, bool>> expression);
        TModel SelecionarUm(Expression<Func<TModel, bool>> expression, params Expression<Func<TModel, object>>[] includes);        
    }
}
