using SaoBei.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SaoBei.Negocio;
using System.Web.Security;

namespace SaoBei.Controllers
{
    public class AutenticacaoController : Controller
    {
        // GET: Autenticacao
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogOn()
        {
            if (HttpContext.User.Identity != null && User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home").ComMensagem("Você não tem permissão para acesso.", TipoMensagem.Info);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult LogOn(string email, string senha, string returnUrl)
        {
            try
            {
                IntegranteBll integranteBll = new IntegranteBll();

                Integrante integrante = integranteBll.LogOn(email, senha);

                if (integrante != null)
                {
                    FormsAuthentication.SetAuthCookie(email, false);

                    if (Url.IsLocalUrl(returnUrl) && returnUrl.Length > 1 && returnUrl.StartsWith("/")
                        && !returnUrl.StartsWith("//") && !returnUrl.StartsWith("/\\"))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Mensalidades");
                    }
                }
                else
                {
                    return View().ComMensagem(Resources.Geral.EmailSenhaInvalidos, TipoMensagem.Aviso);
                }
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, "LogOn");
                return View().ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("LogOn", "Autenticacao");
        }
    }
}