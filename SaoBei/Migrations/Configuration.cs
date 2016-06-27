namespace SaoBei.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using SaoBei.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<SaoBei.Models.Contexto>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "SaoBei.Models.Contexto";
        }

        protected override void Seed(SaoBei.Models.Contexto context)
        {
            Integrante integrante = new Integrante {
                Ativo = false,
                DataNascimento = DateTime.Now,
                Email = "adm@saobei.com.br",
                Nome = "Administrador",
                Senha = "admsaobei",
                TipoIntegrante = TipoIntegrante.Diretoria
            };

            context.Integrantes.AddOrUpdate(integrante);
            context.SaveChanges();

            Calendario calendario = new Calendario();            

            context.Calendarios.AddOrUpdate(calendario);
            context.SaveChanges();
        }
    }
}
