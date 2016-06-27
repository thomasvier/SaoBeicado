using System;
using System.ComponentModel.DataAnnotations;

namespace SaoBei.Models
{
    public class Log
    {
        public int ID { get; set; }

        [Required]
        public TipoLog TipoLog { get; set; }

        [Required]
        public string Mensagem { get; set; }

        public string Observacao { get; set; }

        public string Usuario { get; set; }

        [Required]
        public DateTime DataHora { get; set; }
    }
}