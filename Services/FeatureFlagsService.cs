
namespace YKSTayfa.Services;

public class FeatureFlagsService
{
    const string AdsKey = "use_ads";
    const string FcmKey = "use_fcm";

    public Task SetAsync(bool? useAds = null, bool? useFcm = null)
    {
        if (useAds.HasValue) Preferences.Set(AdsKey, useAds.Value);
        if (useFcm.HasValue) Preferences.Set(FcmKey, useFcm.Value);
        return Task.CompletedTask;
    }

    public bool UseAds => Preferences.Get(AdsKey, false);
    public bool UseFcm => Preferences.Get(FcmKey, false);
}
