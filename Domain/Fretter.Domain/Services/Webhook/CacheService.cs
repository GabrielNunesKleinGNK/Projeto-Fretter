using Fretter.Domain.Config.WebHook;
using Fretter.Domain.Entities;
using Fretter.Domain.Entities.Fusion;
using Fretter.Domain.Interfaces.Repository;
using Fretter.Domain.Interfaces.Repository.Fusion;
using Fretter.Domain.Interfaces.Service.Webhook;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Fretter.Domain.Services.Webhook
{
    public class CacheService<TContext> : ICacheService<TContext>
        where TContext : IUnitOfWork<TContext>
    {
        private readonly IMemoryCache _cache;
        private readonly IServiceProvider _serviceProvider;
        private readonly WebHookConfig _config;
        private readonly string _prefix = "cache_";

        public CacheService(IMemoryCache cache,
                            IServiceProvider serviceProvider,
                            IOptions<WebHookConfig> config)
        {
            _cache = cache;
            _serviceProvider = serviceProvider;
            _config = config.Value;
        }

        public void InicializaCache()
        {
            Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Iniciando Carregamento do Cache Webhook");
            CarregaTabelaAtivos<Transportador>();
            CarregaTabelaAtivos<TransportadorCnpj>();
            CarregaTabelaAtivos<Empresa>();
            CarregaTabelaAtivos<Canal>();
            CarregaTabelaTodos<Tipo>();
        }
        public List<TEntity> ObterLista<TEntity>(Expression<Func<TEntity, bool>> predicate = null, bool apenasAtivos = true)
            where TEntity : EntityBase
        {
            string key = typeof(TEntity).Name;
            var lista = _cache.Get<List<TEntity>>(string.Concat(_prefix, key));

            if (lista == null)
                lista = apenasAtivos ? CarregaTabelaAtivos<TEntity>() : CarregaTabelaTodos<TEntity>();

            if (predicate != null)
                lista = lista.Where(predicate.Compile()).ToList();

            if (lista.Count() == 0)
            {
                lista = apenasAtivos ? CarregaTabelaAtivos<TEntity>() : CarregaTabelaTodos<TEntity>();
                return lista.Where(predicate.Compile()).ToList();
            }

            return lista;
        }
        private List<TEntity> CarregaTabelaAtivos<TEntity>()
            where TEntity : EntityBase
        {
            return CarregaCache<TEntity>(x => x.Ativo);
        }
        private List<TEntity> CarregaTabelaTodos<TEntity>()
          where TEntity : EntityBase
        {
            return CarregaCache<TEntity>();
        }
        private List<TEntity> CarregaCache<TEntity>(Expression<Func<TEntity, bool>> predicate = null)
          where TEntity : EntityBase
        {
            var watch = new Stopwatch();
            watch.Start();

            var repository = (IRepositoryBase<TContext, TEntity>)_serviceProvider.GetService(typeof(IRepositoryBase<TContext, TEntity>));
            return _cache.GetOrCreate(string.Concat(_prefix, typeof(TEntity).Name), entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_config.CacheMinutosExpiracao);
                entry.SetPriority(CacheItemPriority.High);
                var list = repository.GetAll(predicate, null);
                WriteColor($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")} - Cache [{typeof(TEntity).Name}] - Key: [{string.Concat(_prefix, typeof(TEntity).Name)}] - Tempo: [{watch.ElapsedMilliseconds}ms]", ConsoleColor.Yellow);
                watch.Reset();
                return list;
            }).ToList();

        }
        private static void WriteColor(string message, ConsoleColor color)
        {
            var pieces = Regex.Split(message, @"(\[[^\]]*\])");
            for (int i = 0; i < pieces.Length; i++)
            {
                string piece = pieces[i];
                if (piece.StartsWith("[") && piece.EndsWith("]"))
                {
                    Console.ForegroundColor = color;
                    piece = piece.Substring(1, piece.Length - 2);
                }
                Console.Write(piece);
                Console.ResetColor();
            }
            Console.WriteLine();
        }
    }
}
