using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Cache;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;

namespace Model.Entities.Web
{
    public interface ICookieEnabledWebClient
    {
        /// <summary>
        ///     Get the System.String DefaultUserAgent for the class object
        /// </summary>
        string DefaultUserAgent { get; }

        /// <summary>
        ///     Get the System.Net.CookieContainer CookieContainer that contains cookies for the current object
        /// </summary>
        CookieContainer CookieContainer { get; }

        /// <summary>
        ///     Get or Set the System.String UserAgent for the current object
        /// </summary>
        string UserAgent { get; set; }

        CookieCollection GetAllCookies();

        bool AllowReadStreamBuffering { get; set; }
        bool AllowWriteStreamBuffering { get; set; }
        Encoding Encoding { get; set; }
        string BaseAddress { get; set; }
        ICredentials Credentials { get; set; }
        bool UseDefaultCredentials { get; set; }
        WebHeaderCollection Headers { get; set; }
        NameValueCollection QueryString { get; set; }
        WebHeaderCollection ResponseHeaders { get; }
        IWebProxy Proxy { get; set; }
        RequestCachePolicy CachePolicy { get; set; }
        bool IsBusy { get; }
        ISite Site { get; set; }
        IContainer Container { get; }

        string DownloadString(string address, NameValueCollection data, string method = "POST");
        byte[] DownloadData(string address);
        byte[] DownloadData(Uri address);
        void DownloadFile(string address, string fileName);
        void DownloadFile(Uri address, string fileName);
        Stream OpenRead(string address);
        Stream OpenRead(Uri address);
        Stream OpenWrite(string address);
        Stream OpenWrite(Uri address);
        Stream OpenWrite(string address, string method);
        Stream OpenWrite(Uri address, string method);
        byte[] UploadData(string address, byte[] data);
        byte[] UploadData(Uri address, byte[] data);
        byte[] UploadData(string address, string method, byte[] data);
        byte[] UploadData(Uri address, string method, byte[] data);
        byte[] UploadFile(string address, string fileName);
        byte[] UploadFile(Uri address, string fileName);
        byte[] UploadFile(string address, string method, string fileName);
        byte[] UploadFile(Uri address, string method, string fileName);
        byte[] UploadValues(string address, NameValueCollection data);
        byte[] UploadValues(Uri address, NameValueCollection data);
        byte[] UploadValues(string address, string method, NameValueCollection data);
        byte[] UploadValues(Uri address, string method, NameValueCollection data);
        string UploadString(string address, string data);
        string UploadString(Uri address, string data);
        string UploadString(string address, string method, string data);
        string UploadString(Uri address, string method, string data);
        string DownloadString(string address);
        string DownloadString(Uri address);
        void OpenReadAsync(Uri address);
        void OpenReadAsync(Uri address, object userToken);
        void OpenWriteAsync(Uri address);
        void OpenWriteAsync(Uri address, string method);
        void OpenWriteAsync(Uri address, string method, object userToken);
        void DownloadStringAsync(Uri address);
        void DownloadStringAsync(Uri address, object userToken);
        void DownloadDataAsync(Uri address);
        void DownloadDataAsync(Uri address, object userToken);
        void DownloadFileAsync(Uri address, string fileName);
        void DownloadFileAsync(Uri address, string fileName, object userToken);
        void UploadStringAsync(Uri address, string data);
        void UploadStringAsync(Uri address, string method, string data);
        void UploadStringAsync(Uri address, string method, string data, object userToken);
        void UploadDataAsync(Uri address, byte[] data);
        void UploadDataAsync(Uri address, string method, byte[] data);
        void UploadDataAsync(Uri address, string method, byte[] data, object userToken);
        void UploadFileAsync(Uri address, string fileName);
        void UploadFileAsync(Uri address, string method, string fileName);
        void UploadFileAsync(Uri address, string method, string fileName, object userToken);
        void UploadValuesAsync(Uri address, NameValueCollection data);
        void UploadValuesAsync(Uri address, string method, NameValueCollection data);
        void UploadValuesAsync(Uri address, string method, NameValueCollection data, object userToken);
        void CancelAsync();
        Task<string> DownloadStringTaskAsync(string address);
        Task<string> DownloadStringTaskAsync(Uri address);
        Task<Stream> OpenReadTaskAsync(string address);
        Task<Stream> OpenReadTaskAsync(Uri address);
        Task<Stream> OpenWriteTaskAsync(string address);
        Task<Stream> OpenWriteTaskAsync(Uri address);
        Task<Stream> OpenWriteTaskAsync(string address, string method);
        Task<Stream> OpenWriteTaskAsync(Uri address, string method);
        Task<string> UploadStringTaskAsync(string address, string data);
        Task<string> UploadStringTaskAsync(Uri address, string data);
        Task<string> UploadStringTaskAsync(string address, string method, string data);
        Task<string> UploadStringTaskAsync(Uri address, string method, string data);
        Task<byte[]> DownloadDataTaskAsync(string address);
        Task<byte[]> DownloadDataTaskAsync(Uri address);
        Task DownloadFileTaskAsync(string address, string fileName);
        Task DownloadFileTaskAsync(Uri address, string fileName);
        Task<byte[]> UploadDataTaskAsync(string address, byte[] data);
        Task<byte[]> UploadDataTaskAsync(Uri address, byte[] data);
        Task<byte[]> UploadDataTaskAsync(string address, string method, byte[] data);
        Task<byte[]> UploadDataTaskAsync(Uri address, string method, byte[] data);
        Task<byte[]> UploadFileTaskAsync(string address, string fileName);
        Task<byte[]> UploadFileTaskAsync(Uri address, string fileName);
        Task<byte[]> UploadFileTaskAsync(string address, string method, string fileName);
        Task<byte[]> UploadFileTaskAsync(Uri address, string method, string fileName);
        Task<byte[]> UploadValuesTaskAsync(string address, NameValueCollection data);
        Task<byte[]> UploadValuesTaskAsync(string address, string method, NameValueCollection data);
        Task<byte[]> UploadValuesTaskAsync(Uri address, NameValueCollection data);
        Task<byte[]> UploadValuesTaskAsync(Uri address, string method, NameValueCollection data);
        event WriteStreamClosedEventHandler WriteStreamClosed;
        event OpenReadCompletedEventHandler OpenReadCompleted;
        event OpenWriteCompletedEventHandler OpenWriteCompleted;
        event DownloadStringCompletedEventHandler DownloadStringCompleted;
        event DownloadDataCompletedEventHandler DownloadDataCompleted;
        event AsyncCompletedEventHandler DownloadFileCompleted;
        event UploadStringCompletedEventHandler UploadStringCompleted;
        event UploadDataCompletedEventHandler UploadDataCompleted;
        event UploadFileCompletedEventHandler UploadFileCompleted;
        event UploadValuesCompletedEventHandler UploadValuesCompleted;
        event DownloadProgressChangedEventHandler DownloadProgressChanged;
        event UploadProgressChangedEventHandler UploadProgressChanged;
        void Dispose();
        string ToString();
        event EventHandler Disposed;
        object GetLifetimeService();
        object InitializeLifetimeService();
        ObjRef CreateObjRef(Type requestedType);
    }
}