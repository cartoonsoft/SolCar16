using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.AppServices;
using Domain.Car16.enums;
using Dto.Car16.Entities.Cadastros;
using Infra.Data.Car16.UnitsOfWork;
using Infra.Data.Car16.UnitsOfWork.DbCar16New;

namespace ConsoleAppTeste2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnitOfWorkDataBaseCar16New unitOfWork = new UnitOfWorkDataBaseCar16New(BaseDados.DesenvDezesseisNew))
            {
                using (AppServicePais appService = new AppServicePais(unitOfWork))
                {
                    List<DtoPaisModel> listPaizes = appService.GetAll().ToList<DtoPaisModel>();

                    foreach (var pais in listPaizes)
                    {
                        Console.WriteLine("        {0}           {1}", pais.Id, pais.NomePais);
                    }
                    Console.WriteLine("----------------------------fim relatório----------------------");
                }
            }

            Console.ReadKey();

        }
    }
}
