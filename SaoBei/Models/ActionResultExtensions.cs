using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaoBei.Models
{
    public static class ActionResultExtensions
    {
        /// <summary>
        /// Redireciona para uma ActionResult retornando uma mensagem de confirmação para a view
        /// </summary>
        /// <param name="actionResult"></param>
        /// <param name="mensagem"></param>
        /// <param name="classeAlert">classe do alerta a ser exibido (alert-error, alert-success, alert-info)</param>
        /// <returns></returns>
        public static ActionResult ComMensagem(this ActionResult actionResult, string mensagem, TipoMensagem classeAlert)
        {
            return new TempDataActionResult(actionResult, mensagem, classeAlert);
        }
    }
}