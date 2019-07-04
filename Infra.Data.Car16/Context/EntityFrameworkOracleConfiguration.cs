using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Oracle.ManagedDataAccess.EntityFramework;
using Oracle.ManagedDataAccess.Client;

namespace Infra.Data.Cartorio.Context
{
    public sealed class EntityFrameworkOracleConfiguration : DbConfiguration
    {
        public static readonly DbConfiguration Instance = new EntityFrameworkOracleConfiguration();

        public EntityFrameworkOracleConfiguration()
        {
            SetDefaultConnectionFactory(new OracleConnectionFactory());
            SetProviderServices("Oracle.ManagedDataAccess.Client", EFOracleProviderServices.Instance);
            SetProviderFactory("Oracle.ManagedDataAccess.Client", new OracleClientFactory());
        }
    }

}
