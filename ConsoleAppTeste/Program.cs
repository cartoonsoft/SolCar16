using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.AppServices;
using Domain.Car16.Entities;
using Domain.Car16.enums;
using Dto.Car16.Entities.Cadastros;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.UnitOfWorkCar16;

namespace ConsoleAppTeste
{
    class Program
    {
        static void Main(string[] args)
        {

            int opcao;
            do
            {
                Console.WriteLine("[ 1 ] ListaPaizes");
                Console.WriteLine("[ 2 ] Faz insert pais");
                Console.WriteLine("[ 0 ] Sair do Software");
                Console.WriteLine("-------------------------------------");
                Console.Write("Digite uma opção: ");

                opcao = Int32.Parse(Console.ReadLine());
                switch (opcao)
                {
                    case 1:
                        ListaPaizes();

                        break;
                    case 2:
                        //cancelarAluno(ref codigoAluno, ref nomeAluno);
                        NovoPais();
                        break;
                    case 9:
                        break;
                    default:
                        break;
                }
                Console.ReadKey();
                Console.Clear();
            }
            while (opcao != 0);
        }


        public static void ListaPaizes()
        {

            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("***************|       RELATORIO     |**************************");
            Console.WriteLine("----------------------------------------------------------------");

            using (UnitOfWorkCar16 unitOfWork = new UnitOfWorkCar16(BaseDados.DesenvDezesseisNew))
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
            Console.Clear();

        }

        public static void NovoPais()
        {
            using (UnitOfWorkCar16 unitOfWork = new UnitOfWorkCar16(BaseDados.DesenvDezesseisNew))
            {
                using (AppServicePais appService = new AppServicePais(unitOfWork))
                {
                    Pais pais = new Pais();


                    List<DtoPaisModel> listPaizes = appService.GetAll().ToList<DtoPaisModel>();

                    //foreach (var pais in listPaizes)
                    //{
                    //    Console.WriteLine("        {0}           {1}", pais.Id, pais.NomePais);
                    //}
                    //Console.WriteLine("----------------------------fim relatório----------------------");
                }
            }

        }



    }

}
