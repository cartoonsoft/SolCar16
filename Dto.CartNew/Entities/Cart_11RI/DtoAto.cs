﻿using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoAto : DtoEntityBaseModel
    {
        public DtoAto()
        {
            this.ListaPessoasAto = new List<DtoPessoaAto>();
            this.ListaDocxsAto = new List<DtoDocx>();
            this.StatusAto = string.Empty;
        }

        [Key]
        public override long? Id { get; set; }

        public long IdCtaAcessoSist { get; set; } //ID_CTA_ACESSO_SIST NUMERIC(38, 0)       not null,

        public long IdLivro { get; set; }

        public long IdTipoAto { get; set; }

        public long IdPrenotacao { get; set; }

        public long IdModeloDoc { get; set; }

        public string IdUsuarioCadastro { get; set; }

        public string IdUsuarioAlteracao { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAlteracao { get; set; }

        public string Codigo
        {
            get { return this.SiglaSeqAto + "/" + this.NumSequenciaAto.ToString(); }
        }

        public string SiglaSeqAto { get; set; }

        public short NumSequenciaAto { get; set; } //numeric(5,0)

        public DateTime? DataAto { get; set; }

        public string NumMatricula { get; set; } //NRO_MATRICULA        VARCHAR2(20)

        public string DescricaoAto { get; set; }

        public string Texto { get; set; }

        public string StatusAto { get; set; }

        public bool Ativo { get; set; }

        public string Observacao { get; set; } // VARCHAR2(512)

        public short NumFicha { get; set; }  //numero da fihca informado pelo usuario

        public bool GeradoFicha { get; set; }

        public List<DtoPessoaAto> ListaPessoasAto { get; set; }  //pessoas selecionadas para este ato, não é necessariamente, todas as pesoas da prenotação.

        public List<DtoDocx> ListaDocxsAto { get; set; }  //Lista de docx (Fichas) gerados para o ato

    }
}
