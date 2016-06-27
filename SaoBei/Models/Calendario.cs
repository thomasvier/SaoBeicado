using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public class Calendario
    {
        public Calendario()
        {
            Ano = DateTime.Now.Year;
            DataVencimentoAnuidade = DateTime.Now.Date;
            Marco = true;
            Abril = true;
            Maio = true;
            Junho = true;
            Julho = true;
            Agosto = true;
            Setembro = true;
            Outubro = true;
            Novembro = true;
            Dezembro = true;
        }

        public int ID { get; set; }

        [Required]
        public int Ano { get; set; }

        [Display(Name = "Data de vencimento da anuidade")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataVencimentoAnuidade { get; set; }

        public bool Janeiro { get; set; }

        public bool Fevereiro { get; set; }

        [Display(Name="Março")]
        public bool Marco { get; set; }

        public bool Abril { get; set; }

        public bool Maio { get; set; }

        public bool Junho { get; set; }

        public bool Julho { get; set; }

        public bool Agosto { get; set; }

        public bool Setembro { get; set; }

        public bool Outubro { get; set; }

        public bool Novembro { get; set; }

        public bool Dezembro { get; set; }

        [Display(Name="Valor mensalidade")]
        public decimal ValorMensalidade { get; set; }

        [Display(Name = "Valor anuidade")]
        public decimal ValorAnuidade { get; set; }
    }
}