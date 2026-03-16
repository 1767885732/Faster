using FastFrame.Core;
using FastFrame.Data;
using FastFrame.EFCore;
using FastFrame.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Reflection;

namespace DoCare.Startups.Oracle
{
    public class OracleDesignTimeDefaultDbContextFactory : DesignTimeDbContextFactoryBase<DefaultDbContext>
    {
        private readonly IServiceProvider _serviceProvider;

        public OracleDesignTimeDefaultDbContextFactory()
        { }

        public OracleDesignTimeDefaultDbContextFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public override string GetConnectionString()
        {
            if (_serviceProvider == null)
            {
                IConfiguration configuration = Singleton<IConfiguration>.Instance;
                string str = configuration["FastFrame:DbContexts:DoCare:ConnectionString"]
                    ?? configuration["ConnectionStrings:DefaultDbContext"];
                return str;
            }

            FastFrameOptions options = _serviceProvider.GetFastFrameOptions();
            DbOption contextOptions = options.GetDbOption(typeof(DefaultDbContext));
            if (contextOptions == null)
            {
                throw new FastFrameException($"上下文“{typeof(DefaultDbContext)}”的配置信息不存在");
            }
            return contextOptions.ConnectionString;
        }

        public override IEntityManager GetEntityManager()
        {
            if (_serviceProvider != null)
            {
                return _serviceProvider.GetService<IEntityManager>();
            }
            IEntityConfigurationTypeFinder typeFinder = new EntityConfigurationTypeFinder(new AppDomainAllAssemblyFinder());
            IEntityManager entityManager = new EntityManager(typeFinder);
            entityManager.Initialize();
            return entityManager;
        }

        /// <summary>
        /// 重写以获取是否开启延迟加载代理特性
        /// </summary>
        /// <returns></returns>
        public override bool LazyLoadingProxiesEnabled()
        {
            if (_serviceProvider == null)
            {
                IConfiguration configuration = Singleton<IConfiguration>.Instance;
                return configuration["FastFrame:DbContexts:DoCare:LazyLoadingProxiesEnabled"].CastTo(false);
            }
            FastFrameOptions options = _serviceProvider.GetFastFrameOptions();
            DbOption contextOptions = options.GetDbOption(typeof(DefaultDbContext));
            if (contextOptions == null)
            {
                throw new FastFrameException($"上下文“{typeof(DefaultDbContext)}”的配置信息不存在");
            }

            return contextOptions.LazyLoadingProxiesEnabled;
        }

        public override DbContextOptionsBuilder UseSql(DbContextOptionsBuilder builder, string connString)
        {
            string entryAssemblyName = Assembly.GetExecutingAssembly().GetName().Name;
            Console.WriteLine($"entryAssemblyName: {entryAssemblyName}");

            return builder.UseOracle(connString, b => b.MigrationsAssembly(entryAssemblyName));
        }
    }
}