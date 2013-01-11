using PittsburghBabaTemple.Core.Data.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using NHibernate;
using PittsburghBabaTemple.Core.Interfaces;
using PittsburghBabaTemple.Core.Model;
using System.Reflection;
using System;
using NHibernate.Context;

namespace PittsburghBabaTemple.Core.Data
{
    public class SessionConfiguration
    {
        private static ISessionFactory _sessionFactory;

        public static ISessionFactory Setup()
        {
            return _sessionFactory ?? (_sessionFactory = Configure());
        }

        private static ISessionFactory Configure()
        {
            var searchDirectory = (String.IsNullOrEmpty(AppDomain.CurrentDomain.RelativeSearchPath) ? AppDomain.CurrentDomain.BaseDirectory : AppDomain.CurrentDomain.RelativeSearchPath);
            var modelAssemblyPath = String.Format("{0}\\{1}", searchDirectory, "PittsburghBabaTemple.Core.dll");
            var autoMapping = AutoMap.Assembly(Assembly.LoadFrom(modelAssemblyPath), new AutoMapConfig())
                   .Conventions.Add<CustomForeignKeyConvention>()
                   .Conventions.Add<CascadeConvention>()
                   .Conventions.Add<EnumConvention>()
                   .UseOverridesFromAssemblyOf<IEntity>();
            return Fluently.Configure()
                .Database(FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2008.ConnectionString(x => x
                    .FromConnectionStringWithKey("SSSBT"))
                    .UseOuterJoin()
                    .UseReflectionOptimizer())
                  .Cache(c => c.UseQueryCache())
                  .ExposeConfiguration(c => c.SetProperty("current_session_context_class", "thread_static"))
                  .Mappings(m =>
                  {
                      m.AutoMappings.Add(autoMapping);
                      m.HbmMappings.AddFromAssemblyOf<User>();
                  })
                  .BuildSessionFactory();
        }

        public static ISession GetCurrentSession()
        {
            Setup();
            if (!CurrentSessionContext.HasBind(_sessionFactory))
                CurrentSessionContext.Bind(_sessionFactory.OpenSession());

            return _sessionFactory.GetCurrentSession();
        }

        public static void DisposeCurrentSession()
        {
            ISession currentSession = CurrentSessionContext.Unbind(_sessionFactory);

            currentSession.Close();
            currentSession.Dispose();
        }
    }
}
