using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KretaDesktop.ViewModel.BaseClass;
using KretaDesktop.ViewModel.Command;
using KretaDesktop.ViewModel.Content;
using Microsoft.Extensions.Logging;

namespace KretaDesktop.ViewModel.Header
{
    public class DataManagmentHeaderViewModel : ViewModelBase
    {
		private readonly ILogger<DataManagmentHeaderViewModel> _logger;
		private ListStudentViewModel _studentViewModel;
		private ListSubjectViewModel _subjectViewModel;
		private StudentByClassViewModel _studentPerClassViewModel;

        private InitializedViewModelBase selectedView;
		public InitializedViewModelBase SelectedView
		{
			get { return selectedView; }
			set 
			{ 
				selectedView = value; 
				OnPropertyChanged();
			}
		}

		public AsyncRelayCommandWithParameter UpdateViewCommand { get; set; }

		public DataManagmentHeaderViewModel(ILogger<DataManagmentHeaderViewModel> logger, ListStudentViewModel studentViewModel, ListSubjectViewModel subjectViewModel, StudentByClassViewModel studentPerClassViewModel)
		{
			_logger = logger;
			_studentViewModel = studentViewModel;
			_subjectViewModel = subjectViewModel;
			_studentPerClassViewModel = studentPerClassViewModel;

			UpdateViewCommand = new AsyncRelayCommandWithParameter((parameter) => ChangeView(parameter),(ex) =>OnException());
		}

		private async Task ChangeView(object parameter)
		{
			if (parameter != null &&  parameter is string)
			{
				_logger.LogInformation($"{nameof(DataManagmentHeaderViewModel)} -> Választott menüpont: {parameter}");
				switch (parameter)
				{
					case "SubjectDataManagment":						
						SelectedView = _subjectViewModel;
                        await _subjectViewModel.OnInitialize();
                        break;
					case "StudentDateManagment":
						SelectedView = _studentViewModel;
                        await _studentViewModel.OnInitialize();
                        break;
					case "StudentPerClass":
						SelectedView = _studentPerClassViewModel;						
                        await _studentPerClassViewModel.OnInitialize();
                        break;
                    default:
						_logger.LogError($"A menüpont választás nem sikerült");
						break;
                }
			}
		}

		private void OnException()
		{
		}
    }
}
