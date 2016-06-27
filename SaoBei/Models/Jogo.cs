using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public class Jogo
    {
        public Jogo()
        {
            SituacaoJogo = SituacaoJogo.Confirmado;
            Data = DateTime.Today;                                                
        }

        public int ID { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]        
        public DateTime Data { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Time)]
        public DateTime Hora { get; set; }

        [ForeignKey("Adversario")]        
        public virtual int AdversarioID { get; set; }

        public virtual Adversario Adversario { get; set; }

        [ForeignKey("Calendario")]
        public virtual int CalendarioID { get; set; }

        public virtual Calendario Calendario { get; set; }

        [Display(Name = "Situação do jogo")]
        public SituacaoJogo SituacaoJogo { get; set; }

        [Display(Name = "Motivo do cancelamento")]
        public string MotivoCancelamento { get; set; }

        [ForeignKey("LocalJogo")]
        public virtual int LocalJogoID { get; set; }

        public virtual LocalJogo LocalJogo { get; set; }

        [ForeignKey("Destaque")]
        [Display(Name = "Destaque")]
        public virtual int DestaqueID { get; set; }

        public virtual Integrante Destaque { get; set; }

        public virtual IEnumerable<IntegranteJogo> IntegrantesJogo { get; set; }

        public int GolsSaoBei { get; set; }

        public int GolsAdversario { get; set; }

        public TipoResultado Resultado { get; set; }
    }
}