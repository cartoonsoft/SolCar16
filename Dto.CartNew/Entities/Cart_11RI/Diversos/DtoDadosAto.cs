using Domain.CartNew.Enumerations;
using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
	public class DtoDadosAto: DtoEntityBaseModel
	{
		public DtoDadosAto()
		{
			Pessoas = new List<DtoPessoaPesxPre>();
			ListaCamposValor = new List<DtoCamposValor>();
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

		public string SiglaSeqAto { get; set; }

		public short NumSequenciaAto { get; set; }

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

		public short NumFicha { get; set; }  //numero da fihca informado pelo usuario

		public short DistanciaTopo { get; set; } //distancia do inicio di texto do topa da pagina (cm)

		public TipoFolhaFicha FolhaFicha { get; set; }

		public string StatusAto { get; set; }

		public bool ConfTexto { get; set; }

		public bool ConfDocx { get; set; }

		public bool Ativo { get; set; }

		[DataType(DataType.MultilineText)]
		public string Observacao { get; set; } // VARCHAR2(512)

		public List<DtoCamposValor> ListaCamposValor { get; set; }

		public List<DtoPessoaPesxPre> Pessoas { get; set; }
	}
}
