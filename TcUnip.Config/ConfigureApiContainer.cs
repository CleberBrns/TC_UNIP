using AutoMapper;
using TcUnip.Data.Entity.Modelagem.Agenda;
using TcUnip.Data.Entity.Modelagem.Cadastro;
using TcUnip.Data.Entity.Modelagem.FluxoCaixa;
using TcUnip.Model.Agenda;
using TcUnip.Model.Cadastro;
using TcUnip.Model.FluxoCaixa;
using Unity;

namespace TcUnip.Config
{
    public class ConfigureApiContainer
    {
        public static void InitializeContainer(IUnityContainer container, bool fake = false)
        {
            container.RegisterInstance<IMapper>(InitializeAutoMapper().CreateMapper());
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
