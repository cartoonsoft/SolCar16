using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Oracle.ManagedDataAccess.EntityFramework;
using Oracle.ManagedDataAccess.Client;

namespace Infra.Data.Car16.Context
{
    public class OraDbConfiguration: DbConfiguration
    {
        public OraDbConfiguration()
        {
            SetDefaultConnectionFactory(new OracleConnectionFactory());
            SetProviderServices("Oracle.ManagedDataAccess.Client", EFOracleProviderServices.Instance);
            SetProviderFactory("Oracle.ManagedDataAccess.Client", new OracleClientFactory());
        }
    }
}
