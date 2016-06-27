using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SaoBei.Models
{
    public class TempDataActionResult : ActionResult
    {
        private readonly ActionResult _actionResult;
        private readonly string _mensagem;
        private readonly string _classeAlert;

        public TempDataActionResult(ActionResult actionResult, string mensagem, TipoMensagem classeAlert)
        {
            _actionResult = actionResult;
            _mensagem = mensagem;

            switch (classeAlert)
            {
                case TipoMensagem.Aviso:
                    _classeAlert = "alert-warning";
                    break;
                case TipoMensagem.Erro:
                    _classeAlert = "alert-danger";
                    break;
                case TipoMensagem.Sucesso:
                    _classeAlert = "alert-success";
                    break;
                case TipoMensagem.Info:
                    _classeAlert = "alert-info";
                    break;
            }
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.Controller.TempData["Mensagem"] = _mensagem;
            context.Controller.TempData["ClasseAlert"] = _classeAlert;
            _actionResult.ExecuteResult(context);
        }
    }
}