using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SaoBei.Models;
using System.Data.Entity;
using Chart.Mvc.ComplexChart;

namespace SaoBei.Negocio
{
    public class MensalidadeIntegranteBll
    {
        Contexto db;

        public MensalidadeIntegranteBll()
        {
            db = new Contexto();
        }

        /// <summary>
        /// Retorna todas as mensalidades
        /// </summary>
        /// <returns></returns>
        public IQueryable<MensalidadeIntegrante> RetornarMensalidadesTodosIntegrante()
        {
            IQueryable<MensalidadeIntegrante> mensalidades = db.MensalidadesIntegrante;

            return mensalidades;
        }

        /// <summary>
        /// Retornar mensalidades do integrante
        /// </summary>
        /// <param name="integranteID"></param>
        /// <returns></returns>
        public IQueryable<MensalidadeIntegrante> RetornarMensalidadeIntegrante(int integranteID)
        {
            IQueryable<MensalidadeIntegrante> mensalidades = db.MensalidadesIntegrante.Where(x => x.IntegranteID == integranteID);

            return mensalidades;
        }

        public static IQueryable<MensalidadeIntegrante> RetornarMensalidadesCalendario(int? calendarioID)
        {
            Contexto db = new Contexto();

            IQueryable<MensalidadeIntegrante> mensalidades = db.MensalidadesIntegrante.Where(m => m.CalendarioID == calendarioID);

            return mensalidades;
        }

        /// <summary>
        /// Retornar mensalidades do integrante 
        /// </summary>
        /// <param name="integranteID">Integrante</param>
        /// <param name="calendarioID">Calendario passado por parametro</param>
        /// <returns></returns>
        public IQueryable<MensalidadeIntegrante> RetornarMensalidadeIntegranteCalendario(int integranteID, int calendarioID, int mes)
        {
            IQueryable<MensalidadeIntegrante> mensalidades = db.MensalidadesIntegrante.Where(x => x.IntegranteID == integranteID && x.Mes == mes && x.CalendarioID == calendarioID);

            return mensalidades;
        }

        /// <summary>
        /// Insere uma nova mensalidade no banco
        /// </summary>
        /// <param name="mensalidades"></param>
        /// <returns></returns>
        public MensalidadeIntegrante Criar(MensalidadeIntegrante mensalidade)
        {
            db.MensalidadesIntegrante.Add(mensalidade);
            db.SaveChanges();

            return mensalidade;
        }

        /// <summary>
        /// Insere as mensalidades do ano no banco
        /// </summary>
        /// <param name="mensalidades"></param>
        /// <returns></returns>
        public IEnumerable<MensalidadeIntegrante> CriarMensalidadesTodosIntegrantesCalendario(Calendario calendario)
        {
            IList<MensalidadeIntegrante> mensalidades = new List<MensalidadeIntegrante>();
            IQueryable<Integrante> integrantes = IntegranteBll.RetornarIntegrantesAtivos();

            //Percorre todos integrantes ativos
            foreach (Integrante integrante in integrantes)
            {
                int mesesAno = 12;

                //
                for (int i = 1; i <= mesesAno; i++)
                {
                    if (!VerificarExisteMensalidadeIntegranteCalendario(integrante.ID, calendario.ID, i))
                    {

                        MensalidadeIntegrante mensalidade = new MensalidadeIntegrante();

                        mensalidade.Mes = i;
                        mensalidade.IntegranteID = integrante.ID;
                        mensalidade.CalendarioID = calendario.ID;
                        mensalidades.Add(mensalidade);
                    }
                }
            }

            db.MensalidadesIntegrante.AddRange(mensalidades);
            db.SaveChanges();

            return mensalidades;
        }

        /// <summary>
        /// Atualiza uma mensalidade existente
        /// </summary>
        /// <param name="mensalidades"></param>
        /// <returns></returns>
        public MensalidadeIntegrante Atualizar(MensalidadeIntegrante mensalidades)
        {
            db.Entry(mensalidades).State = EntityState.Modified;
            db.SaveChanges();

            return mensalidades;
        }
        

        /// <summary>
        /// Verifica se existe mensalidades para o integrante no calendário recebido
        /// </summary>
        /// <param name="integranteID"></param>
        /// <param name="calendarioID"></param>
        /// <returns></returns>
        public bool VerificarExisteMensalidadeIntegranteCalendario(int integranteID, int calendarioID, int mes)
        {
            if (RetornarMensalidadeIntegranteCalendario(integranteID, calendarioID, mes).ToList().Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Criar mensalidades para todos os integrantes que ainda não possuem
        /// </summary>
        /// <param name="calendarioID"></param>
        public void CriarMensalidadesIntegrante(Integrante integrante)
        {
            IList<MensalidadeIntegrante> mensalidades = new List<MensalidadeIntegrante>();
            IQueryable<Calendario> calendarios = CalendarioBll.ListarCalendarios();
            MensalidadeIntegranteBll mensalidadesBll = new MensalidadeIntegranteBll();

            foreach (Calendario calendario in calendarios)
            {
                int mesesAno = 12;

                for (int i = 1; i <= mesesAno; i++)
                {
                    MensalidadeIntegrante mensalidade = new MensalidadeIntegrante();

                    mensalidade.Mes = i;
                    mensalidade.IntegranteID = integrante.ID;
                    mensalidade.CalendarioID = calendario.ID;
                    mensalidades.Add(mensalidade);
                }
            }

            db.MensalidadesIntegrante.AddRange(mensalidades);
            db.SaveChanges();
        }

        public static IQueryable<MensalidadeIntegrante> RetornarMensalidadesIntegranteCalendario(int? integranteID, int? calendarioID)
        {
            Contexto db = new Contexto();

            IQueryable<MensalidadeIntegrante> mensalidades = db.MensalidadesIntegrante.Where(x => x.CalendarioID == calendarioID && x.IntegranteID == integranteID);

            return mensalidades;
        }

        public static MensalidadeIntegrante RetornarMensalidadeIntegranteCalendario(int? mensalidadeID)
        {
            Contexto db = new Contexto();

            MensalidadeIntegrante mensalidade = db.MensalidadesIntegrante.FirstOrDefault(x => x.ID == mensalidadeID);

            return mensalidade;
        }

        public static IList<MensalidadeIntegrante> RetornarMensalidadesSeremBaixadas(int? integranteID, int? calendarioID)
        {
            Contexto db = new Contexto();

            IQueryable<MensalidadeIntegrante> mensalidades = db.MensalidadesIntegrante.Where(x => x.CalendarioID == calendarioID && x.IntegranteID == integranteID);
            Calendario calendario = db.Calendarios.FirstOrDefault(c => c.ID == calendarioID);

            IList<MensalidadeIntegrante> mensalidadesSeremBaixadas = RetornarMesesAtivos(calendario, mensalidades);

            return mensalidadesSeremBaixadas;
        }

        public static IList<MensalidadeIntegrante> RetornarMesesAtivos(Calendario calendario, IQueryable<MensalidadeIntegrante> mensalidades)
        {
            IList<MensalidadeIntegrante> mensalidadesSeremBaixadas = new List<MensalidadeIntegrante>();

            if (calendario.Janeiro)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 1));
            }

