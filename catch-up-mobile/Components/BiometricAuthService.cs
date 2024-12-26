using Plugin.Fingerprint.Abstractions;
using Plugin.Fingerprint;

public interface IBiometricAuthService
{
    Task<bool> IsAvailableAsync();
    Task<bool> AuthenticateAsync(string title, string reason);
}

public class BiometricAuthService : IBiometricAuthService
{
    private const string FingerprintEnabledKey = "FingerprintEnabled";

    public async Task<bool> IsAvailableAsync()
    {
        return await CrossFingerprint.Current.IsAvailableAsync();
    }

    public async Task<bool> AuthenticateAsync(string title, string reason)
    {
        var request = new AuthenticationRequestConfiguration(title, reason);
        var result = await CrossFingerprint.Current.AuthenticateAsync(request);
        return result.Authenticated;
    }
}
