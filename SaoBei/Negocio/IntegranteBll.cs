using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using SaoBei.Models;
using System.Data.Entity;

namespace SaoBei.Negocio
{
    public class IntegranteBll
    {
        Contexto db;

        public IntegranteBll()
        {
            db = new Contexto();
        }

        public Integrante LogOn(string email, string senha)
        {
            Integrante integrante = (from i in db.Integrantes
                               where i.Email.Equals(email) && i.Senha.Equals(senha) && (i.Ativo.Equals(true) || i.Nome == "Administrador")
                               select i).FirstOrDefault();


            return integrante;
        }

        /// <summary>
        /// Verifica se já existe um usuário com o email passado por parâmetro
        /// </summary>
        /// <param name="integrante"></param>
        /// <param name="tipoOperacao"></param>
        /// <returns></returns>
        public static bool VericarEmailExistente(Integrante integrante, TipoOperacao tipoOperacao)
        {
            Contexto db = new Contexto();

            List<Integrante> integrantes = (from c in db.Integrantes
                                      where c.Email.Equals(integrante.Email)
                                      select c).ToList();

            if (!string.IsNullOrEmpty(integrante.Email))
            {
                if (tipoOperacao.Equals(TipoOperacao.Create))
                {
                    if (integrantes.Count > 0)
                        return true;
                }
                else if (tipoOperacao.Equals(TipoOperacao.Update))
                {
                    if (integrantes.Count > 0)
                    {
                        foreach (Integrante user in integrantes)
                        {
                            if (user.Email.Equals(integrante.Email) && user.ID != integrante.ID)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        public IPagedList<Integrante> BuscarIntegrantes(int? page, string filtro,
                                            string sortOrder, string ativoFiltro, int pageSize)
        {
            int ativo = int.TryParse(ativoFiltro, out ativo) ? ativo : 2;

            var integrantes = from i in db.Integrantes where i.Nome != "Administrador"
                         select i;

            if (!String.IsNullOrEmpty(filtro))
            {
                integrantes = integrantes.Where(i => i.Nome.Contains(filtro));
            }

            if (ativo < 2)
            {
                bool situacao = ativo.Equals(0) ? false : true;
                integrantes = integrantes.Where(x => x.Ativo == situacao);
            }
            
            switch (sortOrder)
            {
                case "nome_desc":
                    integrantes = integrantes.OrderByDescending(s => s.Nome);
                    break;
                default:
                    integrantes = integrantes.OrderBy(s => s.Nome);
                    break;
            }

            int pageNumber = (page ?? 1);

            return integrantes.ToPagedList(pageNumber, pageSize);
        }

        public Integrante Criar(Integrante integrante)
        {
            db.Integrantes.Add(integrante);
            db.SaveChanges();

            return integrante;
        }

        public Integrante Atualizar(Integrante integrante)
        {
            db.Entry(integrante).State = EntityState.Modified;
            db.SaveChanges();

            return integrante;
        }

        public static Integrante RetornarIntegrante(int? id)
        {
            Contexto db = new Contexto();

            Integrante integrante = db.Integrantes.Where(i => i.ID == id).FirstOrDefault();

            return integrante;
        }

        public static IQueryable<Integrante> RetornarIntegrantesAtivos()
        {
            Contexto db = new Contexto();

            IQueryable <Integrante> integrantes = db.Integrantes.Where(i => i.Ativo == true);

            return integrantes;
        }

        public static Integrante RetornarIntegranteMensalidades(int? integranteID, int? calendarioID)
        {
            Contexto db = new Contexto();

            Integrante integrante = db.Integrantes.Where(i => i.ID == integranteID).FirstOrDefault();

            integrante.Mensalidades = db.MensalidadesIntegrante.Where(m => m.CalendarioID == calendarioID && m.IntegranteID == integranteID).ToList();

            return integrante;
        }

        public static TipoIntegrante RetornarTipoIntegrante(string email)
        {
            Contexto db = new Contexto();

            TipoIntegrante tipoIntegrante = db.Integrantes.Where(i => i.Email == email).FirstOrDefault().TipoIntegrante;

            return tipoIntegrante;
        }
    }
}