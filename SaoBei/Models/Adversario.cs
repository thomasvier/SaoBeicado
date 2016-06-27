using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public class Adversario
    {
        public Adversario()
        {
            Ativo = true;
        }

        public int ID { get; set; }

        [Required]
        public string Nome { get; set; }

        [Display(Name = "Nome do contato")]
        public string NomeContato { get; set; }

        public string Telefone { get; set; }

        public bool Ativo { get; set; }
    }
}