using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Oracle.ManagedDataAccess.EntityFramework;

namespace Infra.Data.Car16.Context
{
    public class OraDbConfiguration: DbConfiguration
    {
        public OraDbConfiguration()
        {
            SetProviderServices("Oracle.ManagedDataAccess.Client", EFOracleProviderServices.Instance);
        }
    }
}
