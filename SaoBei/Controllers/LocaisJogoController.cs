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
    public class LocaisJogoController : Controller
    {
        // GET: LocaisJogo
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

                LocalJogoBll localJogoBll = new LocalJogoBll();

                return View("~/Views/LocaisJogo/Index.cshtml", localJogoBll.BuscarLocalJogos(page, filtro, sortOrder, ativoFiltro, 10));
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return View("~/Views/LocaisJogo/Index.cshtml").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        //GET
        public ActionResult LocalJogo(int? id)
        {
            try
            {
                LocalJogo localJogo;

                if (id == null)
                {
                    localJogo = new LocalJogo();
                }
                else
                {
                    localJogo = LocalJogoBll.RetornarLocalJogo(id);
                }

                return View(localJogo);
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LocalJogo([Bind(Include = "ID,Nome,ValorJogo,Ativo")] LocalJogo localJogo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    LocalJogoBll localJogoBll = new LocalJogoBll();

                    if (localJogo.ID > 0)
                    {
                        localJogoBll.Atualizar(localJogo);
                        LogBll.GravarInformacao(string.Format(Resources.LocalJogo.AtualizacaoLog, localJogo.ID), "", User.Identity.Name);
                        return RedirectToAction("Index").ComMensagem(Resources.LocalJogo.LocalJogoSalvo, TipoMensagem.Sucesso);
                    }
                    else
                    {
                        localJogoBll.Criar(localJogo);
                        LogBll.GravarInformacao(string.Format(Resources.LocalJogo.CriacaoLog, localJogo.ID), "", User.Identity.Name);
                        return RedirectToAction("Index").ComMensagem(Resources.LocalJogo.LocalJogoSalvo, TipoMensagem.Sucesso);

                    }
                }

                return View(localJogo);
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        public ActionResult Detalhes(int? id)
        {
            try
            {
                LocalJogo localJogo = LocalJogoBll.RetornarLocalJogo(id);

                return View(localJogo);
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }
    }
}