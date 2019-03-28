using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Core.Entities.Base;

namespace Domain.Core.Entities
{
    public enum TiposFeriado
    {
        [Description("Feriado nacional")] 
        FeriadoNaciona = 1,
        [Description("Feriado estadual")]
        FeriadoEstadual = 2,
        [Description("Feriado municipal")]
        FeriadoMunicipal = 3
    }

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
        public override long Id { get; }

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
