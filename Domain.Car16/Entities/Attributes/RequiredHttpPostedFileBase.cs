using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Car16.Entities.Attributes
{
    public class RequiredHttpPostedFileBase : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            List<HttpPostedFileBase> arq = value as List<HttpPostedFileBase>;
            foreach (var item in arq)
            {
                if (item?.ContentLength <= 0 || item == null) return false;
                return true;
            }
            return base.IsValid(value);
        }
    }
}
