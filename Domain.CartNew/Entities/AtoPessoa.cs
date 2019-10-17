using Domain.CartNew.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Entities
{
    [Table("TB_ATO_PES", Schema = "DEZESSEIS_NEW")]
    public class AtoPessoa
    {
        [Key]
        [Column("ID_ATO", Order = 0)]
        public long IdAto { get; set; }

        [Key]
        [Column("SEQPES", Order = 1)]
        public long SeqPes { get; set; }

        [Column("REL")]
        [StringLength(1)]
        public string Relacao { get; set; }

        [Column("TP_PES")]
        public TipoPessoaPrenotacao TipoPessoa { get; set; }
    }
}
