using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public enum TipoResultado
    {
        [Description("Derrota")]
        Derrota = 0,
        [Description("Empate")]
        Empate = 1,
        [Description("Vitória")]
        Vitoria = 2
    }
}