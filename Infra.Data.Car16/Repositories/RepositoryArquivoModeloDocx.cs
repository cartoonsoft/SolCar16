using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Car16.Entities.Car16New;
using Domain.Car16.Entities.Diversas;
using Domain.Car16.Interfaces.Repositories;
using Infra.Data.Car16.Context;
using Infra.Data.Car16.Repositories.Base;
using Oracle.ManagedDataAccess.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace Infra.Data.Car16.Repositories
{
    public class RepositoryArquivoModeloDocx : RepositoryBaseRead<ArquivoModeloDocx>, IRepositoryArquivoModeloDocx
    {
        private readonly ContextMainCar16 _contexRep;

        public RepositoryArquivoModeloDocx(ContextMainCar16 contexRep) : base(contexRep)
        {
            _contexRep = contexRep;
        }

        public IEnumerable<ArquivoModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null)
        {
            string strQuery = "";
            string sqlWhere = "";
            string sqlOrder = "";

            List<ArquivoModeloDocxList> ListaAquivoModeloDocxList = new List<ArquivoModeloDocxList>();
            List<OracleParameter> oracleParameters = new List<OracleParameter>();

            strQuery +=
                @"select
                    DOC.ID_MODELO_DOC,
                    DOC.ID_TP_ATO,
                    DOC.ID_CTA_ACESSO_SIST,
                    DOC.ID_USR_CAD,
                    DOC.ID_USR_ALTER,
                    DOC.DT_CAD,
                    DOC.DT_ALTER,
                    DOC.DESCRICAO,
                    DOC.ARQUIVO,
                    DOC.ATIVO,
                    ATO.DESCRICAO as DESC_ATO
                from
                    TB_MODELO_DOC DOC
                    left outer join TB_TP_ATO ATO on ATO.ID_TP_ATO = DOC.ID_TP_ATO";

            if (IdTipoAto != null)
            {
                oracleParameters.Add(new OracleParameter("ID_TP_ATO", OracleDbType.Long, IdTipoAto, System.Data.ParameterDirection.Input));
                sqlWhere += this.AddWhereClause("(DOC.ID_TP_ATO = :ID_TP_ATO)", (sqlWhere == ""));
            }

            sqlOrder = "order by DOC.DESCRICAO asc";

            strQuery =
                strQuery + System.Environment.NewLine +
                sqlWhere + System.Environment.NewLine +
                sqlOrder;

            using (OracleConnection conn = new OracleConnection(this.Context.Database.Connection.ConnectionString))
            {
                conn.Open();
                using (OracleCommand command = new OracleCommand(strQuery, conn))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.BindByName = true;

                    foreach (var parametro in oracleParameters)
                    {
                        command.Parameters.Add(parametro);
                    }

                    using (OracleDataReader row = command.ExecuteReader())
                    {
                        while (row.Read())
                        {
                            ListaAquivoModeloDocxList.Add(new ArquivoModeloDocxList
                            {
                                Id = row.GetOracleDecimal(row.GetOrdinal("ID_MODELO_DOC")).ToInt64(), // DOC.ID_MODELO_DOC
                                IdTipoAto = row.IsDBNull(row.GetOrdinal("ID_TP_ATO")) ? default(long?) : row.GetOracleDecimal(row.GetOrdinal("ID_TP_ATO")).ToInt64(), //DOC.ID_TP_ATO,
                                IdContaAcessoSistema = row.GetOracleDecimal(row.GetOrdinal("ID_CTA_ACESSO_SIST")).ToInt64(), // DOC.ID_CTA_ACESSO_SIST,
                                IdUsuarioCadastro = row.GetOracleDecimal(row.GetOrdinal("ID_USR_CAD")).ToInt64(), //DOC.ID_USR_CAD,
                                IdUsuarioAlteracao = row.IsDBNull(row.GetOrdinal("ID_USR_ALTER")) ? default(long?) : row.GetOracleDecimal(row.GetOrdinal("ID_USR_ALTER")).ToInt64(),  //DOC.ID_USR_ALTER
                                DataCadastro = row.GetOracleDate(row.GetOrdinal("DT_CAD")).Value, //DOC.DT_CAD,
                                DataAlteracao = (DateTime?)(row.IsDBNull(row.GetOrdinal("DT_ALTER")) ? null : row.GetValue(row.GetOrdinal("DT_ALTER"))), //DT_ALTER
                                NomeModelo = row.GetOracleString(row.GetOrdinal("DESCRICAO")).ToString(), //DOC.DESCRICAO,
                                CaminhoEArquivo = row.GetOracleString(row.GetOrdinal("ARQUIVO")).ToString(), //DOC.ARQUIVO,
                                Ativo = !row.GetOracleDecimal(row.GetOrdinal("ATIVO")).IsZero, //DOC.ATIVO,
                                DescricaoAto = row.GetOracleString(row.GetOrdinal("DESC_ATO")).ToString()  //DESC_ATO
                            });
                        }
                    }
                }
                conn.Close();
            };

            //List<ArquivoModeloDocxList> aquivoModeloDocxList = this.Context.Database.SqlQuery<ArquivoModeloDocxList>(strQuery).ToList<ArquivoModeloDocxList>();

            return ListaAquivoModeloDocxList;
        }
        /// <summary>
        /// Lista de modelos simplificados (DOC.ID_MODELO_DOC,DOC.DESCRICAO,ATO.DESCRICAO AS DESC_ATO)
        /// </summary>
        /// <param name="IdTipoAto">Id do tipo ato para filtrar </param>
        /// <returns></returns>
        public IEnumerable<ArquivoModeloSimplificadoDocxList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null)
        {
            string strQuery = "";
            string sqlWhere = "";
            string sqlOrder = "";

            List<ArquivoModeloSimplificadoDocxList> ListaArquivos = new List<ArquivoModeloSimplificadoDocxList>();
            List<OracleParameter> oracleParameters = new List<OracleParameter>();


            strQuery +=
                @"SELECT 
                    DOC.ID_MODELO_DOC,
                    DOC.DESCRICAO,
                    ATO.DESCRICAO AS DESC_ATO
                FROM TB_MODELO_DOC DOC
                    INNER JOIN TB_TP_ATO ATO ON DOC.ID_TP_ATO = ATO.ID_TP_ATO";

            if (IdTipoAto != null)
            {
                oracleParameters.Add(new OracleParameter("ID_TP_ATO", OracleDbType.Long, IdTipoAto, System.Data.ParameterDirection.Input));
                sqlWhere += this.AddWhereClause("(DOC.ID_TP_ATO = :ID_TP_ATO)", (sqlWhere == ""));
            }

            sqlOrder = "order by DOC.DESCRICAO asc";

            strQuery =
                strQuery + System.Environment.NewLine +
                sqlWhere + System.Environment.NewLine +
                sqlOrder;

            using (OracleConnection conn = new OracleConnection(this.Context.Database.Connection.ConnectionString))
            {
                conn.Open();
                using (OracleCommand command = new OracleCommand(strQuery, conn))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.BindByName = true;

                    foreach (var parametro in oracleParameters)
                    {
                        command.Parameters.Add(parametro);
                    }

                    using (OracleDataReader row = command.ExecuteReader())
                    {
                        while (row.Read())
                        {
                            ListaArquivos.Add(new ArquivoModeloSimplificadoDocxList
                            {
                                Id = row.GetOracleDecimal(row.GetOrdinal("ID_MODELO_DOC")).ToInt64(), // DOC.ID_MODELO_DOC
                                NomeModelo = row.GetOracleString(row.GetOrdinal("DESCRICAO")).ToString(), //DOC.DESCRICAO,
                                DescricaoTipoAto = row.GetOracleString(row.GetOrdinal("DESC_ATO")).ToString() //ATO.DESCRICAO
                            });
                        }
                    }
                }
                conn.Close();
            };

            //List<ArquivoModeloDocxList> aquivoModeloDocxList = this.Context.Database.SqlQuery<ArquivoModeloDocxList>(strQuery).ToList<ArquivoModeloDocxList>();

            return ListaArquivos;
        }
    }
}