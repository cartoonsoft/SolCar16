using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI.Diversos
{
    public class DtoMenuAcaoList
    {
        [Key]
        public long? Id { get; set; }
        public long? IdMenuPai { get; set; }
        public long? IdAcao { get; set; }
        public long IdTipoMenu { get; set; }
        public long? IdClaimRole { get; set; }
        public long IdCtaAcessoSist { get; set; }
        public int? Ordem { get; set; }
        public string DescricaoMenu { get; set; }
        public string DescricaoMenuMobile { get; set; }
        public string IconeWeb { get; set; }
        public string IconeMobile { get; set; }
        public string DescricaoTip { get; set; } //usar tips
        public string DescricaoBalao { get; set; }
        public string Orientacao { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Parametros { get; set; }
        public bool Permissao { get; set; }
        public bool Ativo { get; set; }
        public bool EmManutencao { get; set; }
    }
}
