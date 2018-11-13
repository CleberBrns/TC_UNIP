using AutoMapper;
using TcUnip.Data.Contract.Agenda;
using TcUnip.Data.Contract.Cadastro;
using TcUnip.Data.Contract.FluxoCaixa;
using TcUnip.Data.Entity.Modelagem.Agenda;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Data.Entity.Modelagem.FluxoCaixa;
using TcUnip.Data.Repositories.Agenda;
using TcUnip.Data.Repositories.Cadastro;
using TcUnip.Data.Repositories.FluxoCaixa;
using TcUnip.Model.Agenda;
using TcUnip.Model.Cadastro;
using TcUnip.Model.FluxoCaixa;
using TcUnip.Service.Agenda;
using TcUnip.Service.Cadastro;
using TcUnip.Service.Contract.Agenda;
using TcUnip.Service.Contract.Cadastro;
using TcUnip.Service.Contract.FluxoCaixa;
using TcUnip.Service.FluxoCaixa;
using Unity;

namespace TcUnip.Config
{
    public class ConfigureApiContainer
    {
        public static void InitializeContainer(IUnityContainer container, bool fake = false)
        {
            container.RegisterInstance<IMapper>(InitializeAutoMapper().CreateMapper());

            #region Repositorios
            //Cadastro
            container.RegisterType<IFuncionarioRepository, FuncionarioRepository>();
            container.RegisterType<IPacienteRepository, PacienteRepository>();
            container.RegisterType<IPessoaRepository, PessoaRepository>();
            container.RegisterType<ITipoPerfilRepository, TipoPerfilRepository>();
            container.RegisterType<IUsuarioRepository, UsuarioRepository>();
            container.RegisterType<IModalidadeFuncionarioRepository, ModalidadeFuncionarioRepository>();

            //Agenda 
            container.RegisterType<IModalidadeRepository, ModalidadeRepository>();
            container.RegisterType<ISessaoRepository, SessaoRepository>();
            container.RegisterType<IStatusSessaoRepository, StatusSessaoRepository>();

            //Fluxo Caixao
            container.RegisterType<ICaixaRepository, CaixaRepository>();
            #endregion

            #region Services

            container.RegisterType<IAgendaService, AgendaService>();
            container.RegisterType<ICadastroService, CadastroService>();
            container.RegisterType<IFluxoCaixaService, FluxoCaixaService>();

            #endregion
        }

        private static MapperConfiguration InitializeAutoMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                /*O 'ReverseMap()' cria apenas um mapeamento inverso simples.
                  Caso o mapeamento tenha alguma configuração especifica, 
                  devemos criar manualmente os dois formato do map*/

                #region Model to Entity

                //Cadastro
                cfg.CreateMap<FuncionarioModel, Funcionario>().ReverseMap();
                cfg.CreateMap<PacienteModel, Paciente>().ReverseMap();
                cfg.CreateMap<PessoaModel, Pessoa>().ReverseMap();
                cfg.CreateMap<TipoPerfilModel, TipoPerfil>().ReverseMap();
                cfg.CreateMap<UsuarioModel, Usuario>().ReverseMap();
                cfg.CreateMap<ModalidadeFuncionarioModel, ModalidadeFuncionario>().ReverseMap();

                //Agenda
                cfg.CreateMap<ModalidadeModel, Modalidade>().ReverseMap();
                cfg.CreateMap<SessaoModel, Sessao>().ReverseMap();
                cfg.CreateMap<StatusSessaoModel, StatusSessao>().ReverseMap();

                //Fluxo Caixa
                cfg.CreateMap<CaixaModel, Caixa>().ReverseMap();

                #endregion
            });
            return config;
        }
    }
}
