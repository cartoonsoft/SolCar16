using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infra.Data.CartNew.Context;

namespace Infra.Data.Cartorio.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ContextMainCartNew>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContextMainCartNew context)
        {

        }
    }
}
