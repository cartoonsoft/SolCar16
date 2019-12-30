﻿using Domain.CartNew.Enumerations;
using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoAto : DtoEntityBaseModel
    {
        public DtoAto()
        {
            this.ListaPessoasAto = new List<DtoPessoaAto>();
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

        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? DataAlteracao { get; set; }

        public string Codigo
        {
            get { return this.SiglaSeqAto + "/" + this.NumSequenciaAto.ToString(); }
        }

        [DataType(DataType.Date)]
        public DateTime? DataAto { get; set; }

        public string NumMatricula { get; set; } //NRO_MATRICULA        VARCHAR2(20)

        public string DescricaoAto { get; set; }

        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Texto { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observacao { get; set; } // VARCHAR2(512)

        public string StatusAto { get; set; }

        public bool Ativo { get; set; }

        public short NumFicha { get; set; }  //numero da fihca informado pelo usuario

        public string SiglaSeqAto { get; set; }

        public short NumSequenciaAto { get; set; }

        public short DistanciaTopo { get; set; } //distancia do inicio di texto do topa da pagina (cm)

        public TipoFolhaFicha FolhaFicha { get; set; }

        public string IdsPessoasSelecionadas { get; set; } //string com ids das pessoas que forma seleconadas separadaos por ";"

        public List<DtoPessoaAto> ListaPessoasAto { get; set; }

        public DtoPREIMO PREIMO { get; set; }

    }
}
