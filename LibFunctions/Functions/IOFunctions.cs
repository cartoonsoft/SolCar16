/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Funções de I/O
by Ronaldo Moreira - ronaldo.poa.rs@gmail.com
------------------------------------------------------------------------------*/
using System;
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
        public static void GerarLogErro(string path, TypeInfo t, Exception ex)
        {
            Exception ex2;
            int cont = 0;
            string str;

            if (!string.IsNullOrEmpty(path))
            {
                string pathFileName = System.IO.Path.Combine(@path, @"errolog.txt");

                try
                {
                    using (var errorLog = new StreamWriter(pathFileName, true))
                    {
                        errorLog.WriteLine(">>>Log em, " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " Class/ Message ==>\"{0}\" \"{1}\" ", (t.Namespace + ": " + t.FullName), ex.Message);
                        ex2 = ex.InnerException;
                        while (ex2 != null)
                        {
                            cont++;
                            str = string.Concat(Enumerable.Repeat(">", cont));
                            errorLog.WriteLine(string.Concat(Enumerable.Repeat(" ", cont)) + str + " InnerException: \"{0}\"", ex2.Message);
                            ex2 = ex2.InnerException;
                        }

                        errorLog.Close();
                    }
                }
                catch (Exception)
                {
                    //faz nada
                    //throw;
                }
            }
        }
    }
}
