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
    public class AdversariosController : Controller
    {
        // GET: Adversario
        public ActionResult Index(string sortOrder, string filtroAtual,
                                    string filtro, int? page,
                                    string ativoFiltro,
                                    string ativoFiltroAtual)
        {
            try
            {
                ViewBag.CurrentSort = sortOrder;
                ViewBag.NomeSort = string.IsNullOrEmpty(sortOrder) ? "nome_desc" : "";

                if (filtro != null)
                {
                    page = 1;
                }
                else
                {
                    filtro = filtroAtual;
                }

                if (ativoFiltro != null)
                {
                    page = 1;
                }
                else
                {
                    ativoFiltro = ativoFiltroAtual;
                }

                ViewBag.FiltroAtual = filtro;
                ViewBag.AtivoFiltroAtual = ativoFiltro;

                AdversarioBll adversarioBll = new AdversarioBll();

                return View("~/Views/Adversarios/Index.cshtml", adversarioBll.BuscarAdversarios(page, filtro, sortOrder, ativoFiltro, 10));
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return View("~/Views/Adversarios/Index.cshtml").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        //GET
        public ActionResult Adversario(int? id)
        {
            try
            {
                Adversario adversario;

                if (id == null)
                {
                    adversario = new Adversario();
                }
                else
                {
                    adversario = AdversarioBll.RetornarAdversario(id);
                }

                return View(adversario);
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adversario([Bind(Include = "ID,Nome,NomeContato,Telefone,Ativo")] Adversario adversario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    AdversarioBll adversarioBll = new AdversarioBll();

                    if (adversario.ID > 0)
                    {
                        adversarioBll.Atualizar(adversario);
                        LogBll.GravarInformacao(string.Format(Resources.Adversario.AtualizacaoLog, adversario.ID), "", User.Identity.Name);
                        return RedirectToAction("Index").ComMensagem(Resources.Adversario.AdversarioSalvo, TipoMensagem.Sucesso);
                    }
                    else
                    {
                        adversarioBll.Criar(adversario);
                        LogBll.GravarInformacao(string.Format(Resources.Calendario.CriacaoLog, adversario.ID), "", User.Identity.Name);
                        return RedirectToAction("Index").ComMensagem(Resources.Adversario.AdversarioSalvo, TipoMensagem.Sucesso);
                     
                    }
                }

                return View(adversario);
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }
    }
}