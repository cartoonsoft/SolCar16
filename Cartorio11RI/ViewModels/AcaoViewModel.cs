using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace Cartorio11RI.ViewModels
{
    public class AcaoViewModel
    {
        public AcaoViewModel()
        {
            this.ListaUsersAcao = new List<UsuarioAcaoViewModel>();
        }

        [Key]
        public long? Id { get; set; }
        public long IdContaAcessoSistema { get; set; }
        public long SeqAcesso { get; set; }
        public string Programa { get; set; }
        public string Obs { get; set; }
        public string DescricaoPequeno { get; set; }
        public string DescricaoMedio { get; set; }
        public string DescricaoGrande { get; set; }
        public string DescricaoTip { get; set; } //usar tips
        public string DescricaoBalao { get; set; }
        public string Orientacao { get; set; }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Parametros { get; set; }
        public string IconeWeb { get; set; }
        public string IconeMobile { get; set; }
        public bool Ativo { get; set; }
        public bool EmManutencao { get; set; }

        public List<UsuarioAcaoViewModel> ListaUsersAcao { get; set; }
    }
}
