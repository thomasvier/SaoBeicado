using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SaoBei.Models;
using SaoBei.Negocio;

namespace SaoBei.Controllers
{
    [Authorize(Roles = "Diretoria")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Jogo jogo = JogoBll.RetornarProximoJogoConfirmado();

            if (jogo != null)
            {
                ViewBag.Adversario = jogo.Adversario.Nome;
                ViewBag.Local = jogo.LocalJogo.Nome;
                ViewBag.Data = string.Format("{0:dd/MM/yyyy HH:mm}", jogo.Data);
            }
            else
            {
                ViewBag.ExisteJogo = "hidden";
            }

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}