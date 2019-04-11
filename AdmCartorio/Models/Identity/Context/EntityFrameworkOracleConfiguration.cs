using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.EntityFramework;

namespace AdmCartorio.Models.Identity.Context
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