using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace SaoBei.Models
{
    public class Contexto : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Contexto() : base("name=Contexto")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }

        public System.Data.Entity.DbSet<SaoBei.Models.Calendario> Calendarios { get; set; }
        public System.Data.Entity.DbSet<SaoBei.Models.Integrante> Integrantes { get; set; }
        public System.Data.Entity.DbSet<SaoBei.Models.Log> Logs { get; set; }
        public System.Data.Entity.DbSet<SaoBei.Models.Jogo> Jogos { get; set; }
        public System.Data.Entity.DbSet<SaoBei.Models.Adversario> Adversarios { get; set; }
        public System.Data.Entity.DbSet<SaoBei.Models.LocalJogo> LocaisJogo { get; set; }
        public System.Data.Entity.DbSet<SaoBei.Models.MensalidadeIntegrante> MensalidadesIntegrante { get; set; }
        public System.Data.Entity.DbSet<SaoBei.Models.IntegranteJogo> IntegrantesJogo { get; set; }
    }
}
