﻿/*----------------------------------------------------------------------------
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
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Enumerations;
using Domain.Core.Interfaces.Data;
using Domain.Core.Interfaces.Repositories;

namespace Domain.Core.Interfaces.UnitOfWork
{
    public interface IUnitOfWorkCar16: IUnitOfWork
    {
        //IContextCore Context { get; set; }

        //string GetContextName(BaseDados baseDados);
    }
}
