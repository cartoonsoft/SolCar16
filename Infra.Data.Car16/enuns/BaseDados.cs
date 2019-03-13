using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.enuns
{
    public enum BaseDados
    {
        //desenvolvimento
        [Description("Desenv dezesseis_new")] // Desenvolvimento dezesseis_new
        DesenvDezesseisNew = 1,
        [Description("Desenv dezesseis")]
        DesenvDezesseis = 2,

        //homologação
        [Description("Homolo dezesseis_new")]
        HomoloDezesseisNew = 3,
        [Description("Homolo dezesseis")]
        HomoloDezesseis = 4,

        //produção
        [Description("Produção dezesseis_new (cuidado!)")] 
        ProdDezesseisNew = 5,
        [Description("Produção dezesseis (cuidado!)")]
        ProdDezesseis = 6
    }
}

