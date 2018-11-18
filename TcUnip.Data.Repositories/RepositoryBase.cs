using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using TcUnip.Data.Contract;
using TcUnip.Data.Entity;

namespace TcUnip.Data.Repositories
{
    public abstract class RepositoryBase<TModel, TRespository> : IRepositoryBase<TModel>
        where TModel : class, new() where TRespository : class, new()
    {
        #region Construtor e propriedade

        protected readonly IMapper Mapper;

        public RepositoryBase(IMapper mapper)
        {
            Mapper = mapper;
        }

        #endregion

        public TModel Salvar(TModel model)
        {
            using (var context = new TcUnipContext())
            {
                var modelBD = Mapper.Map<TModel, TRespository>(model);
                context.Set<TRespository>().Add(modelBD);
                context.SaveChanges();

                return Mapper.Map<TRespository, TModel>(modelBD);
            }
        }

        public TModel Salvar(TRespository modelBD)
        {
            using (var context = new TcUnipContext())
            {
                context.Set<TRespository>().Add(modelBD);
                context.SaveChanges();

                return Mapper.Map<TRespository, TModel>(modelBD);
            }
        }

        public TModel Atualizar(TRespository modelBD)
        {
            using (var context = new TcUnipContext())
            {
                Attach(context, modelBD, EntityState.Modified);

                if (context.SaveChanges() != 0)
                    return Mapper.Map<TRespository, TModel>(modelBD);
                else
                    return null;
            }
        }

        public bool Atualizar(TModel model)
        {
            using (var context = new TcUnipContext())
            {
                var modeloBD = Mapper.Map<TModel, TRespository>(model);
                Attach(context, modeloBD, EntityState.Modified);

                return context.SaveChanges() != 0;
            }
        }

        public IList<TModel> SalvarLista(IList<TModel> listModel)
        {
            using (var context = new TcUnipContext())
            {
                var listaModelBD = Mapper.Map<IList<TModel>, IList<TRespository>>(listModel);
                context.Set<TRespository>().AddRange(listaModelBD);
                context.SaveChanges();

                return Mapper.Map<IList<TRespository>, IList<TModel>>(listaModelBD);
            }
        }

        public bool AtualizarLista(IList<TModel> listModel)
        {
            using (var context = new TcUnipContext())
            {
                foreach (var model in listModel)
                {
                    var modeloBD = Mapper.Map<TModel, TRespository>(model);
                    Attach(context, modeloBD, EntityState.Modified);
                }

                return context.SaveChanges() != 0;
            }
        }

        public bool Excluir(TModel model)
        {
            using (var context = new TcUnipContext())
            {

                try
                {
                    var modelo = Mapper.Map<TModel, TRespository>(model);
                    Attach(context, modelo, EntityState.Deleted);
                    return context.SaveChanges() != 0;
                }
                catch (DbEntityValidationException)
                {

                    return false;
                }
                catch (Exception)
                {

                    return false;
                }

            }
        }

        public bool Excluir(Expression<Func<TModel, bool>> expression)
        {
            using (var context = new TcUnipContext())
            {
                var modelo = context.Set<TRespository>().FirstOrDefault(GetMappedSelector(expression));
                if (modelo == null)
                    return false;
                Attach(context, modelo, EntityState.Deleted);
                return context.SaveChanges() != 0;
            }
        }

        public IList<TModel> Lista()
        {
            using (var context = new TcUnipContext())
            {
                var modelo = context.Set<TRespository>().ToList();
                return Mapper.Map<IList<TRespository>, IList<TModel>>(modelo);
            }
        }

        #region Private

        private Expression<Func<TRespository, bool>> GetMappedSelector(Expression<Func<TModel, bool>> selector)
        {
            var modelo = Mapper.Map<Expression<Func<TModel, bool>>, Expression<Func<TRespository, bool>>>(selector);
            return modelo;
        }

        private Expression<Func<TRespository, object>> GetMappedSelector(Expression<Func<TModel, object>> selector)
        {
            var modelo = Mapper.Map<Expression<Func<TModel, object>>, Expression<Func<TRespository, object>>>(selector);
            return modelo;
        }

        private void Attach(TcUnipContext context, TRespository entity, EntityState state)
        {
            var entry = context.Entry(entity);
            if (entry.State == EntityState.Detached)
                context.Set<TRespository>().Attach(entity);

            context.ChangeObjectState(entity, state);
        }

        #endregion
    }
}
