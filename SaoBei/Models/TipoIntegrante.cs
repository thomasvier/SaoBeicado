using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public enum TipoIntegrante
    {
        [Description("Informação")]
        Diretoria = 0,

        [Description("Integrante")]
        Integrante = 1
    }
}