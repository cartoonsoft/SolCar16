using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Interfaces.Repositories;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;
using Oracle.ManagedDataAccess.Client;

namespace Infra.Data.Car16.Repositories.DbCar16New
{
    public class RepositoryAto : RepositoryBaseReadWrite<Ato>, IRepositoryAto
    {
        private readonly ContextMainCar16New _contextRepository;

        public RepositoryAto(ContextMainCar16New contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }
        /// <summary>
        /// Função que retorna se o ato está cadastrado no nosso banco
        /// Serve para controlar se criara novo arquivo ou abrira e continuará a escrever no qual que existe
        /// </summary>
        /// <param name="numMatricula">Número da Matricula</param>
        /// <returns>Se o ato existe no banco (true or false) </returns>
        public bool ExisteAtoCadastrado(long numMatricula)
        {
            long quantidadeDeAtos = 0;
            string strQuery =
                @"SELECT 
                    COUNT(A.ID_ATO) AS QUANTIDADE
                FROM TB_ATO A
                    WHERE A.NRO_MATRICULA = :NRO_MATRICULA
                    AND A.ATIVO = 1";

            using (OracleConnection conn = new OracleConnection(this.Context.Database.Connection.ConnectionString))
            {
                conn.Open();
                using (OracleCommand command = new OracleCommand(strQuery, conn))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter("NRO_MATRICULA", OracleDbType.Long, numMatricula, System.Data.ParameterDirection.Input));

                    using (OracleDataReader row = command.ExecuteReader())
                    {
                        while (row.Read())
                        {
                            quantidadeDeAtos = row.GetOracleDecimal(row.GetOrdinal("QUANTIDADE")).ToInt64();
                        }
                    }
                }
                conn.Close();
            }
            return quantidadeDeAtos > 0;
        }

        public long? GetNumSequenciaAto(long numeroMatricula)
        {
            long? NumSequenciaAto = null;
            string strQuery =
                @"SELECT 
                    MAX(A.NUM_SEQ) AS NUM_SEQ_MAX
                FROM TB_ATO A
                    WHERE A.NRO_MATRICULA = :NRO_MATRICULA
                    AND ID_TP_ATO != 3
                    AND A.ATIVO = 1";

            using (OracleConnection conn = new OracleConnection(this.Context.Database.Connection.ConnectionString))
            {
                conn.Open();
                using (OracleCommand command = new OracleCommand(strQuery, conn))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.BindByName = true;
                    command.Parameters.Add(new OracleParameter("NRO_MATRICULA", OracleDbType.Long, numeroMatricula, System.Data.ParameterDirection.Input));

                    using (OracleDataReader row = command.ExecuteReader())
                    {
                        while (row.Read())
                        {
                            NumSequenciaAto = row.IsDBNull(row.GetOrdinal("NUM_SEQ_MAX")) ? default(long?) : row.GetOracleDecimal(row.GetOrdinal("NUM_SEQ_MAX")).ToInt64();
                        }
                    }
                }
                conn.Close();
            }
            return NumSequenciaAto;
        }
    }
}
