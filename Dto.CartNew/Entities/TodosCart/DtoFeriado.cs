﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.CartNew.Enumerations;
using Dto.CartNew.Base;
using Dto.CartNew.Entities.TodosCart;

namespace Dto.CartNew.Entities.Cart_11RI.TodosCart
{
    public class DtoFeriado : DtoEntityBaseModel
    {
        [Key]
        public override long? Id { get; set; }

        public long? IdUf { get; set; }

        public long? IdMunicipio { get; set; }

        public int Ano { get; set; }

        public DateTime DataFeriado { get; set; }

        public string Descricao { get; set; }

        public bool PontoFacultativo { get; set; }

        public TiposFeriado TipoFeriado { get; set; }

        public bool Ativo { get; set; }

    }
}
