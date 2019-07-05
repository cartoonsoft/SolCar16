using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Cartorio.AppServices;
using Domain.Cartorio.enums;
using Dto.Cartorio.Entities.Cadastros;
using Infra.Data.Cartorio.UnitsOfWork;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorio;
using Infra.Data.Cartorio.UnitsOfWork.DbCartorioNew;

namespace ConsoleAppTeste2
{
    class Program
    {
        static void Main(string[] args)
        {
            using (UnitOfWorkDataBaseCartorio unitOfWork1 = new UnitOfWorkDataBaseCartorio(BaseDados.DesenvDezesseis))
            {
                using (UnitOfWorkDataBaseCartorioNew unitOfWork2 = new UnitOfWorkDataBaseCartorioNew(BaseDados.DesenvDezesseisNew))
                {
                    using (AppServicePais appService = new AppServicePais(unitOfWork1, unitOfWork2))
                    {
                        List<DtoPaisModel> listPaizes = appService.GetAll().ToList<DtoPaisModel>();

                        foreach (var pais in listPaizes)
                        {
                            Console.WriteLine("        {0}           {1}", pais.Id, pais.NomePais);
                        }
                        Console.WriteLine("----------------------------fim relatório----------------------");
                    }
                }
            }

            Console.ReadKey();

        }
    }
}
