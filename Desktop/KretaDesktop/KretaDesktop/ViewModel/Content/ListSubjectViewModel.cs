﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KretaCommandLine.Model;
using KretaDesktop.ViewModel.BaseClass;

namespace KretaDesktop.ViewModel.Content
{
    public class ListSubjectViewModel : ListViewModelBase<Subject>
    {
        public ListSubjectViewModel()
        {
            InitializePage();
        }
    }
}
