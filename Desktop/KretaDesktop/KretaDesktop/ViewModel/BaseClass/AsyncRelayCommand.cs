using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KretaDesktop.ViewModel.BaseClass
{
    public class AsyncRelayCommand : AsyncCommandBase
    {
        private readonly Func<Task> _callback;

        public AsyncRelayCommand(Func<Task> callback, Action<Exception> onException) : base(onException)
        {
            _callback = callback;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _callback();
        }
    }

    public class AsyncRelayCommandWithParameter : AsyncCommandBase
    {
        private readonly Func<object,Task> _callback;

        public AsyncRelayCommandWithParameter(Func<object,Task> callback, Action<Exception> onException) : base(onException)
        {
            _callback = callback;
        }

        protected override async Task ExecuteAsync(object parameter)
        {
            await _callback(parameter);
        }
    }
}
