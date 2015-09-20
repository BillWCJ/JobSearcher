using System.ComponentModel;
using System.Runtime.CompilerServices;
using Microsoft.Practices.Prism.PubSubEvents;
using Presentation.WPF.Annotations;

namespace Presentation.WPF
{
    public interface IViewModelBase : INotifyPropertyChanged
    {
    }

    public class ViewModelBase : IViewModelBase
    {
        protected EventAggregator Aggregator;
        public event PropertyChangedEventHandler PropertyChanged;

        public ViewModelBase(EventAggregator aggregator)
        {
            Aggregator = aggregator;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}