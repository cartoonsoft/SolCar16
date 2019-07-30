using Domain.CartNew.Enumerations;
using Domain.CartNew.Interfaces.Repositories;
using Dto.Cartorio.Entities.Diversos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibFunctions.Functions.FeriadosFunc
{
    public class FeriadoHelper
    {
        private readonly IRepositoryFeriadoHelper _repositoryFeriadoHelper;

        #region  Private Fields
        private DateTime _DomingoPascoa;
        private DateTime _DomingoCarnaval;
        private DateTime _SegundaCarnaval;
        private DateTime _TercaCarnaval;
        private DateTime _SextaPaixao;
        private DateTime _CorpusChristi;
        private int _ano;
        #endregion

        /// <summary>
        //  Função para calcular a data do domingo de pascoa dado um ano qualquer
        /// </summary>
        /// <param name="AnoCalcular"></param>
        /// <returns></returns>
        private DateTime CalculaDiaPascoa(int AnoCalcular)
        {
            int x = 24;
            int y = 5;

            int a = AnoCalcular % 19;
            int b = AnoCalcular % 4;
            int c = AnoCalcular % 7;

            int d = (19 * a + x) % 30;
            int e = (2 * b + 4 * c + 6 * d + y) % 7;

            int dia = 0;
            int mes = 0;

            if (d + e > 9)
            {
                dia = (d + e - 9);
                mes = 4;
            }
            else
            {
                dia = (d + e + 22);
                mes = 3;
            }

            return DateTime.Parse(string.Format("{0},{1},{2}", AnoCalcular.ToString(), mes.ToString(), dia.ToString()));
        }

        private DateTime CalculaDomingoCarnaval(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-49);
        }

        private DateTime CalculaSegundaCarnaval(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-48);
        }

        private DateTime CalculaTercaCarnaval(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-47);
        }

        private DateTime CalculaSextaPaixao(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(-2);
        }

        private DateTime CalculaCorpusChristi(DateTime DataPascoa)
        {
            return DataPascoa.AddDays(+60);
        }

        /// <summary>
        /// Inicializa a classe feriados com o ano corrente
        /// </summary>
        public FeriadoHelper(int ano, IRepositoryFeriadoHelper repositoryFeriadoHelper)
        {
            _repositoryFeriadoHelper = repositoryFeriadoHelper;
            this._ano = ano;

            _DomingoPascoa   = CalculaDiaPascoa(this._ano);
            _DomingoCarnaval = CalculaDomingoCarnaval(_DomingoPascoa);
            _SegundaCarnaval = CalculaSegundaCarnaval(_DomingoPascoa);
            _TercaCarnaval   = CalculaTercaCarnaval(_DomingoPascoa);
            _SextaPaixao     = CalculaSextaPaixao(_DomingoPascoa);
            _CorpusChristi   = CalculaCorpusChristi(_DomingoPascoa);
        }

        private IEnumerable<DtoFeriado> FeriadosAno 
        {
            get {
                List<DtoFeriado> feriadosAno = new List<DtoFeriado>();
                feriadosAno.Add(new DtoFeriado { DataFeriado = DateTime.Parse("01/01/" + this._ano), Descricao = "Confraternização Universal", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = this._DomingoPascoa, Descricao = "Domingo de Páscoa", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = this._SegundaCarnaval, Descricao = "Segunda Carnaval", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = this._TercaCarnaval, Descricao = "Terça Carnaval", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = this._SextaPaixao, Descricao = "Sexta feira da paixão", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = DateTime.Parse("21/04/" + this._ano), Descricao = "Tiradentes", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = DateTime.Parse("01/05/" + this._ano), Descricao = "Dia do trabalho", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = this._CorpusChristi, Descricao = "Corpus Christi", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = DateTime.Parse("07/09/" + this._ano), Descricao = "Independência do Brasil", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = DateTime.Parse("12/10/" + this._ano), Descricao = "Padroeira do Brasil", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = DateTime.Parse("02/11/" + this._ano), Descricao = "Finados", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = DateTime.Parse("15/11/" + this._ano), Descricao = "Proclamação da República", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });
                feriadosAno.Add(new DtoFeriado { DataFeriado = DateTime.Parse("25/12/" + this._ano), Descricao = "Natal", Ano = this._ano, PontoFacultativo = false, TipoFeriado = TiposFeriado.FeriadoNaciona, Ativo = true });

                //todo: Criar rotina para ler do banco os feriados municipais/regionais e povoar. 
                //Adicione aqui os feriados para sua cidade ou estado se necessário
                //_repositoryFeriadoHelper.FeriadosDoAno
                return FeriadosAno;
            }
        }

        public bool IsDiaUtil(DateTime data)
        {
            if (IsFimDeSemana(data) || IsFeriado(data))
                return false;
            else
                return true;
        }

        public bool IsFeriado(DateTime data, long? idMunicipio = null)
        {
            //todo: ronaldo fazer
            return false; // _feriados.Find(delegate (FeriadoHelper f1) { return f1.Data == data; }) != null ? true : false;
        }

        public bool IsFimDeSemana(DateTime data)
        {
            if (data.DayOfWeek == DayOfWeek.Sunday || data.DayOfWeek == DayOfWeek.Saturday)
                return true;
            else
                return false;
        }

        public DateTime ProximoDiaUtil(DateTime data)
        {
            DateTime auxData = data;

            if (IsFeriado(auxData) || IsFimDeSemana(auxData))
            {
                auxData = data.AddDays(1);
                auxData = ProximoDiaUtil(auxData);
            }

            return auxData;
        }

        public DateTime ObtenhaDataUtil(DateTime data, int quantidadeDiasUteis)
        {
            int cont = 0;

            while (true)
            {
                if (IsDiaUtil(data))
                {
                    cont++;
                }

                if (cont >= quantidadeDiasUteis)
                {
                    return data;
                }

                data = data.AddDays(1);
            }
        }

        #region Properties
        public DateTime FeriadoPascoa { get { return _DomingoPascoa; } }

        public DateTime FeriadoDomingoCarnaval { get { return _DomingoCarnaval; } }

        public DateTime FeriadoSegundaCarnaval { get { return _SegundaCarnaval; } }

        public DateTime FeriadoTercaCarnaval { get { return _TercaCarnaval; } }

        public DateTime FeriadoSextaPaixao { get { return _SextaPaixao; } }

        public DateTime FeriadoCorpusChristi { get { return _CorpusChristi; } }
        #endregion

    }
}
