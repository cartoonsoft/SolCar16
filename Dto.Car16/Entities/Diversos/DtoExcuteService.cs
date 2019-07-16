using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto.Car16.Entities.Diversos
{
    public class DtoExcuteService
    {
        public DtoExcuteService()
        {
            this.Id = new Guid();
            this.Execute = false;
        }

        public Guid Id { get; private set; }
        public bool Execute { get; set; }
        public string Message { get; set; }
    }
}
