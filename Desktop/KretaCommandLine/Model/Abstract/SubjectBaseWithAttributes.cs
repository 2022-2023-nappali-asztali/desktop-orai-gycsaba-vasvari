using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KretaCommandLine.Model.Abstract
{
    class SubjectBaseWithAttributes : SubjectBase
    {
        [Column("name")]
        [Display(Name = "Subject name:")]
        public override string SubjectName 
        { 
            get => base.SubjectName; 
            set => base.SubjectName = value; 
        }
    }
}
