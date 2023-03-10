using KretaCommandLine.Model.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaCommandLine.APIModel
{
    public class QueryStringParameters
    {
        public int NumberOfPage { get; set; }=int.MaxValue;
        public int NumberOfItems { get; set; } =0;
        public int CurrentPage { get; set; } =0;
        public int PageSize { get; set; } = int.MaxValue;

        public bool HasPreviousPage => CurrentPage > 0;
        public bool HasNextPage => (CurrentPage+1) < NumberOfPage;

        public bool IsCorrect => CurrentPage >= 0 && CurrentPage < NumberOfPage && PageSize > 0;

        public void Populate<TEntity>(PagedList<TEntity> pagedList) where TEntity : ClassWithId, new()
        {
            NumberOfPage = pagedList.NumberOfPage;
            NumberOfItems = pagedList.NumberOfItems;
            CurrentPage = pagedList.CurrentPage;
            PageSize= pagedList.PageSize;
        }
    }

    /*

    */
}
