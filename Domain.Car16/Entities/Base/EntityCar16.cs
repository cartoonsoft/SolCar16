using Domain.Core.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Car16.Entities.Base
{

    public class OraSequence: Attribute  
    {
        private readonly string _seqName;

        public OraSequence(string seqName )
        {
            _seqName = seqName;
        }

        public string  SequenceName
        {
            get { return _seqName; }
        }

    }

    public class EntityCar16: EntityBase
    {

        public EntityCar16(bool generateNewId = false) : base()
        {
            OraSequence oraSequence = (OraSequence)Attribute.GetCustomAttribute(this.GetType(), typeof(OraSequence));
            string teste = oraSequence.SequenceName;

        }

    }
}
