using Infra.Data.Cartorio.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Cartorio.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ContextMainCartorioNew>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContextMainCartorioNew context)
        {

        }
    }
}
