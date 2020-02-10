using System;
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
    public class RepositoryModeloDoc : RepositoryBaseReadWrite<ModeloDoc>, IRepositoryModeloDoc
    {
        private readonly ContextMainCartNew _contextRepository;

        public RepositoryModeloDoc(ContextMainCartNew contexRepository) : base(contexRepository)
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

        public IEnumerable<ModeloDocxList> GetListModelosDocx(long? IdTipoAto = null)
        {
            List<ModeloDocxList> listaModelosDocxList = new List<ModeloDocxList>();

            var listaModelos =
                from M in this._contextRepository.DbModeloDoc.Where(m => (IdTipoAto == null) || (m.IdTipoAto == IdTipoAto))
                join TA in this._contextRepository.DbTipoAto on M.IdTipoAto equals TA.Id into _a
                from TA in _a.DefaultIfEmpty()
                orderby (M.Descricao)
                select new ModeloDocxList
                {
                    Id = M.Id,
                    IdCtaAcessoSist = M.IdCtaAcessoSist,
                    IdTipoAto = M.IdTipoAto,
                    IdTipoAtoPai = TA.IdTipoAtoPai,
                    IdUsuarioCadastro = M.IdUsuarioCadastro,
                    IdUsuarioAlteracao = M.IdUsuarioAlteracao,
                    DataCadastro = M.DataCadastro,
                    DataAlteracao = M.DataAlteracao,
                    Descricao = M.Descricao,
                    Orientacao = M.Orientacao,
                    DescricaoTipo = TA.Descricao,
                    SiglaSeqAto = TA.SiglaSeqAto,
                    Ativo = M.Ativo
                };

            foreach (var modelo in listaModelos)
            {
                listaModelosDocxList.Add(new ModeloDocxList {
                    Id = modelo.Id,
                    IdCtaAcessoSist = modelo.IdCtaAcessoSist,
                    IdTipoAto = modelo.IdTipoAto,
                    IdTipoAtoPai = modelo.IdTipoAtoPai,
                    IdUsuarioCadastro = modelo.IdUsuarioCadastro,
                    IdUsuarioAlteracao = modelo.IdUsuarioAlteracao,
                    DataCadastro = modelo.DataCadastro,
                    DataAlteracao = modelo.DataAlteracao,
                    Descricao  = modelo.Descricao,
                    Orientacao = modelo.Orientacao,
                    CaminhoEArquivo = modelo.CaminhoEArquivo,
                    DescricaoTipo = modelo.DescricaoTipo,
                    SiglaSeqAto = modelo.SiglaSeqAto,
                    Ativo = modelo.Ativo
                });
            }

            return listaModelosDocxList;
        }

        public IEnumerable<ModeloDocxList> GetListModelosDocx2(long? IdTipoAto = null)
        {
            string strQuery = string.Empty;
            string sqlWhere = string.Empty;
            string sqlOrder = string.Empty;

            List<ModeloDocxList> listaModelosDocxList = new List<ModeloDocxList>();
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
                    DOC.TEXTO,
                    DOC.ORIENTACAO,
                    DOC.ATIVO,
                    TA.ID_TP_ATO_PAI,
                    TA.DESCRICAO DESC_TP_ATO,
                    TA.SIGLA_SEQ_ATO
                from
                    TB_MODELO_DOC DOC
                    LEFT OUTER JOIN TB_TP_ATO TA ON  TA.ID_TP_ATO = DOC.ID_TP_ATO";

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
                            listaModelosDocxList.Add(new ModeloDocxList
                            {
                                Id = row.GetOracleDecimal(row.GetOrdinal("ID_MODELO_DOC")).ToInt64(), // DOC.ID_MODELO_DOC
                                IdTipoAto = row.IsDBNull(row.GetOrdinal("ID_TP_ATO")) ? default(long?) : row.GetOracleDecimal(row.GetOrdinal("ID_TP_ATO")).ToInt64(), //DOC.ID_TP_ATO,
                                IdTipoAtoPai = row.IsDBNull(row.GetOrdinal("ID_TP_ATO_PAI")) ? default(long?) : row.GetOracleDecimal(row.GetOrdinal("ID_TP_ATO_PAI")).ToInt64(), //DOC.ID_TP_ATO,
                                IdCtaAcessoSist = row.GetOracleDecimal(row.GetOrdinal("ID_CTA_ACESSO_SIST")).ToInt64(), // DOC.ID_CTA_ACESSO_SIST,
                                IdUsuarioCadastro = row.GetOracleString(row.GetOrdinal("ID_USR_CAD")).ToString(), //DOC.ID_USR_CAD,
                                IdUsuarioAlteracao = row.GetOracleString(row.GetOrdinal("ID_USR_ALTER")).ToString(),  //DOC.ID_USR_ALTER
                                DataCadastro = row.GetOracleDate(row.GetOrdinal("DT_CAD")).Value, //DOC.DT_CAD,
                                DataAlteracao = (DateTime?)(row.IsDBNull(row.GetOrdinal("DT_ALTER")) ? null : row.GetValue(row.GetOrdinal("DT_ALTER"))), //DT_ALTER
                                Descricao = row.GetOracleString(row.GetOrdinal("DESCRICAO")).ToString(), //DOC.DESCRICAO,
                                DescricaoTipo = row.GetOracleString(row.GetOrdinal("DESC_TP_ATO")).ToString(), //TA.DESC_TP_ATO,
                                Orientacao = row.GetOracleString(row.GetOrdinal("ORIENTACAO")).ToString(), // DOC.ORIENTACAO,
                                CaminhoEArquivo = row.GetOracleString(row.GetOrdinal("ARQUIVO")).ToString(), //DOC.ARQUIVO,
                                Ativo = !row.GetOracleDecimal(row.GetOrdinal("ATIVO")).IsZero,  //DOC.ATIVO,
                                SiglaSeqAto = row.GetOracleString(row.GetOrdinal("SIGLA_SEQ_ATO")).ToString()
                            });
                        }
                    }
                }
                conn.Close();
            };

            //List<ArquivoModeloDocxList> aquivoModeloDocxList = this.Context.Database.SqlQuery<ArquivoModeloDocxList>(strQuery).ToList<ArquivoModeloDocxList>();

            return listaModelosDocxList;
        }

        /// <summary>
        /// Lista de modelos simplificados (DOC.ID_MODELO_DOC,DOC.DESCRICAO,ATO.DESCRICAO AS DESC_ATO)
        /// </summary>
        /// <param name="IdTipoAto">Id do tipo ato para filtrar </param>
        /// <returns></returns>
        public IEnumerable<ModeloDocxSimplificadoList> GetListModeloSimplificadoDocx(long? IdTipoAto = null)
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

        public IEnumerable<CampoTipoAto> GetListCamposIdTipoAto(long? IdTipoAto, long IdCtaAcessoSist)
        {
            List<CampoTipoAto> campoTipoAtos = new List<CampoTipoAto>();
            
            var listaCampos =
                from ta in _contextRepository.DbTipoAtoCampo.Where(a => a.IdTipoAto == IdTipoAto)
                join ac in _contextRepository.DbCampoTipoAto.Where(c => c.IdCtaAcessoSist == IdCtaAcessoSist) on ta.IdCampoTipoAto equals ac.Id    
                orderby ac.Entidade, ac.NomeCampo
                select new 
                {
                    Id = ac.Id,
                    IdCtaAcessoSist = ac.IdCtaAcessoSist,
                    NomeCampo = ac.NomeCampo,
                    PlaceHolder = ac.PlaceHolder,
                    Campo = ac.Campo,
                    Entidade = ac.Entidade
                };

            foreach (var campo in listaCampos)
            {
                campoTipoAtos.Add(
                    new CampoTipoAto
                    {
                        Id = campo.Id,
                        IdCtaAcessoSist = campo.IdCtaAcessoSist,
                        Campo = campo.Campo,
                        NomeCampo = campo.NomeCampo,
                        Entidade = campo.Entidade,
                        PlaceHolder = campo.PlaceHolder
                    }    
                );
            }

            return campoTipoAtos;
        }
    }
}