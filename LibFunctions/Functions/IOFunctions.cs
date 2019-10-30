﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace LibFunctions.Functions.IOAdmCartorio
{
    public static class IOFunctions
    {
        public static void GerarLogErro(TypeInfo t, Exception ex)
        {
            Exception ex2;
            int cont = 0;
            string str;
            string path = HostingEnvironment.MapPath(@"~/App_Data/errolog.txt");
            
            using (var errorLog = new StreamWriter(path, true))
            {
                try
                {
                    errorLog.WriteLine(">>>Log em, " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " Class/ Message ==>\"{0}\" \"{1}\" ", (t.Namespace + ": " + t.FullName), ex.Message);
                    ex2 = ex.InnerException;
                    while (ex2 != null)
                    {
                        cont++;
                        str = string.Concat(Enumerable.Repeat(">", cont));
                        errorLog.WriteLine(string.Concat(Enumerable.Repeat(" ", cont)) + str + " InnerException: \"{0}\"", ex.Message);
                        ex2 = ex2.InnerException;
                    }

                    errorLog.Close();
                }
                catch (Exception)
                {
                    //throw;
                }
            }
        }
    }
}