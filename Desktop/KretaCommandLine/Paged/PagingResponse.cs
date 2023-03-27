using System.Collections.Generic;

namespace APIHelpersLibrary.Paged
{
    public class PagingResponse<T> 
    {
        public List<T>? Items { get; set; } = null;
        public MetaData MetaData { get; set; } = new MetaData();
    }
}
