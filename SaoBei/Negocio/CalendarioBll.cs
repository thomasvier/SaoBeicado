using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaoBei.Models;
using System.Data.Entity;

namespace SaoBei.Negocio
{
    public class CalendarioBll
    {
        Contexto db;

        public CalendarioBll()
        {
            db = new Contexto();
        }

        public static Calendario RetornarCalendario(int? id)
        {
            Contexto db = new Contexto();

            Calendario calendario = db.Calendarios.Where(c => c.ID == id).FirstOrDefault();

            return calendario;
        }

        public static Calendario RetornarCalendario(int ano)
        {
            Contexto db = new Contexto();

            Calendario calendario = db.Calendarios.Where(c => c.Ano == ano).FirstOrDefault();

            return calendario;
        }

        public static IQueryable<Calendario> ListarCalendarios()
        {
            Contexto db = new Contexto();

            IQueryable<Calendario> calendarios = db.Calendarios.OrderBy(c => c.Ano);

            return calendarios;
        }

        public Calendario Criar(Calendario calendario)
        {
            db.Calendarios.Add(calendario);
            db.SaveChanges();

            return calendario;
        }

        public Calendario Atualizar(Calendario calendario)
        {
            db.Entry(calendario).State = EntityState.Modified;
            db.SaveChanges();

            return calendario;
        }

        /// <summary>
        /// Verifica se já existe um calendário com o ano passado por parâmetro
        /// </summary>
        /// <param name="calendario"></param>
        /// <param name="tipoOperacao"></param>
        /// <returns></returns>
        public static bool VericarCalendarioExistente(Calendario calendario, TipoOperacao tipoOperacao)
        {
            Contexto db = new Contexto();

            List<Calendario> calendarios = (from c in db.Calendarios
                                            where c.Ano.Equals(calendario.Ano)
                                            select c).ToList();

            if (calendario.Ano > 0)
            {
                if (tipoOperacao.Equals(TipoOperacao.Create))
                {
                    if (calendarios.Count > 0)
                        return true;
                }
                else if (tipoOperacao.Equals(TipoOperacao.Update))
                {
                    if (calendarios.Count > 0)
                    {
                        foreach (Calendario c in calendarios)
                        {
                            if (c.Ano.Equals(calendario.Ano) && c.ID != calendario.ID)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}