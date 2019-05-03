using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;


namespace Domain.Car16.Attributes
{
    public class RequiredHttpPostedFileBase : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool resposta = value != null;

            if (resposta)
            {
                List<HttpPostedFileBase> arq = value as List<HttpPostedFileBase>;
                foreach (var item in arq)
                {
                    if (item?.ContentLength <= 0 || item == null)
                    {
                        resposta = false;
                    }
                }
            }

            return resposta;
        }
    }
}