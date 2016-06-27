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
    public class IntegrantesController : Controller
    {
        Contexto db = new Contexto();

        // GET: Integrantes
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

                IntegranteBll integranteBll = new IntegranteBll();

                return View("~/Views/Integrantes/Index.cshtml", integranteBll.BuscarIntegrantes(page, filtro, sortOrder, ativoFiltro, 10));
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return View("~/Views/Integrantes/Index.cshtml").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }
        
        //GET
        public ActionResult Integrante(int? id)
        {
            try
            {
                Integrante integrante;

                if (id == null)
                {
                    integrante = new Integrante();
                }
                else
                {
                    integrante = IntegranteBll.RetornarIntegrante(id);
                }

                return View(integrante);
            }
            catch(Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Integrante([Bind(Include = "ID,Nome,DataNascimento,Telefone,Email,Ativo,Senha")] Integrante integrante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    IntegranteBll integranteBll = new IntegranteBll();

                    if (integrante.ID > 0)
                    {
                        if (IntegranteBll.VericarEmailExistente(integrante, TipoOperacao.Update))
                        {
                            return View().ComMensagem(Resources.Integrantes.IntegranteExistente, TipoMensagem.Aviso);
                        }
                        else
                        {
                            integranteBll.Atualizar(integrante);

                            MensalidadeIntegranteBll mensalidadeIntegranteBll = new MensalidadeIntegranteBll();
                            mensalidadeIntegranteBll.CriarMensalidadesIntegrante(integrante);

                            LogBll.GravarInformacao(string.Format(Resources.Integrantes.AtualizacaoLog, integrante.ID), "", User.Identity.Name);
                            return RedirectToAction("Index").ComMensagem(Resources.Integrantes.IntegranteSalvo, TipoMensagem.Sucesso);
                        }
                    }
                    else
                    {
                        if (IntegranteBll.VericarEmailExistente(integrante, TipoOperacao.Create))
                        {
                            return View().ComMensagem(Resources.Integrantes.IntegranteExistente, TipoMensagem.Aviso);
                        }
                        else
                        {
                            integranteBll.Criar(integrante);
                            LogBll.GravarInformacao(string.Format(Resources.Calendario.CriacaoLog, integrante.ID), "", User.Identity.Name);

                            MensalidadeIntegranteBll mensalidadeIntegranteBll = new MensalidadeIntegranteBll();
                            mensalidadeIntegranteBll.CriarMensalidadesIntegrante(integrante);

                            return RedirectToAction("Index").ComMensagem(Resources.Integrantes.IntegranteSalvo, TipoMensagem.Sucesso);
                        }
                    }
                }

                return View(integrante);
            }
            catch(Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        //GET
        public ActionResult Detalhes(int? id)
        {
            try
            {
                Integrante integrante = IntegranteBll.RetornarIntegrante(id);

                return View(integrante);
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
