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
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;
using Domain.Core.Interfaces.DomainServices;

namespace Domain.Core.Interfaces.DomainServices
{
    /// <summary>
    /// Domain Services Fatory Base
    /// </summary>
    public interface IDomainServicesFactoryBase : IDisposable
    {
        /// <summary>
        /// Generic Domain Service
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IDomainServiceBase<TEntity> GenericDs<TEntity>() where TEntity : class;

    }
}
