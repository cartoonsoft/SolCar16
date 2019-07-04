using Domain.Cartorio.Entities;
using Domain.Cartorio.Entities.CartorioNew;
using Domain.Cartorio.Interfaces.Repositories;
using Domain.Core.Interfaces.Data;
using Infra.Data.Cartorio.Context;
using Infra.Data.Cartorio.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Data.Cartorio.Repositories.DbCartorioNew
{
    public class RepositoryLogArquivoModeloDocx : RepositoryBaseReadWrite<LogArquivoModeloDocx>, IRepositoryLogArquivoModeloDocx
    {
        private readonly ContextMainCartorioNew _contextRepository;

        public RepositoryLogArquivoModeloDocx(ContextMainCartorioNew contextRepository) : base(contextRepository)
        {
            _contextRepository = contextRepository;
        }
    }
}
