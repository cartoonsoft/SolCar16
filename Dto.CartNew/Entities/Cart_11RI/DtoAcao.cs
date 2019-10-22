using Dto.CartNew.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.CartNew.Entities.Cart_11RI
{
    public class DtoAcao: DtoEntityBaseModel
    {
        [Key]
        public override long? Id { get; set; }
        public long IdCtaAcessoSist { get; set; }
        public long? IdClaimRole { get; set; }
        public long? SeqAcesso { get; set; }
        public string Programa { get; set; }
        public string Obs { get; set; }
        public string DescricaoPequeno { get; set; }
        public string DescricaoMedio { get; set; }
        public string DescricaoGrande { get; set; }
        public string DescricaoTip { get; set; } //usar tips
        public string DescricaoBalao { get; set; }
        public string Orientacao { get; set; } // orientações sobre o que faz a action
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Parametros { get; set; }
        public string IconeWeb { get; set; }
        public string IconeMobile { get; set; }
        public bool Ativo { get; set; }
        public bool EmManutencao { get; set; }
    }
}
