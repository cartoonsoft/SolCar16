﻿using System;
using System.Linq;
using System.Collections.Generic;
using Oracle.ManagedDataAccess.Client;
using Domain.CartNew.Entities;
using Domain.CartNew.Interfaces.Repositories;
using Domain.CartNew.Entities.Diversos;
using Infra.Data.Core.Repositories;
using Infra.Data.CartNew.Context;

namespace Infra.Data.CartNew.Repositories.DbCartNew
{
    public class RepositoryModeloDocx : RepositoryBaseReadWrite<ModeloDocx>, IRepositoryModeloDocx
    {
        private readonly ContextMainCartNew _contextRepository;

        public RepositoryModeloDocx(ContextMainCartNew contexRepository) : base(contexRepository)
        {
            _contextRepository = contexRepository;
        }

        public byte[] GetBytesArquivo(long idArquivo)
        {
            byte[] arrayBytes = null;
            string strQuery = @"
                SELECT DOC.ARQ_BYTES
                FROM TB_MODELO_DOC DOC
                    WHERE DOC.ID_MODELO_DOC = :ID_MODELO_DOC
                    AND DOC.ATIVO = 1";
            using (OracleConnection conn = new OracleConnection(this.Context.Database.Connection.ConnectionString))
            {
                conn.Open();
                using (OracleCommand command = new OracleCommand(strQuery, conn))
                {
                    command.CommandType = System.Data.CommandType.Text;
                    command.Parameters.Add(new OracleParameter("ID_MODELO_DOC", OracleDbType.Long, idArquivo, System.Data.ParameterDirection.Input));

                    using (OracleDataReader row = command.ExecuteReader(System.Data.CommandBehavior.Default))
                    {
                        while (row.Read())
                        {
                            arrayBytes = (byte[])row["ARQ_BYTES"];
                        }
                    }
                }
                conn.Close();
            }
            return arrayBytes;
        }

        public IEnumerable<ModeloDocxList> ListarArquivoModeloDocx(long? IdTipoAto = null)
        {
            string strQuery = string.Empty;
            string sqlWhere = string.Empty;
            string sqlOrder = string.Empty;

            List<ModeloDocxList> ListaAquivoModeloDocxList = new List<ModeloDocxList>();
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
                sqlWhere += this.AddWhereClause("(DOC.ID_TP_ATO = :ID_TP_ATO)", "AND", (sqlWhere == string.Empty));
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
                            ListaAquivoModeloDocxList.Add(new ModeloDocxList
                            {
                                Id = row.GetOracleDecimal(row.GetOrdinal("ID_MODELO_DOC")).ToInt64(), // DOC.ID_MODELO_DOC
                                IdTipoAto = row.IsDBNull(row.GetOrdinal("ID_TP_ATO")) ? default(long?) : row.GetOracleDecimal(row.GetOrdinal("ID_TP_ATO")).ToInt64(), //DOC.ID_TP_ATO,
                                IdCtaAcessoSist = row.GetOracleDecimal(row.GetOrdinal("ID_CTA_ACESSO_SIST")).ToInt64(), // DOC.ID_CTA_ACESSO_SIST,
                                IdUsuarioCadastro = row.GetOracleString(row.GetOrdinal("ID_USR_CAD")).ToString(), //DOC.ID_USR_CAD,
                                IdUsuarioAlteracao = row.GetOracleString(row.GetOrdinal("ID_USR_ALTER")).ToString(),  //DOC.ID_USR_ALTER
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
        public IEnumerable<ModeloDocxSimplificadoList> ListarArquivoModeloSimplificadoDocx(long? IdTipoAto = null)
        {
            string strQuery = string.Empty;
            string sqlWhere = string.Empty;
            string sqlOrder = string.Empty;

            List<ModeloDocxSimplificadoList> ListaArquivos = new List<ModeloDocxSimplificadoList>();
            List<OracleParameter> oracleParameters = new List<OracleParameter>();

            strQuery +=
                @"SELECT 
                    DOC.ID_MODELO_DOC,
                    DOC.DESCRICAO,
                    ATO.DESCRICAO AS DESC_ATO
                FROM TB_MODELO_DOC DOC
                    INNER JOIN TB_TP_ATO ATO ON DOC.ID_TP_ATO = ATO.ID_TP_ATO";

            sqlWhere += this.AddWhereClause("(DOC.ATIVO = 1)", "AND", (sqlWhere == string.Empty));

            if (IdTipoAto != null)
            {
                oracleParameters.Add(new OracleParameter("ID_TP_ATO", OracleDbType.Long, IdTipoAto, System.Data.ParameterDirection.Input));
                sqlWhere += this.AddWhereClause("(DOC.ID_TP_ATO = :ID_TP_ATO)", "AND", (sqlWhere == string.Empty));
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
                            ListaArquivos.Add(new ModeloDocxSimplificadoList
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

        public IEnumerable<CamposModeloDocx> GetListaCamposIdTipoAto(long? IdTipoAto)
        {
            List<CamposModeloDocx> listaCamposArquivoModeloDocx = new List<CamposModeloDocx>();
            
            var listaCampos =
                from campos in _contextRepository.DbCamposArquivoModeloDocx.Where(c => c.IdTipoAto == IdTipoAto)
                orderby campos.NomeCampo
                select new
                {
                    campos.Id,
                    campos.IdCtaAcessoSist,
                    campos.IdTipoAto,
                    campos.NomeCampo,
                    campos.PlaceHolder,
                    campos.Entidade,
                    campos.Campo
                };

            foreach (var item in listaCampos)
            {
                listaCamposArquivoModeloDocx.Add(new CamposModeloDocx
                {
                    Id = item.Id,
                    IdCtaAcessoSist = item.IdCtaAcessoSist,
                    IdTipoAto = item.IdTipoAto,
                    NomeCampo = item.NomeCampo,
                    Campo = item.Campo,
                    Entidade = item.Entidade,
                    PlaceHolder = item.PlaceHolder
                });
            }

            return listaCamposArquivoModeloDocx;
        }

    }
}