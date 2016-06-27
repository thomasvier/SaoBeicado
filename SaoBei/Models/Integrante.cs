using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public class Integrante
    {
        public Integrante()
        {
            Ativo = true;
            TipoIntegrante = TipoIntegrante.Integrante;
            DataNascimento = DateTime.Today.AddYears(-18);
        }

        [DisplayFormat(DataFormatString = "{0:000000}", ApplyFormatInEditMode = true)]
        public int ID { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Telefone { get; set; }

        [Display(Name = "Data de nascimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DataNascimento { get; set; }

        [DataType(DataType.EmailAddress)]
        [Display(Name="E-mail")]
        public string Email { get; set; }

        [Display(Name = "Tipo de integrante")]
        public TipoIntegrante TipoIntegrante { get; set; }

        [DataType(DataType.Password)]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "O campo Senha deve ter entre 6 a 20 caracterers")]
        public string Senha { get; set; }

        public virtual List<MensalidadeIntegrante> Mensalidades { get; set; }

        public bool Ativo { get; set; }
    }
}