﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIHelpersLibrary.Paged
{
    public class ItemParameters
    {        
        const int maxPageSize = 50;        
        private int _pageSize = 10;


        public ItemParameters(int pageSize)
        {
            _pageSize = pageSize;
           
        }

        public int PageSize { get => _pageSize; set => _pageSize = value > maxPageSize ? maxPageSize : value; }
        public int PageNumber { get; set; } = 1;
    }
}