            if(calendario.Fevereiro)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 2));
            }

            if (calendario.Marco)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 3));
            }

            if (calendario.Abril)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 4));
            }

            if (calendario.Maio)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 5));
            }

            if (calendario.Junho)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 6));
            }

            if (calendario.Julho)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 7));
            }

            if (calendario.Agosto)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 8));
            }

            if (calendario.Setembro)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 9));
            }

            if (calendario.Outubro)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 10));
            }

            if (calendario.Novembro)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 11));
            }

            if (calendario.Dezembro)
            {
                mensalidadesSeremBaixadas.Add(mensalidades.FirstOrDefault(m => m.Mes == 12));
            }

            return mensalidadesSeremBaixadas;
        }

        public static int RetornarNumeroPagosMes(int ano, int mes)
        {
            Contexto db = new Contexto();

            IQueryable<MensalidadeIntegrante> mensalidades = (from m in db.MensalidadesIntegrante
                                                     where m.Integrante.Ativo
                                                     select m);



            return 0;
        }

        public static void GraficoMensalidadeIntegrante(int ano, out IEnumerable<string> labels, out IEnumerable<ComplexDataset> dataset)
        {
            List<string> meses = new List<string>();
            List<double> dados = new List<double>();

            Calendario calendario = CalendarioBll.RetornarCalendario(ano);

            if (calendario != null)
            {
                IQueryable<MensalidadeIntegrante> mensalidades = RetornarMensalidadesCalendario(calendario.ID);

                if (mensalidades.Count() > 0)
                {
                    if (calendario.Janeiro)
                    {
                        meses.Add("Janeiro");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 1).Count());
                    }

                    if (calendario.Fevereiro)
                    {
                        meses.Add("Fevereiro");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 2).Count());
                    }

                    if (calendario.Marco)
                    {
                        meses.Add("Março");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 3).Count());
                    }

                    if (calendario.Abril)
                    {
                        meses.Add("Abril");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 4).Count());
                    }

                    if (calendario.Maio)
                    {
                        meses.Add("Maio");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 5).Count());
                    }

                    if (calendario.Junho)
                    {
                        meses.Add("Junho");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 6).Count());
                    }

                    if (calendario.Julho)
                    {
                        meses.Add("Julho");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 7).Count());
                    }

                    if (calendario.Agosto)
                    {
                        meses.Add("Agosto");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 8).Count());
                    }

                    if (calendario.Setembro)
                    {
                        meses.Add("Setembro");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 9).Count());
                    }

                    if (calendario.Outubro)
                    {
                        meses.Add("Outubro");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 10).Count());
                    }

                    if (calendario.Novembro)
                    {
                        meses.Add("Novembro");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 11).Count());
                    }

                    if (calendario.Dezembro)
                    {
                        meses.Add("Dezembro");
                        dados.Add(mensalidades.Where(m => m.DataPagamento.Value.Month == 12).Count());
                    }
                }
            }

            if (dados.Count == 0 && meses.Count == 0)
            {
                meses.Add("Janeiro");
                meses.Add("Fevereiro");
                meses.Add("Março");
                meses.Add("Abril");
                meses.Add("Maio");
                meses.Add("Junho");
                meses.Add("Julho");
                meses.Add("Agosto");
                meses.Add("Setembro");
                meses.Add("Outubro");
                meses.Add("Novembro");
                meses.Add("Dezembro");
                dados.Add(0);
                dados.Add(0);
                dados.Add(0);
                dados.Add(0);
                dados.Add(0);
                dados.Add(0);
                dados.Add(0);
                dados.Add(0);
            }


            List<ComplexDataset> complexDataSets = new List<ComplexDataset>();

            complexDataSets.Add(new ComplexDataset
            {
                Data = dados,
                Label = "My First dataset",
                FillColor = "rgba(12, 29, 86, 0.7)",
                StrokeColor = "yellow",
                PointColor = "white",
                PointStrokeColor = "black",
                PointHighlightFill = "black",
                PointHighlightStroke = "rgba(220,220,220,1)"
            });

            labels = meses;
            dataset = complexDataSets;
        }
    }
}