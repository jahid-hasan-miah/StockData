using Autofac;
using StockData.StockManaging.Contexts;
using StockData.StockManaging.Repositories;
using StockData.StockManaging.Services;
using StockData.StockManaging.UnitOfWorks;

namespace StockData.StockManaging.Modules
{
    public class StockingModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        public StockingModule(string connectionStringName, string migrationAssemblyName)
        {
            _connectionString = connectionStringName;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<StockContext>().AsSelf()
                  .WithParameter("connectionString", _connectionString)
                  .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                  .InstancePerLifetimeScope();

            builder.RegisterType<StockContext>().As<IStockContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<CompanyService>().As<ICompanyService>()
               .InstancePerLifetimeScope();
            builder.RegisterType<CompanyRepository>().As<ICompanyRepository>()
             .InstancePerLifetimeScope();

            builder.RegisterType<StockPriceRepository>().As<IStockPriceRepository>()
              .InstancePerLifetimeScope();
            builder.RegisterType<StockManagingUnitOfWork>().As<IStockManagingUnitOfWork>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StockPriceService>().As<IStockPriceService>()
                .InstancePerLifetimeScope();
            
            base.Load(builder);   
        }
    }
}
