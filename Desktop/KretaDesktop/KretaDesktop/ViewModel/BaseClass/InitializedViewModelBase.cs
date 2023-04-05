﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public abstract class InitializedViewModelBase : ViewModelBase
    {
        public abstract Task OnInitialize();
    }
}
