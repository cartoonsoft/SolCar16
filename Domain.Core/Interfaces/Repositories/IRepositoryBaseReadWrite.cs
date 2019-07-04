/*----------------------------------------------------------------------------
  _____            _                    _____        __ _   
/  __ \          | |                  /  ___|      / _| |  
| /  \/ __ _ _ __| |_ ___   ___  _ __ \ `--.  ___ | |_| |_ 
| |    / _` | '__| __/ _ \ / _ \| '_ \ `--. \/ _ \|  _| __|
| \__/\ (_| | |  | || (_) | (_) | | | /\__/ / (_) | | | |_ 
 \____/\__,_|_|   \__\___/ \___/|_| |_\____/ \___/|_|  \__|
Todos os direitos reservados ®                       
-----------------------------------------------------------------------------*/
using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Domain.Core.Interfaces.Repositories
{
    public interface IRepositoryBaseReadWrite<TEntity> : IRepositoryBaseRead<TEntity> where TEntity : class
    {
        void Add(TEntity item);
        void AddRange(IEnumerable<TEntity> itens);

        void Update(TEntity item);

        void Remove(long id);
        void Remove(TEntity item);
        void RemoveRange(IEnumerable<TEntity> itens);

        long GetNextValFromOracleSequence(string SequenceName);

    }
}
