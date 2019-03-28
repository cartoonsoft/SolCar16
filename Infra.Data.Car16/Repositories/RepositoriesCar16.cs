using Domain.Car16.Entities;
using Domain.Car16.Interfaces.Repositories;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Entities;
using Domain.Core.Interfaces.Repositories;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.Repositories
{
    class RepositoriesCar16 : RepositoriesBase, IRepositoriesCar16
    {
        private Dictionary<Type, object> Repositories = new Dictionary<Type, object>();

        /// <summary>
        /// Método construtor
        /// </summary>
        /// <param name="context"></param>
        public RepositoriesCar16(ContextMainCar16 context): base(context)
        {
            //
            
        }

        private bool disposedValue = false; // To detect redundant calls

        protected override void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).

                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.
                Repositories = null;

                disposedValue = true;
            }

            base.Dispose(disposing);
        }

        private Object GetRepositoryInstance<T>() where T: EntityBase
        {
            this.VerifyContext();
            Object repository = null;

            try
            {
                if (Repositories.Keys.Contains(typeof(T)))
                {
                    repository = Repositories[typeof(T)] as IRepositoryBase<T>;
                }
                else
                {
                    if (typeof(T).Equals(typeof(Pais)))
                    {
                        repository = new RepositoryPais(this._context as ContextMainCar16);
                    }

                    if (typeof(T).Equals(typeof(Uf)))
                    {
                        repository = new RepositoryUf(this._context as ContextMainCar16);
                    }
                    if (typeof(T).Equals(typeof(Municipio)))
                    {
                        repository = new RepositoryMunicipio(this._context as ContextMainCar16);
                    }

                }

                if (repository != null)
                {
                    Repositories.Add(typeof(T), repository);
                }
                else
                {
                    throw new NullReferenceException("repositório é nulo!");
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Erro na criação de repositorio, tipo: " + typeof(T).Name +" "+ ex.Message);
            }

            return repository; // new TRepository();
        }


        public IRepositoryPais RepositoryPais
        {
            get {
                return GetRepositoryInstance<Pais>() as RepositoryPais;
            }
        }

        public IRepositoryMunicipio RepositoryMunicipio
        {
            get
            {
                return GetRepositoryInstance<Municipio>() as RepositoryMunicipio;
            }
        }

        public IRepositoryUf RepositoryUf
        {
            get
            {
                return GetRepositoryInstance<Uf>() as RepositoryUf;
            }
        }

    }
}
