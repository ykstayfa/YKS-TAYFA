
namespace YKSTayfa.Services;

public class ThemeService
{
    public void SetTheme(AppTheme theme) => Application.Current.UserAppTheme = theme;
}
