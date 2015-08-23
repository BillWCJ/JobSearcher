using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.PubSubEvents;
using Model.Entities.JobMine;
using Model.Entities.PostingFilter;

namespace Presentation.WPF.Events
{
    public class FilterSelectionChangedEvent : PubSubEvent<IEnumerable<Filter>> { }
    public class SelectedJobChangedEvent : PubSubEvent<Job> { }
}
