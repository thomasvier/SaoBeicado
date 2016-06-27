using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public class LocalJogo
    {
        public LocalJogo()
        {
            Ativo = true;
        }

        public int ID { get; set; }

        [StringLength(300)]
        public string Nome { get; set; }

        [Display(Name = "Valor jogo")]
        public decimal? ValorJogo { get; set; }

        public bool Ativo { get; set; }
    }
}