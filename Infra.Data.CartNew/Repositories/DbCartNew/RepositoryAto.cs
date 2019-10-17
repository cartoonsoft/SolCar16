using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Cart11RI.Entities;
using Domain.CartNew.Entities;
using Domain.CartNew.Entities.Diversos;
using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.Repositories;
using Infra.Data.CartNew.Context;
using Infra.Data.Core.Repositories;
using LibFunctions.Functions.DatesFunc;
using Oracle.ManagedDataAccess.Client;

namespace Infra.Data.CartNew.Repositories.DbCartNew
{
    public class RepositoryAto : RepositoryBaseReadWrite<Ato>, IRepositoryAto
    {
        private readonly ContextMainCartNew _contextRepository;
        private readonly string[] PapelPessoaAto = { "O", "E" };  // O - outorgantes E - outorgados 

        public RepositoryAto(ContextMainCartNew contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }

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

        public IEnumerable<PessoaPesxPre> GetPessoasPrenotacao(long numeroPrenotacao) 
        {
            List<PessoaPesxPre> listaDtoPessoaPesxPres = new List<PessoaPesxPre>();

            var listaPessoasPrenotacao =
                from pre in this._contextRepository.DbPESXPRE.Where(pr => (pr.SEQPRE == numeroPrenotacao) && (PapelPessoaAto.Contains(pr.REL)))
                join pes in this._contextRepository.DbPESSOAS on pre.SEQPES equals pes.SEQPES
                orderby pre.REL, pes.NOM
                select new
                {
                    IdPessoa = pes.SEQPES,
                    IdPrenotacao = numeroPrenotacao,
                    TipoPessoa =
                        pre.REL == "E" ? TipoPessoaPrenotacao.outorgado :
                        pre.REL == "O" ? TipoPessoaPrenotacao.outorgante : TipoPessoaPrenotacao.indefinido,
                    Nome = pes.NOM,
                    Relacao = pre.REL,
                    Endereco = pes.ENDER,
                    Bairro = pes.BAI,
                    Cidade = pes.CID,
                    Telefone = pes.TEL,
                    Cep = pes.CEP,
                    Uf = pes.UF,
                    TipoDoc1 = pes.TIPODOC1,
                    Numero1 = pes.NRO1,
                    TipoDoc2 = pes.TIPODOC2,
                    Numero2 = pes.NRO2
                };

            foreach (var pessoa in listaPessoasPrenotacao)
            {
                listaDtoPessoaPesxPres.Add(new PessoaPesxPre {
                    IdPessoa = pessoa.IdPessoa,
                    IdPrenotacao = pessoa.IdPrenotacao,
                    TipoPessoa = pessoa.TipoPessoa,
                    Nome = pessoa.Nome,
                    Relacao = pessoa.Relacao,
                    Endereco = pessoa.Endereco,
                    Bairro = pessoa.Bairro,
                    Cidade = pessoa.Cidade,
                    Uf = pessoa.Uf,
                    Cep =  pessoa.Cep.ToString(),
                    Telefone = pessoa.Telefone,
                    TipoDoc1 = pessoa.TipoDoc1.ToString(),
                    Numero1 = pessoa.Numero1,
                    TipoDoc2 = pessoa.TipoDoc2,
                    Numero2 = pessoa.Numero2
                });
            }

            return null;
        }

        public IEnumerable<Ato> GetListAtosMatricula(string NumMatricula)
        {
            List<Ato> listaAtos = this.GetWhere(a => a.NumMatricula == NumMatricula).ToList();
            return listaAtos;
        }

        public IEnumerable<Ato> GetListAtosPeriodo(DateTime DataIni, DateTime DataFim)
        {
            if (DataIni > DataFim)
            {
                throw new AccessViolationException("Intervalo de datas inválido!");
            }

            List<Ato> listaAtos = this.GetWhere(a => (a.DataAto >= DataIni) && (a.DataAto <= DataFim)).ToList();
            return listaAtos;
        }

        public IEnumerable<PessoaAto> GetListPessoasAto(long? IdAto) 
        {
            List<PessoaAto> listaPessoas = new List<PessoaAto>();

            if(IdAto != null)
            {
                var qry =
                    from Pes in this._contextRepository.DbPESSOAS
                    join AtoPes in this._contextRepository.DbAtoPessoa.Where(ad => ad.IdAto == IdAto) on Pes.SEQPES equals AtoPes.SeqPes
                    join Atos in this._contextRepository.DbAto on AtoPes.IdAto equals Atos.Id
                    orderby Pes.NOM
                    select new
                    {
                        IdPessoa = Pes.SEQPES,
                        IdAto = AtoPes.IdAto,
                        IdPrenotacao  = Atos.IdPrenotacao,
                        Relacao = AtoPes.Relacao,
                        TipoPessoa = AtoPes.TipoPessoa,
                        Nome = Pes.NOM,
                        Endereco = Pes.ENDER,
                        Bairro = Pes.BAI,
                        Cidade = Pes.CID,
                        Uf = Pes.UF,
                        Cep = Pes.CEP,
                        Telefone = Pes.TEL,
                        TipoDoc1 = Pes.TIPODOC1,
                        TipoDoc2 = Pes.TIPODOC2,
                        Numero1 = Pes.NRO1,
                        Numero2 = Pes.NRO2
                    };

                foreach (var pessoa in qry)
                {
                    listaPessoas.Add(new PessoaAto { 
                        IdPessoa = pessoa.IdPessoa,
                        IdAto = pessoa.IdAto,
                        IdPrenotacao = pessoa.IdPrenotacao,
                        Relacao = pessoa.Relacao,
                        TipoPessoa = pessoa.TipoPessoa,
                        Nome = pessoa.Nome,
                        Endereco = pessoa.Endereco,
                        Bairro = pessoa.Bairro,
                        Cidade = pessoa.Cidade,
                        Uf = pessoa.Uf,
                        Cep = pessoa.Cep.ToString(),
                        Telefone = pessoa.Telefone,
                        TipoDoc1 = pessoa.TipoDoc1.ToString(),
                        TipoDoc2 = pessoa.TipoDoc2,
                        Numero1 = pessoa.Numero1,
                        Numero2 = pessoa.Numero2
                    });
                }
            }

            return listaPessoas;
        }

