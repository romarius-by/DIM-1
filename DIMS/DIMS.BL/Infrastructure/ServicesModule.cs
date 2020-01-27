using HIMS.EF.DAL.Data;
using HIMS.EF.DAL.Data.Interfaces;
using HIMS.EF.DAL.Data.Repositories;
using HIMS.EF.DAL.Identity.Repositories;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.BL.Infrastructure
{
    public class ServicesModule : NinjectModule
    {
        private readonly string _connectionString;
        private readonly string _identityConnectionString;

        public ServicesModule(string connectionString, string identityConnectionString)
        {
            _connectionString = connectionString;
            _identityConnectionString = identityConnectionString;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(_connectionString);
            Bind<HIMS.EF.DAL.Identity.Interfaces.IUnitOfWork>().To<IdentityUnitOfWork>().WithConstructorArgument(_identityConnectionString);
            Bind<IProcedureManager>().To<ProcedureManager>().WithConstructorArgument(_connectionString);
        }
    }
}
