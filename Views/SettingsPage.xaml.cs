
using YKSTayfa.Services;

namespace YKSTayfa.Views;

public partial class SettingsPage : ContentPage
{
    private readonly ThemeService _theme;
    private readonly ExportService _export;
    private readonly FeatureFlagsService _flags;
    private readonly IPushService _push;

    public SettingsPage(ThemeService theme, ExportService export, FeatureFlagsService flags, IPushService push)
    {
        InitializeComponent();
        _theme = theme; _export = export; _flags = flags; _push = push;
    }

    private void OnLight(object s, EventArgs e) => _theme.SetTheme(AppTheme.Light);
    private void OnDark(object s, EventArgs e) => _theme.SetTheme(AppTheme.Dark);

    private async void OnExportDb(object s, EventArgs e)
    {
        var path = await _export.ExportDatabaseAsync();
        await DisplayAlert("Yedek", $"DB kopyalandı:\n{path}", "Tamam");
    }
    private async void OnExportMessages(object s, EventArgs e)
    {
        var path = await _export.ExportMessagesCsvAsync();
        await DisplayAlert("CSV", $"Mesajlar kaydedildi:\n{path}", "Tamam");
    }
    private async void OnExportScores(object s, EventArgs e)
    {
        var path = await _export.ExportScoresCsvAsync();
        await DisplayAlert("CSV", $"Skorlar kaydedildi:\n{path}", "Tamam");
    }
    private async void OnReset(object s, EventArgs e)
    {
        var svc = new ResetDataService();
        await svc.ResetAsync();
        await DisplayAlert("Reset", "Demo veritabanı geri yüklendi.", "Tamam");
    }

    private async void OnAdsOn(object s, EventArgs e) => await _flags.SetAsync(useAds: true);
    private async void OnAdsOff(object s, EventArgs e) => await _flags.SetAsync(useAds: false);
    private async void OnFcmOn(object s, EventArgs e) { await _flags.SetAsync(useFcm: true); await _push.RegisterAsync(); }
    private async void OnFcmOff(object s, EventArgs e) { await _flags.SetAsync(useFcm: false); await _push.UnregisterAsync(); }
}
