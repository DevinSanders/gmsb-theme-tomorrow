using Avalonia;
using Avalonia.Headless;

// Bootstraps a headless Avalonia runtime for the whole test assembly so that
// [AvaloniaFact]/[AvaloniaTheory] tests can load embedded avares:// resources.
[assembly: AvaloniaTestApplication(typeof(TomorrowThemePlugin.Tests.TestApp))]

namespace TomorrowThemePlugin.Tests;

public sealed class TestApp : Application
{
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<TestApp>().UseHeadless(new AvaloniaHeadlessPlatformOptions());
}
