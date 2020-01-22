﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
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
        private readonly string[] PapelPessoaAto = { "O", "E" };  // O - outorgante E - outorgado 

        public RepositoryAto(ContextMainCartNew contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }

        #region Private Methods
        private IEnumerable<CampoTipoAto> GetListCampos(long? IdTipoAto, long IdCtaAcessoSist, string entidade)
        {
            List<CampoTipoAto> campoTipoAtos = new List<CampoTipoAto>();

            var listaCampos =
                from ta in _contextRepository.DbTipoAtoCampo.Where(a => a.IdTipoAto == IdTipoAto)
                join ac in _contextRepository.DbCampoTipoAto.Where(c => (c.IdCtaAcessoSist == IdCtaAcessoSist) && (c.Entidade.ToLower() == entidade.ToLower())) on ta.IdCampoTipoAto equals ac.Id
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

        private TipoPessoaPrenotacao GetTipoPessoa(string relacao)
        {
            return 
                relacao == "E" ? TipoPessoaPrenotacao.outorgado :
                relacao == "O" ? TipoPessoaPrenotacao.outorgante : TipoPessoaPrenotacao.indefinido;

        }
        #endregion


        public override Ato Add(Ato entity)
        {
            entity.Ativo = true;
            return base.Add(entity);
        }

        public bool ExisteAtoCadastrado(string NumMatricula)
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
                    command.Parameters.Add(new OracleParameter("NRO_MATRICULA", OracleDbType.Varchar2, NumMatricula, System.Data.ParameterDirection.Input));

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

        public long? GetNumSequenciaAto(string NumMatricula)
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
                    command.Parameters.Add(new OracleParameter("NRO_MATRICULA", OracleDbType.Long, NumMatricula, System.Data.ParameterDirection.Input));

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

        public IEnumerable<Docx> GerarFichas(Ato ato)
        {
            throw new NotImplementedException();
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

            DateTime dataFimTmp = DataFim.AddDays(1);

            List<Ato> listaAtos = this.GetWhere(a => 
                ((a.DataCadastro >= DataIni) && (a.DataCadastro < dataFimTmp) && (a.DataAlteracao == null)) ||
                ((a.DataAlteracao >= DataIni) && (a.DataAlteracao < dataFimTmp) && (a.DataAlteracao != null))
                
            ).OrderByDescending(o => (o.DataAlteracao ?? o.DataCadastro).Ticks).ToList();

            return listaAtos;
        }

        //qd nao sanvou o ato, para gerar o docx
        public IEnumerable<CampoTipoAto> GetListCamposAto(long IdTipoAto, long IdCtaAcessoSist)
        {
            return this.GetListCampos(IdTipoAto, IdCtaAcessoSist, "ATO");
        }
        public IEnumerable<CampoTipoAto> GetListCamposPrenotacao(long IdTipoAto, long IdCtaAcessoSist) 
        {
            return this.GetListCampos(IdTipoAto, IdCtaAcessoSist, "PRENOTACAO");
        }

        public IEnumerable<CampoTipoAto> GetListCamposImovel(long IdTipoAto, long IdCtaAcessoSist)
        {
            return this.GetListCampos(IdTipoAto, IdCtaAcessoSist, "IMOVEL");
        }

        public IEnumerable<CampoTipoAto> GetListCamposPessoa(long IdTipoAto, long IdCtaAcessoSist)
        {
            return this.GetListCampos(IdTipoAto, IdCtaAcessoSist, "PESSOA");
        }

        public DateTime? DataRegPrenotacao(long IdPrenotacao)
        {
            DateTime? dataTmp = null;
            var Premad = this._contextRepository.DbPREMAD.Where(pp => (pp.SEQPRE == IdPrenotacao) && (pp.TIPODATA.Trim() == "R")).FirstOrDefault();

            if (Premad != null)
            {
                dataTmp = new DateTime(1800, 1, 2, 0, 0, 0);
                dataTmp = dataTmp.Value.AddDays(Premad.DATA);
            }

            return dataTmp;
        }


        public IEnumerable<DadosImovel> GetListImoveisPrenotacao(long IdPrenotacao)
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
                lista.Add(new DadosImovel
                {
                    IdPrenotacao = imovel.IdPrenotacao,
                    NumMatricula = imovel.NumMatricula.ToString(),
                    APTO = imovel.APTO.Trim(),
                    BLOCO = imovel.BLOCO.Trim(),
                    CONTRIB = imovel.CONTRIB,
                    EDIF = imovel.EDIF.Trim(),
                    ENDER = imovel.ENDER.Trim(),
                    HIPO = imovel.HIPO,
                    INSCR = imovel.INSCR,
                    LOTE = imovel.LOTE.Trim(),
                    MATRI = imovel.MATRI,
                    NUM = imovel.NUM.Trim(),
                    OUTROS = imovel.OUTROS.Trim(),
                    QUADRA = imovel.QUADRA.Trim(),
                    RD = imovel.RD,
                    SEQIMO = imovel.SEQIMO,
                    SEQPRE = imovel.SEQPRE,
                    SUBD = imovel.SUBD,
                    TIPO = imovel.TIPO.Trim(),
                    TITULO = imovel.TITULO.Trim(),
                    TRANS = imovel.TRANS,
                    VAGA = imovel.VAGA.Trim()
                });
            }

            return lista;
        }

        public IEnumerable<PessoaAto> GetListPessoasAto(long? IdAto)
        {
            List<PessoaAto> listaPessoas = new List<PessoaAto>();

            if (IdAto != null)
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
                        IdPrenotacao = Atos.IdPrenotacao,
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
                    listaPessoas.Add(new PessoaAto
                    {
                        IdPessoa = pessoa.IdPessoa,
                        IdAto = pessoa.IdAto,
                        IdPrenotacao = pessoa.IdPrenotacao,
                        Relacao = pessoa.Relacao.Trim(),
                        TipoPessoa = pessoa.TipoPessoa,
                        Nome = pessoa.Nome.Trim(),
                        Endereco = pessoa.Endereco.Trim(),
                        Bairro = pessoa.Bairro.Trim(),
                        Cidade = pessoa.Cidade.Trim(),
                        Uf = pessoa.Uf.Trim(),
                        Cep = pessoa.Cep.ToString().Trim(),
                        Telefone = pessoa.Telefone.Trim(),
                        TipoDoc1 = pessoa.TipoDoc1.ToString(),
                        TipoDoc2 = pessoa.TipoDoc2.Trim(),
                        Numero1 = pessoa.Numero1.Trim(),
                        Numero2 = pessoa.Numero2.Trim()
                    });
                }
            }

            return listaPessoas;
        }

        /// <summary>
        /// os métodos GetListPessoas, GetPessoa estão aqui pq se referem a pessoas da base onzeri, só são lidos para gerar o ato
        /// </summary>
        /// <param name="IdPrenotacao"></param>
        /// <returns></returns>
        public IEnumerable<PessoaPesxPre> GetListPessoasPrenotacao(long IdPrenotacao)
        {
            List<PessoaPesxPre> listaPessoaPesxPre = new List<PessoaPesxPre>();

            var listaPessoasPrenotacao =
                from pre in this._contextRepository.DbPESXPRE.Where(pr => (pr.SEQPRE == IdPrenotacao) && (PapelPessoaAto.Contains(pr.REL)))
                join pes in this._contextRepository.DbPESSOAS on pre.SEQPES equals pes.SEQPES
                orderby pre.REL, pes.NOM
                select new
                {
                    IdPessoa = pes.SEQPES,
                    IdPrenotacao = IdPrenotacao,
                    Relacao = pre.REL,
                    Nome = pes.NOM,
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
                listaPessoaPesxPre.Add(new PessoaPesxPre
                {
                    IdPessoa = pessoa.IdPessoa,
                    IdPrenotacao = pessoa.IdPrenotacao,
                    Relacao = pessoa.Relacao.Trim(),
                    TipoPessoa = this.GetTipoPessoa(pessoa.Relacao.Trim()),
                    Nome = pessoa.Nome.Trim(),
                    Endereco = pessoa.Endereco.Trim(),
                    Bairro = pessoa.Bairro.Trim(),
                    Cidade = pessoa.Cidade.Trim(),
                    Uf = pessoa.Uf.Trim(),
                    Cep = pessoa.Cep.ToString().Trim(),
                    Telefone = pessoa.Telefone.Trim(),
                    TipoDoc1 = pessoa.TipoDoc1.ToString().Trim(),
                    Numero1 = pessoa.Numero1.Trim(),
                    TipoDoc2 = pessoa.TipoDoc2.Trim(),
                    Numero2 = pessoa.Numero2.Trim()
                });
            }

            return listaPessoaPesxPre;
        }

        public IEnumerable<PessoaPesxPre> GetListPessoas(long[] idsPessoas, long? idPrenotacao)
        {
            List<PessoaPesxPre> listaPessoaCart11RI = new List<PessoaPesxPre>();

            var listaPessoas =
                from  pes in this._contextRepository.DbPESSOAS.Where(p => idsPessoas.Contains(p.SEQPES))
                join  pre in this._contextRepository.DbPESXPRE.Where(pr => (pr.SEQPRE == idPrenotacao) && (PapelPessoaAto.Contains(pr.REL))) on pes.SEQPES equals pre.SEQPES into _pre
                from pre in _pre.DefaultIfEmpty()
                orderby pre.REL, pes.NOM
                select new
                {
                    IdPessoa = pes.SEQPES,
                    IdPrenotacao = idPrenotacao,
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

            foreach (var pessoa in listaPessoas)
            {
                listaPessoaCart11RI.Add(new PessoaPesxPre
                {
                    IdPessoa = pessoa.IdPessoa,
                    IdPrenotacao = idPrenotacao??0,
                    Relacao = pessoa.Relacao.Trim(),
                    TipoPessoa = this.GetTipoPessoa(pessoa.Relacao.Trim()),
                    Nome = pessoa.Nome.Trim(),
                    Endereco = pessoa.Endereco.Trim(),
                    Bairro = pessoa.Bairro.Trim(),
                    Cidade = pessoa.Cidade.Trim(),
                    Uf = pessoa.Uf.Trim(),
                    Cep = pessoa.Cep.ToString(),
                    Telefone = pessoa.Telefone.Trim(),
                    TipoDoc1 = pessoa.TipoDoc1.ToString(),
                    Numero1 = pessoa.Numero1.Trim(),
                    TipoDoc2 = pessoa.TipoDoc2.Trim(),
                    Numero2 = pessoa.Numero2.Trim()
                });
            }

            return listaPessoaCart11RI;
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

        public IEnumerable<AtoEvento> GetListHistoricoAto(long? IdAto)
        {
            List<AtoEvento> listaAtoEvento = this._contextRepository.DbAtoEvento.Where(ae => ae.IdAto == IdAto).ToList();

            return listaAtoEvento;
        }

        public PessoaPesxPre GetPessoa(long idPessoa, long? idPrenotacao)
        {
            string relacao = string.Empty;
            var pessoa = this._contextRepository.DbPESSOAS.Where(p => p.SEQPES == idPessoa).FirstOrDefault();

            if (idPrenotacao.HasValue && (idPrenotacao.Value > 0))
            {
                var pesPrenotacao = this._contextRepository.DbPESXPRE.Where(pr => (pr.SEQPRE == idPrenotacao) && (pr.SEQPES == idPessoa) && (PapelPessoaAto.Contains(pr.REL))).FirstOrDefault();

                if (pesPrenotacao != null)
                {
                    relacao = pesPrenotacao.REL.Trim();
                }
            }

            PessoaPesxPre pessoaPesxPre = new PessoaPesxPre
            {
                IdPessoa = pessoa.SEQPES,
                IdPrenotacao = idPrenotacao??0,
                Relacao = relacao,
                TipoPessoa = this.GetTipoPessoa(relacao),
                Nome = pessoa.NOM.Trim(),
                Endereco = pessoa.ENDER.Trim(),
                Bairro = pessoa.BAI.Trim(),
                Cidade = pessoa.CID.Trim(),
                Uf = pessoa.UF.Trim(),
                Cep = pessoa.CEP.ToString(),
                Telefone = pessoa.TEL.Trim(),
                TipoDoc1 = pessoa.TIPODOC1.ToString(),
                Numero1 = pessoa.NRO1.Trim(),
                TipoDoc2 = pessoa.TIPODOC2.Trim(),
                Numero2 = pessoa.NRO2.Trim()
            };

            return pessoaPesxPre;
        }

        public short GetUltimoNumFicha(string NumMatricula)
        {
            throw new NotImplementedException();
        }

        public DadosImovel GetDadosImovel(long IdPrenotacao, string NumMatricula)
        {
            long mat = 0;
            DadosImovel dadosImovel = new DadosImovel();

            if (long.TryParse(NumMatricula, out mat))
            {
                var imovel = this._contextRepository.DbPREIMO.Where(p => (p.SEQPRE == IdPrenotacao) && (p.MATRI == mat)).FirstOrDefault();

                dadosImovel.IdPrenotacao = IdPrenotacao;
                dadosImovel.NumMatricula = NumMatricula;

                if (imovel != null)
                {
                    dadosImovel.APTO = imovel.APTO.Trim();
                    dadosImovel.BLOCO = imovel.BLOCO.Trim();
                    dadosImovel.CONTRIB = imovel.CONTRIB;
                    dadosImovel.EDIF = imovel.EDIF.Trim();
                    dadosImovel.ENDER = imovel.ENDER.Trim();
                    dadosImovel.HIPO = imovel.HIPO;
                    dadosImovel.INSCR = imovel.INSCR;
                    dadosImovel.LOTE = imovel.LOTE.Trim();
                    dadosImovel.MATRI = imovel.MATRI;
                    dadosImovel.NUM = imovel.NUM.Trim();
                    dadosImovel.OUTROS = imovel.OUTROS.Trim();
                    dadosImovel.QUADRA = imovel.QUADRA.Trim();
                    dadosImovel.RD = imovel.RD;
                    dadosImovel.SEQIMO = imovel.SEQIMO;
                    dadosImovel.SEQPRE = imovel.SEQPRE;
                    dadosImovel.SUBD = imovel.SUBD;
                    dadosImovel.TIPO = imovel.TIPO.Trim();
                    dadosImovel.TITULO = imovel.TITULO.Trim();
                    dadosImovel.TRANS = imovel.TRANS;
                    dadosImovel.VAGA = imovel.VAGA.Trim();
                }
            }

            return dadosImovel;
        }

    }

}
