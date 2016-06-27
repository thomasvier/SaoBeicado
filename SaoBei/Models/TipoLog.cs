using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public enum TipoLog
    {
        [Description("Informação")]
        Informacao = 0,
        [Description("Erro")]
        Erro = 1
    }
}