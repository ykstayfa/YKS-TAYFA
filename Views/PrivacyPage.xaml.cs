
namespace YKSTayfa.Views;

public partial class PrivacyPage : ContentPage
{
    public PrivacyPage()
    {
        InitializeComponent();
        using var s = FileSystem.OpenAppPackageFileAsync("privacy.html").Result;
        using var sr = new StreamReader(s);
        var html = sr.ReadToEnd();
        Web.Source = new HtmlWebViewSource { Html = html };
    }
}
