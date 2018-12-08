using AutoMapper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Data.Entity;
using TcUnip.Data.Entity.Modelagem.Agenda;
using TcUnip.Model.Agenda;
using TcUnip.Model.Common;

namespace TcUnip.Data.Repositories.Agenda
{
    public class SessaoRepository : RepositoryBase<SessaoModel, Sessao>, ISessaoRepository
    {        
        public SessaoRepository(IMapper mapper) : base(mapper) { }

        public SessaoModel GetById(int id)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<SessaoModel>(
                    context.Sessao.Where(x => x.Id == id)
                                  .Include(x => x.Modalidade)
                                  .Include(x => x.Paciente.Pessoa)
                                  .Include(x => x.Funcionario.Pessoa)
                                  .FirstOrDefault()
                    );
            }
        }

        public List<SessaoModel> ListSessoesPeriodo(PesquisaModel pesquisaModel)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<SessaoModel>>(
                    context.Sessao.Where(x => x.Data >= pesquisaModel.DataIncio &&
                                              x.Data <= pesquisaModel.DataFim)
                                  .Include(x => x.Modalidade)
                                  .Include(x => x.Paciente.Pessoa)                                  
                                  .Include(x => x.Funcionario.Pessoa)
                                  .AsNoTracking()
                                  .ToList()
                    );
            }
        }

        public List<SessaoModel> ListSessoesPeriodoFuncionario(PesquisaModel pesquisaModel)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<SessaoModel>>(
                    context.Sessao.Where(x => x.Data >= pesquisaModel.DataIncio &&
                                              x.Data <= pesquisaModel.DataFim &&
                                              x.Funcionario.Pessoa.Cpf == pesquisaModel.CpfPesquisa)
                                  .Include(x => x.Funcionario.Pessoa)  
                                  .Include(x => x.Modalidade)
                                  .AsNoTracking()
                                  .ToList()
                    );
            }
        }

        public List<SessaoModel> ListSessoesPeriodoCpfPaciente(PesquisaModel pesquisaModel)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<SessaoModel>>(
                    context.Sessao.Where(x => x.Data >= pesquisaModel.DataIncio &&
                                              x.Data <= pesquisaModel.DataFim &&
                                              x.Paciente.Pessoa.Cpf == pesquisaModel.CpfPesquisa)
                                  .Include(x => x.Paciente.Pessoa)
                                  .Include(x => x.Modalidade)
                                  .AsNoTracking()
                                  .ToList()
                    );
            }
        }

        public List<SessaoModel> ListSessoesPeriodoIdPaciente(PesquisaModel pesquisaModel)
        {
            using (var context = new TcUnipContext())
            {
                return Mapper.Map<List<SessaoModel>>(
                    context.Sessao.Where(x => !x.Excluido &&
                                              x.Data >= pesquisaModel.DataIncio &&
                                              x.Data <= pesquisaModel.DataFim &&
                                              x.Paciente.Id == pesquisaModel.IdPesquisa)
                                  .Include(x => x.Paciente.Pessoa)
                                  .Include(x => x.Modalidade)
                                  .AsNoTracking()
                                  .ToList()
                    );
            }
        }
    }
}
