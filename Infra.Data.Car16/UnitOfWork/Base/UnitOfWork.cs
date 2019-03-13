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
        private IContextCore contextCore;
        protected DbContextTransaction  transaction = null;
        private Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        /// <summary>
        /// Método construtor
        /// </summary>
        public UnitOfWork()
        {
            contextCore = null;
            transaction = null;
        }

        #region dispose
        private bool disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // Free any other managed objects here.
                    if (this.contextCore != null)
                    {
                        this.contextCore.Dispose();
                    }
                }
            }

            disposed = true;
        }
        #endregion
        
        private void VerifyContext()
        {
            if (contextCore == null)
            {
                throw new ArgumentNullException("Contexto é nulo. verificar!");
            }
        }

        protected IContextCore Context
        {
            get { return this.contextCore; }
            set { contextCore = value; }
        }

        public IRepositoryBase<TEntity> Repository<TEntity>() where TEntity : class
        {
            this.VerifyContext();

            IRepositoryBase<TEntity> repository = null;

            if (Repositories.Keys.Contains(typeof(TEntity)))
            {
                repository = Repositories[typeof(TEntity)] as IRepositoryBase<TEntity>;
            } else {
                repository = new RepositoryBase<TEntity>(contextCore);
            }

            if (repository != null)
            {
                Repositories.Add(typeof(TEntity), repository);
            }

            return repository;
        }

        public virtual void BeginTransaction(IsolationLevel pIsolationLevel = IsolationLevel.ReadCommitted)
        {
            this.VerifyContext();
            if (this.transaction == null)
            {
                this.transaction = this.contextCore.Database.BeginTransaction(pIsolationLevel);
            }
            disposed = false;
        }

        /// <summary>
        /// Fax o SaveChanges do contexto e commit da transacao(se estivaer ativa
        /// </summary>
        /// <returns></returns>
        public virtual int? Commit()
        {
            this.VerifyContext();

            int? resposta = null;

            try
            {
                resposta = this.contextCore.SaveChanges();
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
            this.VerifyContext();
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
