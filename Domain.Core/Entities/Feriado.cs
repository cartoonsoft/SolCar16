using System.Linq;
using System.Text;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Core.Entities.Base;
using Domain.Core.enums;

namespace Domain.Core.Entities
{
    [Table("TB_FERIADO")]
    public class Feriado : EntityBase
    {
        private readonly string _seqName = "SQ_FERIADO";

        public string SeqName
        {
            get { return _seqName; }
        }

        [Key]
        [Column("ID_FERIADO")]
        public override long? Id { get; set; }

        public int Ano { get; set; }

        public DateTime DataFeriado { get; set; }

        public string Descricao { get; set; }

        public bool PontoFacultativo { get; set; }

        public TiposFeriado TipoFeriado { get; set; }

        public bool Ativo { get; set; }

        public long? IdUf { get; set; }

        public long? IdMunicipio { get; set; }
    }
}
