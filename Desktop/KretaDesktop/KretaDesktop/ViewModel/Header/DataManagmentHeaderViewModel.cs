using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Content;
using Microsoft.Extensions.Logging;

namespace KretaDesktop.ViewModel.Header
{
    public class DataManagmentHeaderViewModel : ViewModelBase
    {
		private readonly ILogger<DataManagmentHeaderViewModel> _logger;
		private ContentListStudentViewModel _studentViewModel;
		private ContentListSubjectViewModel _subjectViewModel;

        private ViewModelBase selectedView;
		public ViewModelBase SelectedView
		{
			get { return selectedView; }
			set 
			{ 
				selectedView = value; 
				OnPropertyChanged();
			}
		}

		public RelayCommand UpdateViewCommand { get; set; }

		public DataManagmentHeaderViewModel(ILogger<DataManagmentHeaderViewModel> logger, ContentListStudentViewModel studentViewModel, ContentListSubjectViewModel subjectViewModel )
		{
			_logger = logger;
			_studentViewModel = studentViewModel;
			_subjectViewModel = subjectViewModel;

			UpdateViewCommand = new RelayCommand((parameter) => ChangeView(parameter));
		}

		private void ChangeView(object parameter)
		{
			if (parameter != null &&  parameter is string)
			{
				_logger.LogInformation($"{nameof(DataManagmentHeaderViewModel)} -> Választott menüpont: {parameter}");
				switch (parameter)
				{
					case "SubjectDataManagment":
						SelectedView = _subjectViewModel;
						break;
					case "StudentDateManagment":
						SelectedView = _studentViewModel;
						break;
					default:
						_logger.LogError($"A menüpont választás nem sikerült");
						break;
                }
			}
		}




	}
}
