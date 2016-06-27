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
    [Authorize(Roles="Diretoria")]
    public class CalendariosController : Controller
    {
        private Contexto db = new Contexto();

        // GET: Calendarios
        public ActionResult Index()
        {
            try
            {
                IQueryable<Calendario> calendarios = CalendarioBll.ListarCalendarios();

                return View(calendarios);
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        // GET
        public ActionResult Calendario(int? id)
        {
            try
            {
                Calendario calendario;


                if (id == null)
                {
                    calendario = new Calendario();
                }
                else
                {
                    calendario = CalendarioBll.RetornarCalendario(id);
                }

                return View(calendario);
            }
            catch (Exception exception)
            {
                LogBll.GravarErro(exception, User.Identity.Name);
                return RedirectToAction("Index").ComMensagem(Resources.Geral.ContateAdministrador, TipoMensagem.Erro);
            }
        }

        // POST: Calendarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Calendario([Bind(Include = "ID,Ano,Janeiro,Fevereiro,Marco,Abril,Maio,Junho,Julho,Agosto,Setembro,Outubro,Novembro,Dezembro,ValorMensalidade,ValorAnuidade,DataVencimentoAnuidade")] Calendario calendario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    CalendarioBll calendarioBll = new CalendarioBll();                    

                    if (calendario.ID > 0)
                    {
                        if (CalendarioBll.VericarCalendarioExistente(calendario, TipoOperacao.Update))
                        {
                            return View().ComMensagem(string.Format(Resources.Calendario.CalendarioExistente, calendario.Ano), TipoMensagem.Aviso);
                        }
                        else
                        {
                            calendarioBll.Atualizar(calendario);

                            MensalidadeIntegranteBll mensalidadeIntegranteBll = new MensalidadeIntegranteBll();
                            mensalidadeIntegranteBll.CriarMensalidadesTodosIntegrantesCalendario(calendario);

                            LogBll.GravarInformacao(string.Format(Resources.Calendario.AtualizacaoLog, calendario.ID), "", User.Identity.Name);
                            return RedirectToAction("Index").ComMensagem(Resources.Calendario.CalendarioAtualizado, TipoMensagem.Sucesso);
                        }
                    }
                    else
                    {
                        if (CalendarioBll.VericarCalendarioExistente(calendario, TipoOperacao.Create))
                        {
                            return View().ComMensagem(string.Format(Resources.Calendario.CalendarioExistente, calendario.Ano), TipoMensagem.Aviso);
                        }
                        else
                        {
                            calendarioBll.Criar(calendario);

                            MensalidadeIntegranteBll mensalidadeIntegranteBll = new MensalidadeIntegranteBll();
                            mensalidadeIntegranteBll.CriarMensalidadesTodosIntegrantesCalendario(calendario);

                            LogBll.GravarInformacao(string.Format(Resources.Calendario.CriacaoLog, calendario.ID), "", User.Identity.Name);
                            return RedirectToAction("Index").ComMensagem(Resources.Calendario.CalendarioSalvo, TipoMensagem.Sucesso);
                        }
                    }
                }

                return View(calendario);
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
                Calendario calendario = CalendarioBll.RetornarCalendario(id);
                return View(calendario);
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
