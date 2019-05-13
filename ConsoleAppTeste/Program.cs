using System;
using System.Collections.Generic;
using System.Linq;
using AppServices.Car16.AppServices;
using Domain.Car16.Entities.Car16;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.enums;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.DbCar16New;
using Infra.Data.Car16.UnitsOfWork.DbCar16New;

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
                Console.WriteLine("[ 2 ] Lista Matriculas");
                Console.WriteLine("[ 3 ] NovoPais ");
                Console.WriteLine("[ 4 ] Teste ");
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
                        ListaMatriculas();

                        break;
                    case 3:
                        //cancelarAluno(ref codigoAluno, ref nomeAluno);
                        NovoPais();
                        break;
                    case 4:
                        //cancelarAluno(ref codigoAluno, ref nomeAluno);
                        Teste();
                        break;
                    case 5:
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
            //Console.WriteLine("----------------------------------------------------------------");
            //Console.WriteLine("***************|       RELATORIO     |**************************");
            //Console.WriteLine("----------------------------------------------------------------");

            using (UnitOfWorkDataBaseCar16New unitOfWork = new UnitOfWorkDataBaseCar16New(BaseDados.DesenvDezesseisNew))
            {
                using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(unitOfWork))
                {
                    //
                }
                //unitOfWork.SaveChanges();
            }

            Console.ReadKey();
        }

        public static void ListaMatriculas()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("***************|       RELATORIO     |**************************");
            Console.WriteLine("----------------------------------------------------------------");

            using (UnitOfWorkDataBaseCar16New unitOfWork = new UnitOfWorkDataBaseCar16New(BaseDados.DesenvDezesseis))
            {
                List<Matricula> listMatriculas = unitOfWork.Repositories.GenericRepository<Matricula>().GetAll().ToList();

                foreach (var matricula in listMatriculas)
                {
                    Console.WriteLine("  {0}  {1}  {2}  {3}  ", matricula.NUMERO, matricula.HORAENTRADA, matricula.RESPONSAVEL, matricula.MOTIVO);
                }
                Console.WriteLine("----------------------------fim relatório----------------------");
            }

            Console.ReadKey();
        }

        public static void NovoPais()
        {
            using (UnitOfWorkDataBaseCar16New unitOfWork = new UnitOfWorkDataBaseCar16New(BaseDados.DesenvDezesseisNew))
            {
                //Pais pais = new Pais();
                //pais.NomePais = "Teste id pelo banco realizado em :" + DateTime.Now.ToString();
                //pais.SiglaPais = "TES";
                //pais.CodIbge = "1234";


                Pais pais2 = new Pais();
                pais2.NomePais = "Teste forneci o id realizado em :" + DateTime.Now.ToString();
                pais2.SiglaPais = "TES2";
                pais2.CodIbge = "00111";

                unitOfWork.Repositories.GenericRepository<Pais>().Add(pais2);
                Console.WriteLine(pais2.Id);
                //unitOfWork.Repositories.GenericRepository<Pais>().Add(pais2);
                var resultado = unitOfWork.SaveChanges();

                Console.WriteLine(resultado);
            }

            Console.ReadKey();
        }

        public static void Teste()
        {

            ContextMainCar16New context = new ContextMainCar16New("contextOraDevCartorioNew");

            RepositoryPais RepPais = new RepositoryPais(context);
            List<Pais> listPaizes = RepPais.GetAll().ToList();


            ContextMainCar16 context2 = new ContextMainCar16("contextOraDevCartorio");

            //Repository Pais RepPais = new RepositoryPais(context);

            //using (UnitOfWorkCar16 unitOfWork = new UnitOfWorkCar16(BaseDados.DesenvDezesseisNew))
            //{
            //    List<Pais> listPaizes = unitOfWork.Repositories.GenericRepository<Pais>().GetAll().ToList();

            //        //foreach (var pais in listPaizes)
            //        //{
            //        //    Console.WriteLine("        {0}           {1}", pais.Id, pais.NomePais);
            //        //}
            //        //Console.WriteLine("----------------------------fim relatório----------------------");
            //}

        }

    }
}
