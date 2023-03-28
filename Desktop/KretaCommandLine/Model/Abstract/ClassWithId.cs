using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.Model.Abstract
{
    public abstract class ClassWithId : ICloneable
    {
        [Key]
        [Column("id")]
        public long Id { get; set; } = -1;

        public bool HasId => Id > 0;

        public string DisplayedId => Id > 0 ? Id.ToString() : string.Empty; 

        public abstract object Clone();

        public ClassWithId(long id)
        {
            this.Id = id;
        }
    }
}
