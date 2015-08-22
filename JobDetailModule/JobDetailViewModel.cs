using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Business.Manager;
using JobBrowserModule.ViewModels;
using Model.Entities.JobMine;
using Presentation.WPF;

namespace JobDetailModule
{
    public class JobDetailViewModel : ViewModelBase
    {
        private Job _currentJob;

        public Job CurrentJob
        {
            get
            {
                return _currentJob;
            }
            set
            {
                _currentJob = value;
                OnPropertyChanged("CurrentJobString");
                OnPropertyChanged("GoogleSearchUrl");
            }
        }

        public Action JobChangedCallBack { get; set; }

        public string CurrentJobString
        {
            get
            {
                return CurrentJob != null ? CurrentJob.ToString("") : "No Job Selected";
            }
        }

        public void JobChanged(Job newJob)
        {
            CurrentJob = newJob;
            if (JobChangedCallBack != null)
                JobChangedCallBack();
        }
        public void AddSelectedJobToShortList(string name)
        {
            if (LocalShortListManager.AddJobToShortList(CurrentJob, name))
            {
                if (!ShortListNames.Contains(name))
                    ShortListNames.Add(name);
            }

        }

        public ObservableCollection<string> ShortListNames { get; set; }

        public string GoogleSearchUrl
        {
            get
            { 
                return @"http://www.google.com/search?q=" + (CurrentJob == null ? "" : CurrentJob.Employer.Name);
            }
        }

        public string GoogleMapUrl
        {
            get
            {
                //ie mode
                return @"http://maps.google.com/?q=" + (CurrentJob == null ? "" : CurrentJob.Employer.Name + " " + CurrentJob.JobLocation.Region);
            }
        }
    }

    public static class WebBrowserProperties
    {
        public static readonly DependencyProperty UrlProperty = DependencyProperty.RegisterAttached("Url", typeof(string), typeof(WebBrowserProperties), new UIPropertyMetadata(string.Empty, UrlPropertyChanged));

        public static string GetUrl(DependencyObject dependencyObject)
        {
            return (string)dependencyObject.GetValue(UrlProperty);
        }

        public static void SetUrl(DependencyObject dependencyObject, string value)
        {
            dependencyObject.SetValue(UrlProperty, value);
        }

        public static void UrlPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            WebBrowser webBrowser = dependencyObject as WebBrowser;
            if (webBrowser != null && GetUrl(webBrowser) != string.Empty)
            {
                webBrowser.Navigate(GetUrl(webBrowser));
                webBrowser.LoadCompleted += WebBrowserLoaded;
            }
        }

        public static void WebBrowserLoaded(object sender, NavigationEventArgs e)
        {
        }
    }
}