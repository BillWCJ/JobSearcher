using System.ComponentModel;
using System.Runtime.CompilerServices;
using Presentation.WPF.Annotations;

namespace Presentation.WPF
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
    }

    public class ViewModelBase : IViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}