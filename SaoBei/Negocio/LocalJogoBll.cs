using PagedList;
using SaoBei.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SaoBei.Negocio
{
    public class LocalJogoBll
    {
        Contexto db;

        public LocalJogoBll()
        {
            db = new Contexto();
        }

        public IPagedList<LocalJogo> BuscarLocalJogos(int? page, string filtro,
                                            string sortOrder, string ativoFiltro, int pageSize)
        {
            int ativo = int.TryParse(ativoFiltro, out ativo) ? ativo : 2;

            var localJogos = from l in db.LocaisJogo
                              select l;

            if (!String.IsNullOrEmpty(filtro))
            {
                localJogos = localJogos.Where(a => a.Nome.Contains(filtro));
            }

            if (ativo < 2)
            {
                bool situacao = ativo.Equals(0) ? false : true;
                localJogos = localJogos.Where(x => x.Ativo == situacao);
            }

            switch (sortOrder)
            {
                case "nome_desc":
                    localJogos = localJogos.OrderByDescending(s => s.Nome);
                    break;
                default:
                    localJogos = localJogos.OrderBy(s => s.Nome);
                    break;
            }

            int pageNumber = (page ?? 1);

            return localJogos.ToPagedList(pageNumber, pageSize);
        }

        public LocalJogo Criar(LocalJogo localJogo)
        {
            db.LocaisJogo.Add(localJogo);
            db.SaveChanges();

            return localJogo;
        }

        public LocalJogo Atualizar(LocalJogo localJogo)
        {
            db.Entry(localJogo).State = EntityState.Modified;
            db.SaveChanges();

            return localJogo;
        }

        public static LocalJogo RetornarLocalJogo(int? id)
        {
            Contexto db = new Contexto();

            LocalJogo localJogo = db.LocaisJogo.Where(l => l.ID == id).FirstOrDefault();

            return localJogo;
        }

        public static IQueryable<LocalJogo> RetornarLocaisJogoAtivos()
        {
            Contexto db = new Contexto();

            IQueryable<LocalJogo> localJogos = db.LocaisJogo.Where(l => l.Ativo == true);

            return localJogos;
        }
    }
}