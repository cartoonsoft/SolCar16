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
using System.Data;
using System.Data.Entity;
using System.Linq;
using Oracle.ManagedDataAccess.Client;
using Domain.Core.Interfaces.UnitOfWork;
using Domain.Core.Interfaces.Repositories;
using Domain.Core.Interfaces.Data;
using Infra.Data.Car16.Repositories.Base;

namespace Infra.Data.Car16.UnitOfWorkCar16.Base
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IContextCore _contextCore;
        protected DbContextTransaction  transaction = null;

        /// <summary>
        /// Método construtor
        /// </summary>
        public UnitOfWork(IContextCore contextCore)
        {
            _contextCore = contextCore;
            transaction = null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                    if (transaction != null)
                    {
                        transaction.Dispose();
                    }

                    if (_contextCore != null)
                    {
                        _contextCore.Dispose();
                    }
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

                disposedValue = true;
            }
        }

        // override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UnitOfWork() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

        public IRepositoriesBase repositoriesBase => throw new NotImplementedException();

        public virtual void BeginTransaction(IsolationLevel pIsolationLevel = IsolationLevel.ReadCommitted)
        {
            if (this.transaction == null)
            {
                this.transaction = this._contextCore.Database.BeginTransaction(pIsolationLevel);
            }
            //disposed = false;
        }

        /// <summary>
        /// Fax o SaveChanges do contexto e commit da transacao(se estivaer ativa
        /// </summary>
        /// <returns></returns>
        public virtual int? Commit()
        {
            int? resposta = null;

            try
            {
                resposta = this._contextCore.SaveChanges();
                this.CommitTransaction();
                return resposta;
            }
            catch (OracleException exOracle)
            {
                this.RollbackTransaction();
                this.SaveLog(exOracle);

                throw exOracle;
            }

            catch (Exception exGeneirc)
            {
                this.RollbackTransaction();
                this.SaveLog(exGeneirc);

                throw exGeneirc;
            }

        }

        public virtual void RollBack()
        {
            this.RollbackTransaction();
        }

        private void CommitTransaction()
        {
            if (this.transaction != null)
            {
                this.transaction.Commit();
                this.transaction.Dispose();
            }
        }

        private void RollbackTransaction()
        {
            if (this.transaction != null)
            {
                this.transaction.Rollback();
                this.transaction.Dispose();
            }
        }

        protected virtual void SaveLog(OracleException ex)
        {
            //todo: 3 implementar savelog -ronaldo
            //throw new NotImplementedException("Voce deve implementar o metódo SaveLog!");
        }

        protected virtual void SaveLog(Exception ex)
        {
            //todo: implementar savelog
            //throw new NotImplementedException("Voce deve implementar o metódo SaveLog!");
        }


    }
}
