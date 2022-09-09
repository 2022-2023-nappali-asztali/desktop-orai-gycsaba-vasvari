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
        [Required(ErrorMessage = "The name is required")]
        [StringLength(30, ErrorMessage = "The name cannot be longer than 30 characters")]
        [MinLength(3, ErrorMessage = "The name must be at least 3 characters long")]
        [RegularExpression(@"[^A-ZÁÉIÍOÓÖŐUÚÜŰ]+[a-záéiíoóöőuúüű]*$",ErrorMessage = "First letter must be uppercase, other lowercase")]
        public override string SubjectName 
        { 
            get => base.SubjectName; 
            set => base.SubjectName = value; 
        }
    }
}