        public IEnumerable<Docx> GetListDocxAto(long? IdAto)
        {
            List<Docx> lista = new List<Docx>();

            if (IdAto != null)
            {
                var qry =
                    from Doc in this._contextRepository.DbDocx
                    join AtoDoc in this._contextRepository.DbAtoDocx.Where(ad => ad.IdAto == IdAto) on Doc.Id equals AtoDoc.IdAto 
                    orderby Doc.DataDocx descending
                    select new
                    {
                        IdDocx = Doc.Id,
                        IdCtaAcessoSist = Doc.IdCtaAcessoSist,
                        IdUsuarioCadastro = Doc.IdUsuarioCadastro,
                        IdUsuarioAlteracao = Doc.IdUsuarioAlteracao,
                        DataCadastro = Doc.DataCadastro,
                        DataAlteracao = Doc.DataAlteracao,
                        NumFicha = Doc.NumFicha,
                        DataDocx = Doc.DataDocx,
                        NomeArquivo = Doc.NomeArquivo
                    };

                foreach (var doc in qry)
                {
                    lista.Add(new Docx
                    {
                        Id = doc.IdDocx,
                        IdCtaAcessoSist = doc.IdCtaAcessoSist,
                        IdUsuarioCadastro = doc.IdUsuarioCadastro,
                        IdUsuarioAlteracao = doc.IdUsuarioAlteracao,
                        DataCadastro = doc.DataCadastro,
                        DataAlteracao = doc.DataAlteracao,
                        NumFicha = doc.NumFicha,
                        DataDocx = doc.DataDocx, //data cabeçalho docx
                        NomeArquivo = doc.NomeArquivo
                    });
                }
            }
            return lista;
        }

        public IEnumerable<DadosImovel> GetDadosImoveisPrenotacao(long IdPrenotacao) 
        {
            List<DadosImovel> lista = new List<DadosImovel>();
            string dataTmp = DateTimeFunctions.GetDataPorExtenso("São Paulo");

            var qryImoveis =
                from mi in this._contextRepository.DbMATRIMO.Where(m => (m.SEQPRE == IdPrenotacao) && (m.TIPO.Trim() == "1"))
                join pre in this._contextRepository.DbPREIMO.Where(p => (p.SEQPRE == IdPrenotacao)) on mi.NUMERO equals pre.MATRI into mi_pre
                from pre in mi_pre.DefaultIfEmpty() 
                orderby mi.NUMERO
                select new 
                {
                    IdPrenotacao = IdPrenotacao,
                    NumMatricula = mi.NUMERO,
                    DataAtualExtenso = dataTmp,
                    APTO = pre.APTO,
                    BLOCO = pre.BLOCO,
                    CONTRIB = pre.CONTRIB,
                    EDIF = pre.EDIF,
                    ENDER = pre.ENDER,
                    HIPO = pre.HIPO,
                    INSCR = pre.INSCR,
                    LOTE = pre.LOTE,
                    MATRI = pre.MATRI,
                    NUM = pre.NUM,
                    OUTROS = pre.OUTROS,
                    QUADRA = pre.QUADRA,
                    RD = pre.RD,
                    SEQIMO = pre.SEQIMO,
                    SEQPRE = pre.SEQPRE,
                    SUBD = pre.SUBD,
                    TIPO = pre.TIPO,
                    TITULO = pre.TITULO,
                    TRANS = pre.TRANS,
                    VAGA = pre.VAGA
                };

            foreach (var imovel in qryImoveis)
            {
                lista.Add(new DadosImovel {
                    IdPrenotacao = imovel.IdPrenotacao,
                    NumMatricula = imovel.NumMatricula.ToString(),
                    DataAtualExtenso = imovel.DataAtualExtenso,
                    APTO = imovel.APTO,
                    BLOCO = imovel.BLOCO,
                    CONTRIB = imovel.CONTRIB,
                    EDIF = imovel.EDIF,
                    ENDER = imovel.ENDER,
                    HIPO = imovel.HIPO,
                    INSCR = imovel.INSCR,
                    LOTE = imovel.LOTE,
                    MATRI = imovel.MATRI,
                    NUM = imovel.NUM,
                    OUTROS = imovel.OUTROS,
                    QUADRA = imovel.QUADRA,
                    RD = imovel.RD,
                    SEQIMO = imovel.SEQIMO,
                    SEQPRE = imovel.SEQPRE,
                    SUBD = imovel.SUBD,
                    TIPO = imovel.TIPO,
                    TITULO = imovel.TITULO,
                    TRANS = imovel.TRANS,
                    VAGA = imovel.VAGA
                });
            }

            return lista;
        }

        public IEnumerable<CamposValor> GetCamposAto(long IdAto)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CamposValor> GetCamposImovel(long numeroMatricula)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<CamposValor> GetCamposPessoa(long IdPessoa)
        {
            throw new NotImplementedException();
        }
    }

}
