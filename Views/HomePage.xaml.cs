
namespace YKSTayfa.Views;

public partial class HomePage : ContentPage
{
    public HomePage() { InitializeComponent(); }
    private async void OnSettings(object s, EventArgs e) => await Shell.Current.GoToAsync("//Ayarlar");
    private async void OnScoreCalc(object s, EventArgs e) => await Shell.Current.GoToAsync("ScoreCalcPage");
    private async void OnHealth(object s, EventArgs e) => await Shell.Current.GoToAsync("HealthCheckPage");
    private async void OnAbout(object s, EventArgs e) => await Shell.Current.GoToAsync("AboutPage");
    private async void OnPrivacy(object s, EventArgs e) => await Shell.Current.GoToAsync("PrivacyPage");
}
