## How to Create a COM-Visible UserControl Using WebView2 (.NET Framework)
### Create a new project 
- Template: `Windows Froms Control Library (.Net framework)`.
- Name the project, for example: `iFixWebViewer`.
### Install WebView2 Package
- Right-click the project > Manage NuGet Packages.
- Search for and install the package: `Microsoft.Web.WebView2`.
- 
![Example Image](images/webview2.png)

### Design the UserControl
- Open `UserControl1.cs` (rename it to WebView if desired)
- Drag and drop the `WebView2` control from the Toolbox onto the form
- Rename the control to `webView21`
### Add a GUID for COM Interop
- Generate a new GUID using one of these methods:
- Visual Studio: Tools > Create GUID
PowerShell:
```base 
[Guid("4B66E666-33D6-4819-8BD6-D87885D3FE9C")]`
```
Add the generated GUID to your class as follows:
```csharp
[ComVisible(true)]
[ProgId("iFIXWebView")]
[Guid("4B66E666-33D6-4819-8BD6-D87885D3FE9C")]
```
### Initialize WebView2
Add a method to configure the `WebView2` control:
```csharp
private void InitializeWebView()
{
    webView21.Dock = DockStyle.Fill;
    if (!string.IsNullOrEmpty(_url))
    {
        webView21.Source = new Uri(_url);
    }
}
```
### Add a Public Property to Set the URL
Expose a property that allows external programs to set the URL:
```csharp
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

```
### Complete Example Code
```csharp
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
```
## How to Import .NET Component into iFIX
### Copy .NET Component Folder
- Copy the folder: `.\iFixWebViewer\bin\Release`
- Paste it into: `{Proficy iFIX Path}\DotNet Components\`
- Rename the `Release` folder to: `Webviewer`.
### Register the .NET Component
Use `RegAsm` to register the component:
```base
"{dotnet40}\RegAsm.exe" "{Proficy iFIX Path}\DotNet Components\Webviewer\iFixWebViewer.dll" /codebase
```
> [!NOTE]
> Replace {dotnet40} with the actual .NET Framework path (e.g. C:\Windows\Microsoft.NET\Framework64\v4.0.30319).
> 
> Replace {Proficy iFIX Path} with the actual Proficy iFIX Path.
### Import .NET Component into iFIX
- In iFIX Workspace, go to: Tools > Objects/Links > .NET Compone
- Click Add Components, then browse to: `iFixWebViewer.dll` inside the `Webviewer` folder
- From the list, select: `iFixWebViewer.WebView`.
### Using the WebViewer Control
- You can set the `URL` property to specify which website to display.
