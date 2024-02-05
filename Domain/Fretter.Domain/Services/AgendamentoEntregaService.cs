using Fretter.Domain.Dto.EntregaAgendamento;
using Fretter.Domain.Entities;
using Fretter.Domain.Helpers;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Globalization;
using Fretter.Domain.Extensions;

namespace Fretter.Domain.Services
{
    public class AgendamentoEntregaService<TContext> : ServiceBase<TContext, AgendamentoEntrega>, IAgendamentoEntregaService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IUsuarioHelper _user;
        private readonly IAgendamentoEntregaRepository<TContext> _repository;
        private readonly IEmpresaService<TContext> _repositoryEmpresa;

        public AgendamentoEntregaService(IAgendamentoEntregaRepository<TContext> repository, IUnitOfWork<TContext> unitOfWork, IEmpresaService<TContext> repositoryEmpresa,  IUsuarioHelper user) 
            : base(repository, unitOfWork, user)
        {
            _user = user;
            _repository = repository;
            _repositoryEmpresa = repositoryEmpresa;
        }

        public SkuDetalhesDto ObterProdutoPorCodigo(string codigo)
        {
            try
            {
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Codigo", codigo));
                parameters.Add(new SqlParameter("@EmpresaId", _user.UsuarioLogado.EmpresaId));

                var result = _repository.ExecuteStoredProcedureWithParam<SkuDetalhesDto>("dbo.Pr_Age_BuscaProdutoPorCodigo", parameters.ToArray()).FirstOrDefault();

                return result;
            }
            catch
            {
                return null;
            }
        }

        public List<DisponibilidadeDto> ObterDisponibilidade(AgendamentoDisponibilidadeFiltro filtro)
        {
            try
            {
                CultureInfo culture = new CultureInfo("pt-BR");
                List<SqlParameter> parameters = new List<SqlParameter>();
                parameters.Add(new SqlParameter("@Id_Empresa", _user.UsuarioLogado.EmpresaId));
                parameters.Add(new SqlParameter("@Cd_Cep", filtro.Cep));
                parameters.Add(new SqlParameter("@Cd_Quantidade", filtro.QuantidadeItens));
                parameters.Add(new SqlParameter("@Cd_Pagina", filtro.Pagina));
                parameters.Add(new SqlParameter("@Id_EmpresaFilial", filtro.CanalId));

                var result = _repository.ExecuteStoredProcedureWithParam<DisponibilidadeDto>("dbo.Pr_MF_BuscaRegiaoCEPCapacidade", parameters.ToArray());
                var id = 4;
                result.ForEach(disp => 
                {
                    disp.Id = (disp.Id == 0) ? id++ : disp.Id;
                    disp.DataCompleta = string.Concat(culture.DateTimeFormat.GetDayName(disp.Data.DayOfWeek), " (", disp.Data.ToShortDateString(), ")").ToTitleCase();
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  async Task<ConsultaCepDto> ObterCep(string cep)
        {
            var token = _repositoryEmpresa.Get(_user.UsuarioLogado.EmpresaId)?.TokenId;

            var webApiClient = new WebApiClient("http://fretter-webhook-correios.uxsolutions.com.br/api/correios/buscacep");
            webApiClient.AddHeader("Token", token.ToString());
            
            var request = new List<object>();
            request.Add(new { cep = cep });

            var response = await webApiClient.Post(null, request);
            var obj = JsonConvert.DeserializeObject<List<ConsultaCepDto>>(response.Content.ReadAsStringAsync().Result);

            var result = obj.FirstOrDefault(x => x.cep == cep);

            return result;
        }

        public override AgendamentoEntrega Save(AgendamentoEntrega entidade)
        {
            entidade.AtualizarUsuarioCriacao(_user.UsuarioLogado.Id);
            entidade.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            entidade.Destinatarios.ForEach(x =>
            {
                x.AtualizarDocumentoHash(CryptoHelper.SetHashValue(x.CpfCnpj));
            });

            return base.Save(entidade);
        }

        public override AgendamentoEntrega Update(AgendamentoEntrega entidade)
        {
            entidade.AtualizarEmpresaId(_user.UsuarioLogado.EmpresaId);
            entidade.Destinatarios.ForEach(x =>
            {
                x.AtualizarDocumentoHash(CryptoHelper.SetHashValue(x.CpfCnpj));
            });

            return base.Update(entidade);
        }

        public override IPagedList<AgendamentoEntrega> GetPaginated(QueryFilter filter, int start, int limit, bool orderByDescending)
        {
            //filter.Filters.Clear();
            filter.AddFilter("IdEmpresa", _user.UsuarioLogado.EmpresaId);

            return _repository.GetPaginated(filter, start, limit, orderByDescending);
        }
    }
}
