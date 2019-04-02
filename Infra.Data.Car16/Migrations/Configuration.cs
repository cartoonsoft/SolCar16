using Infra.Data.Car16.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Car16.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<ContextMainCar16>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContextMainCar16 context)
        {

        }
    }
}
