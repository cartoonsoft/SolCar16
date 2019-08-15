using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.CartNew.Enumerations
{
    public enum GrupoUsuarioEnum
    {
        [Description("Administrador")]
        Admin = 1,
        [Description("Gerente RI")]
        GerenteRI = 2,
        [Description("Usuário atendimento RI")]
        UsuarioRI = 3
    }
}
