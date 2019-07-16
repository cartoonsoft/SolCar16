/*----------------------------------------------------------------------------
  _____            _                    _____        __ _   
/  __ \          | |                  /  ___|      / _| |  
| /  \/ __ _ _ __| |_ ___   ___  _ __ \ `--.  ___ | |_| |_ 
| |    / _` | '__| __/ _ \ / _ \| '_ \ `--. \/ _ \|  _| __|
| \__/\ (_| | |  | || (_) | (_) | | | /\__/ / (_) | | | |_ 
 \____/\__,_|_|   \__\___/ \___/|_| |_\____/ \___/|_|  \__|
Todos os direitos reservados ®                       
-----------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Repositories;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace Infra.Data.Cartorio.Repositories.Base
{
    public class RepositoryBaseReadWrite<TEntity> : RepositoryBaseRead<TEntity>, IRepositoryBaseReadWrite<TEntity> where TEntity : class
    {
        public RepositoryBaseReadWrite(IContextCore context): base(context)
        {
            //
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }

            base.Dispose(disposing);
        }
        #endregion

        public void Add(TEntity entity)
        {
            //todo: ronaldo incrementar Id
            this.DbSet.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> itens)
        {
            (this.DbSet as DbSet).AddRange(itens);
        }

        public void Remove(long id)
        {
            var item = this.DbSet.Find(id);
            if (item != null)
            {
                this.Remove(item);
            }
        }

        public void Remove(TEntity item)
        {
            this.DbSet.Remove(item);
        }

        public void RemoveRange(IEnumerable<TEntity> itens)
        {
            (this.DbSet as DbSet).RemoveRange(itens);
        }

        public int SaveChanges()
        {
            int resposta =  this.Context.SaveChanges();

            return resposta;
        }

        public void Update(TEntity item)
        {
            if (item != null)
            {
                this.DbSet.Attach(item);
                this.Context.Entry(item).State = EntityState.Modified;
            }
        }

        /// <summary>
        /// Busca o NextVal de uma sequence
        /// </summary>
        /// <param name="SequenceName"></param>
        /// <returns></returns>
        public long GetNextValFromOracleSequence(string SequenceName)
        {
            long SeqTmp = 0;
            ConnectionStringsSection connectionStringsSection = WebConfigurationManager.GetSection("connectionStrings") as ConnectionStringsSection;
            var connStr = connectionStringsSection.ConnectionStrings[this.Context.ContextName].ConnectionString;

            using (OracleConnection conn = new OracleConnection(connStr))
            {
                conn.Open();
                using (OracleCommand command = new OracleCommand(string.Format("select {0}.NEXTVAL from dual ", SequenceName), conn))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.BindByName = true;
                    using (OracleDataReader row = command.ExecuteReader())
                    {
                        while (row.Read())
                        {
                            SeqTmp = row.GetOracleDecimal(0).ToInt64();
                        }
                    }
                }
                conn.Close();
            }

            return SeqTmp;
        }
    }
}