/*
---------1---------2---------3---------4---------5---------6---------7---------8
01234567890123456789012345678901234567890123456789012345678901234567890123456789
--------------------------------------------------------------------------------
Funções do App
by Ronaldo Moreira - ronaldo.poa.rs@gmail.com
------------------------------------------------------------------------------*/
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LibFunctions.Functions.AppFuncs
{
    public static class AppFunctions
    {
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        private static CompilerResults CompileScript(string source)
        {
            CompilerParameters parms = new CompilerParameters();

            parms.GenerateExecutable = false;
            parms.GenerateInMemory = true;
            parms.IncludeDebugInformation = false;

            CodeDomProvider compiler = CSharpCodeProvider.CreateProvider("CSharp");

            return compiler.CompileAssemblyFromSource(parms, source);
        }

        private static string GetCodeExpression(string expression)
        {
            string code = string.Format  // Note: Use "{{" to denote a single "{"
            (
                "public static class Func{{ public static double func(){{ return {0};}}}}",
                expression
            );

            return code;
        }

        public static bool ValideFormula(string expression)
        {
            bool resultado = false;

            try
            {
                var code = GetCodeExpression(expression);
                CompilerResults compilerResults = CompileScript(code);
                resultado = !compilerResults.Errors.HasErrors;
            }
            catch (Exception)
            {

            }

            return resultado;
        }

        public static double ExecutaFormula(string expression)
        {
            var code = GetCodeExpression(expression);
            CompilerResults compilerResults = CompileScript(code);

            if (compilerResults.Errors.HasErrors)
            {
                throw new InvalidOperationException("Expressão contém erro de sitaxe.");
            }

            Assembly assembly = compilerResults.CompiledAssembly;
            MethodInfo method = assembly.GetType("Func").GetMethod("func");

            return (double)method.Invoke(null, null);
        }

        public static bool IsDebugMode()
        {
            bool isDebug = false;

            #if DEBUG
            isDebug = true;
            #endif

            return isDebug;
        }

        public static void SendEmail(
            string subject,
            string message,
            string emitenteNome,
            string contaEmail,
            string contaSenha,
            string contaSmtp,
            int contaPorta,
            bool ssl,
            List<string> destinatarios,
            Dictionary<string, Stream> files = null)
        {
            var loginInfo = new NetworkCredential(contaEmail, contaSenha);
            var msg = new MailMessage();
            var smtpClient = new SmtpClient(contaSmtp, contaPorta);

            if (string.IsNullOrWhiteSpace(emitenteNome))
            {
                msg.From = new MailAddress(contaEmail, emitenteNome);
            }
            else
            {
                msg.From = new MailAddress(contaEmail);
            }

            msg.Subject = subject;
            msg.Body = message;
            msg.IsBodyHtml = true;

            if (destinatarios == null || destinatarios.Count == 0)
            {
                throw new Exception("Nenhum destinatário informado.");
            }
            else
            {
                foreach (var destinatario in destinatarios)
                {
                    //todo: ronaldo arrumar ValidaEmail
                    //if (ValidaEmail(destinatario))
                    //    msg.To.Add(new MailAddress(destinatario));
                }
            }

            if (files != null)
            {
                foreach (var file in files)
                {
                    Attachment anexo = new Attachment(file.Value, file.Key);
                    msg.Attachments.Add(anexo);
                }
            }

            smtpClient.EnableSsl = ssl;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(msg);
        }

        public static List<int> GetIds(string idList, char pchar = ',')
        {
            string[] values = idList.Split(pchar);
            List<int> ids = new List<int>(values.Length);

            foreach (string s in values)
            {
                int i;

                if (int.TryParse(s, out i))
                {
                    ids.Add(i);
                }
            }

            return ids;
        }

        public static string Cryptografa(string value)
        {
            var hash = System.Security.Cryptography.SHA1.Create();
            var encoder = new ASCIIEncoding();
            var combined = encoder.GetBytes(value ?? "");
            return BitConverter.ToString(hash.ComputeHash(combined)).ToLower().Replace("-", "");
        }


        public static bool EnviaEmail(string emailDestino, string assunto, string conteudo)
        {
            //Define os dados do e-mail
            string nomeRemetente = "Contato Help Desk";
            string emailRemetente = "suporte@pontoid.com.br";
            string senha = "jbcm1015";

            //Host da porta SMTP
            string SMTP = "mail.pontoid.com.br";

            string emailDestinatario = emailDestino;
            //string emailComCopia        = "email@comcopia.com.br";
            //string emailComCopiaOculta  = "email@comcopiaoculta.com.br";

            string assuntoMensagem = assunto;
            string conteudoMensagem = conteudo;

            //Cria objeto com dados do e-mail.
            MailMessage objEmail = new MailMessage();

            //Define o Campo From e ReplyTo do e-mail.
            objEmail.From = new MailAddress(nomeRemetente + "<" + emailRemetente + ">");

            //Define os destinatários do e-mail.
            objEmail.To.Add(emailDestinatario);

            //Enviar cópia para.
            //objEmail.CC.Add(emailComCopia);

            //Enviar cópia oculta para.
            //objEmail.Bcc.Add(emailComCopiaOculta);

            //Define a prioridade do e-mail.
            objEmail.Priority = MailPriority.Normal;

            //Define o formato do e-mail HTML (caso não queira HTML alocar valor false)
            objEmail.IsBodyHtml = true;

            //Define título do e-mail.
            objEmail.Subject = assuntoMensagem;

            //Define o corpo do e-mail.
            objEmail.Body = conteudoMensagem;

            //Para evitar problemas de caracteres "estranhos", configuramos o charset para "ISO-8859-1"
            objEmail.SubjectEncoding = Encoding.GetEncoding("ISO-8859-1");
            objEmail.BodyEncoding = Encoding.GetEncoding("ISO-8859-1");


            // Caso queira enviar um arquivo anexo
            //Caminho do arquivo a ser enviado como anexo
            //string arquivo = Server.MapPath("arquivo.jpg");

            // Ou especifique o caminho manualmente
            //string arquivo = @"e:\home\LoginFTP\Web\arquivo.jpg";

            // Cria o anexo para o e-mail
            //Attachment anexo = new Attachment(arquivo, System.Net.Mime.MediaTypeNames.Application.Octet);

            // Anexa o arquivo a mensagem de email
            //objEmail.Attachments.Add(anexo);

            //Cria objeto com os dados do SMTP
            SmtpClient objSmtp = new SmtpClient();

            //Alocamos o endereço do host para enviar os e-mails  
            objSmtp.Credentials = new NetworkCredential(emailRemetente, senha);
            objSmtp.Host = SMTP;
            objSmtp.Port = 587;
            //Caso utilize conta de email do exchange da locaweb deve habilitar o SSL
            //objEmail.EnableSsl = true;

            //Enviamos o e-mail através do método .send()
            try
            {
                if (IsDebugMode())
                {
                    objEmail.To.Clear();
                    objEmail.To.Add("ronaldo.poa.rs@gmail.com");
                }

                objSmtp.Send(objEmail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                //excluímos o objeto de e-mail da memória
                objEmail.Dispose();
                //anexo.Dispose();
            }
        }

        public static void SendEmail(
            string subject,
            string message,
            string emitenteNome,
            string contaEmail,
            string contaSenha,
            string contaSmtp,
            int contaPorta,
            bool ssl,
            string emailDestinatario,
            Dictionary<string, Stream> files = null)
        {
            List<string> destinatarios = new List<string>();
            if (!string.IsNullOrWhiteSpace(emailDestinatario))
                destinatarios.Add(emailDestinatario);

            SendEmail(subject,
                message,
                emitenteNome,
                contaEmail,
                contaSenha,
                contaSmtp,
                contaPorta,
                ssl,
                destinatarios.ToString(),
                files);
        }

        public static long StrtoLong(string valor)
        {
            long valorTmp = 0;
            long.TryParse(Regex.Match(valor, @"\d+").Value, out valorTmp);

            return valorTmp;
        }

    }
}
