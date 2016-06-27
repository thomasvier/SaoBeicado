using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaoBei.Models;

namespace SaoBei.Negocio
{
    public class LogBll
    {
        public static void GravarInformacao(string mensagem, string observacao, string usuario)
        {
            Log log = new Log()
            {
                TipoLog = TipoLog.Informacao,
                Mensagem = mensagem,
                Observacao = observacao,
                Usuario = usuario,
                DataHora = DateTime.Now
            };

            using (Contexto db = new Contexto())
            {
                db.Logs.Add(log);
                db.SaveChanges();
            }
        }

        public static void GravarErro(Exception exception, string usuario)
        {
            Log log = new Log()
            {
                TipoLog = TipoLog.Erro,
                Observacao = exception.StackTrace,
                Usuario = usuario,
                DataHora = DateTime.Now
            };

            if (exception.InnerException != null)
            {
                log.Mensagem = exception.InnerException.ToString();
            }
            else
            {
                log.Mensagem = exception.Message;
            }

            using (Contexto db = new Contexto())
            {
                db.Logs.Add(log);
                db.SaveChanges();
            }
        }
    }
}