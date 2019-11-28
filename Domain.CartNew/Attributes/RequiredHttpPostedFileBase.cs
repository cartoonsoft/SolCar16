using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Domain.CartNew.Attributes
{
    public class RequiredHttpPostedFileBase : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool resposta = value != null;

            try
            {
                if (resposta)
                {
                    List<HttpPostedFileBase> arquivos = value as List<HttpPostedFileBase>;
                    foreach (var item in arquivos)
                    {
                        if (item?.ContentLength <= 0 || item == null)
                        {
                            resposta = false;
                        }
                    }
                }
            }
            catch (Exception)
            {
                //
            }


            return resposta;
        }
    }
}