using Fretter.Domain.Config;
using Fretter.Domain.Dto.Carrefour;
using Fretter.Domain.Entities;
using Fretter.Domain.Enum;
using Fretter.Domain.Interfaces;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Fretter.Domain.Services
{
    public class EntregaStageService<TContext> : ServiceBase<TContext, EntregaStage>, IEntregaStageService<TContext>
     where TContext : IUnitOfWork<TContext>
    {
        private readonly IEntregaStageRepository<TContext> _Repository;
        private readonly IStageConfigRepository<TContext> _RepositoryStageConfig;
        private readonly IMessageBusService<EntregaStageService<TContext>> _bus;
        private readonly MessageBusConfig _config;
        private readonly ShipNConfig _shipNConfig;

        public EntregaStageService(IEntregaStageRepository<TContext> Repository,
            IStageConfigRepository<TContext> RepositoryStageConfig,
            IOptions<MessageBusConfig> config,
            IOptions<ShipNConfig> shipNConfig,
            IMessageBusService<EntregaStageService<TContext>> bus,
            IUnitOfWork<TContext> unitOfWork, IUsuarioHelper user) : base(Repository, unitOfWork, user)
        {
            _Repository = Repository;
            _RepositoryStageConfig = RepositoryStageConfig;
            _config = config.Value;
            _shipNConfig = shipNConfig.Value;
            _bus = bus;
            _bus.InitSender(_config.ConnectionString, _config.EntregaStageReciclagemEtiquetaTopic);          
        }

        public async Task<int> PopulaFilaReclicagemEtiquetas()
        {
            List<StageEtiquetaReciclagemFilaDTO> etiquetasParaReciclar = new List<StageEtiquetaReciclagemFilaDTO>();
            try
            {
                var dataAtual = DateTime.Now.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                var dataInicialParaBusca = dataAtual.AddMonths(-_shipNConfig.MesLimiteReciclagemEtiquetas).Date;

                etiquetasParaReciclar = _Repository.ObterEtiquetasParaReciclagem(dataAtual, dataInicialParaBusca).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(@$"{DateTime.Now:HH:mm:ss} - Falha ao obter etiquetas no banco de dados.
                                    InnerException : {ex.InnerException} / Message : {ex.Message}");
                return 0;
            }

            Console.WriteLine($"{DateTime.Now:HH:mm:ss} - Iniciando envio de {etiquetasParaReciclar.Count} etiquetas para reciclagem.");

            if(etiquetasParaReciclar.Count > 0)
            {
                try
                {
                    await _bus.SendRange<StageEtiquetaReciclagemFilaDTO>(etiquetasParaReciclar);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(@$"{DateTime.Now:HH:mm:ss} - Falha no envio de {etiquetasParaReciclar.Count} etiquetas para reciclagem.
                                        InnerException : {ex.InnerException} / Message : {ex.Message}");
                    return 0;
                }
            }

            Console.WriteLine(@$"{DateTime.Now:HH:mm:ss} - Finalizando envio de {etiquetasParaReciclar.Count} etiquetas para fila de reciclagem.");

            return etiquetasParaReciclar.Count;
        }
    }
}
