using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public class IntegranteJogo
    {
        public int ID { get; set; }

        [ForeignKey("Integrante")]
        public virtual int IntegranteID { get; set; }

        public virtual Integrante Integrante { get; set; }

        [ForeignKey("Jogo")]
        public virtual int JogoID { get; set; }

        public virtual Jogo Jogo { get; set; }

        public int? Gols { get; set; }

        [Display(Name = "Assistências")]
        public int? Assistencias { get; set; }
    }
}