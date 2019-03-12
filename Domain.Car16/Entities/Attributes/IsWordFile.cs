using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Domain.Car16.Entities.Attributes
{
    public class IsWordFile : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            List<HttpPostedFileBase> arq = value as List<HttpPostedFileBase>;
            foreach (var item in arq)
            {
                return Path.GetExtension(item?.FileName) == ".docx";
            }
            return base.IsValid(value);
        }
    }
}
