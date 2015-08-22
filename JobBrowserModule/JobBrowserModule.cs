using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace JobBrowserModule
{
    public class JobBrowserModule : IModule
    {
        public const string JobPostingTable = "JobPostingTable";
        public const string FilterPanel = "FilterPanel";
        private readonly IRegionManager _regionManager;

        public JobBrowserModule(IRegionManager regionManager)
        {
            this._regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion(JobPostingTable, typeof(Views.PostingTableView));
            _regionManager.RegisterViewWithRegion(FilterPanel, typeof(Views.FilterPanelView));
        }
    }
}