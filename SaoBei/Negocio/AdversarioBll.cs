using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using SaoBei.Models;
using System.Data.Entity;

namespace SaoBei.Negocio
{
    public class AdversarioBll
    {
        Contexto db;

        public AdversarioBll()
        {
            db = new Contexto();
        }

        public IPagedList<Adversario> BuscarAdversarios(int? page, string filtro,
                                            string sortOrder, string ativoFiltro, int pageSize)
        {
            int ativo = int.TryParse(ativoFiltro, out ativo) ? ativo : 2;

            var adversarios = from a in db.Adversarios
                              select a;

            if (!String.IsNullOrEmpty(filtro))
            {
                adversarios = adversarios.Where(a => a.Nome.Contains(filtro));
            }

            if (ativo < 2)
            {
                bool situacao = ativo.Equals(0) ? false : true;
                adversarios = adversarios.Where(x => x.Ativo == situacao);
            }

            switch (sortOrder)
            {
                case "nome_desc":
                    adversarios = adversarios.OrderByDescending(s => s.Nome);
                    break;
                default:
                    adversarios = adversarios.OrderBy(s => s.Nome);
                    break;
            }

            int pageNumber = (page ?? 1);

            return adversarios.ToPagedList(pageNumber, pageSize);
        }

        public Adversario Criar(Adversario adversario)
        {
            db.Adversarios.Add(adversario);
            db.SaveChanges();

            return adversario;
        }

        public Adversario Atualizar(Adversario adversario)
        {
            db.Entry(adversario).State = EntityState.Modified;
            db.SaveChanges();

            return adversario;
        }

        public static Adversario RetornarAdversario(int? id)
        {
            Contexto db = new Contexto();

            Adversario adversario = db.Adversarios.Where(a => a.ID == id).FirstOrDefault();

            return adversario;
        }

        public static IQueryable<Adversario> RetornarAdversariosAtivos()
        {
            Contexto db = new Contexto();

            IQueryable<Adversario> adversarios = db.Adversarios.Where(a => a.Ativo == true);

            return adversarios;
        }
    }
}