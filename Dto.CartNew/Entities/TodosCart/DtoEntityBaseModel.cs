﻿using Dto.CartNew.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.TodosCart
{
    public abstract class DtoEntityBaseModel : IDtoEntityBaseModel
    {
        private long? _Id;

        /// <summary>
        /// Get or set the persisten object identifier
        /// </summary>
        public virtual long? Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
                OnSetId();
            }
        }

        /// <summary>
        /// Evento diparado quando um Id é definido
        /// </summary>
        protected virtual void OnSetId()
        {

        }

    }
}