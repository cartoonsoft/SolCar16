using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Web;

namespace Domain.CartNew.Attributes
{
    public class IsWordFile : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            bool resposta = value != null;

            try
            {
                if (resposta)
                {
                    List<HttpPostedFileBase> arq = value as List<HttpPostedFileBase>;
                    foreach (var item in arq)
                    {
                        resposta = Path.GetExtension(item?.FileName) == ".docx";
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