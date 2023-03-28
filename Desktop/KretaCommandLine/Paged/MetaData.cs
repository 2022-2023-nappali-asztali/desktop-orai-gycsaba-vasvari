using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIHelpersLibrary.Paged
{
    public class MetaData
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
        public bool HasFirst => CurrentPage > 1;
        public bool HasLast => CurrentPage< TotalPages;

        public override string ToString()
        {
            return $"Aktuális oldal:{CurrentPage}. Összes odal:{TotalPages} Lap méret:{PageSize}. Öszes elem száma:{TotalItems}. Előző van:{HasPrevious}. Következő van:{HasNext}. Előző van:{HasFirst}. Ultolsó van:{HasLast}.";
        }
    }
}
