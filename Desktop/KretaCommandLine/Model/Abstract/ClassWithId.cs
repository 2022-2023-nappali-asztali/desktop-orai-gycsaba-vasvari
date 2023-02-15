using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Abstract
{
    public abstract class ClassWithId : ICloneable
    {
        public long Id { get; set; }

        public abstract object Clone();            
    }
}
