using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AppServices.Car16.AppServices;
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

            ContextMainCar16 context = new ContextMainCar16("connOraDbNew");
            

            Console.Clear();
            //Console.WriteLine("----------------------------------------------------------------");
            //Console.WriteLine("***************|       RELATORIO     |**************************");
            //Console.WriteLine("----------------------------------------------------------------");

            using (UnitOfWorkCar16 unitOfWork = new UnitOfWorkCar16(context))
            {
                using (AppServiceArquivoModeloDocx appService = new AppServiceArquivoModeloDocx(unitOfWork))
                {

                    appService.SalvarModelo(new DtoArquivoModeloDocxModel()
                    {
                        NomeArquivo = "Pedro"
                    });
                }
                unitOfWork.Commit();
            }

            Console.ReadKey();
            Console.Clear();

        }



    }

}
