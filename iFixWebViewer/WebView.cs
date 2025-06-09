using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Microsoft.Web.WebView2.WinForms;

namespace iFixWebViewer
{
    [ComVisible(true)]
    [ProgId("iFIXWebView")]
    [Guid("4B66E666-33D6-4819-8BD6-D87885D3FE9C")]

    public partial class WebView: UserControl
    {

        public string _url = "";
        public WebView()
        {
            InitializeComponent();
            InitializeWebView();
        }
        private void InitializeWebView()
        {
            webView21.Dock = DockStyle.Fill;
            if (!string.IsNullOrEmpty(_url))
            {
                webView21.Source = new Uri(_url);
            }
        }

        [ComVisible(true)]
        public string URL
        {
            get
            {
                return _url;
            }
            set
            {
                _url = value;
                webView21.Source = new Uri(_url);
            }
        }

    }
}
